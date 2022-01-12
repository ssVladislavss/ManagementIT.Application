using AutoMapper;
using ManagementIt.Core.Abstractions.AppRepository;
using ManagementIt.Core.Abstractions.TEntityRepository;
using ManagementIt.DataAccess.DataBase;
using ManagementIt.DataAccess.Repositories.AppEFRepository;
using ManagementIt.DataAccess.Repositories.TRepository;
using ManagementIt.WebHost.AutoMap;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using ManagementIt.Core.Abstractions.Interlayer;
using ManagementIt.Core.Abstractions.MongoAbstractions;
using ManagementIt.DataAccess.InterlayerRepositories;
using ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerAction;
using ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerApplication;
using ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerPriority;
using ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerState;
using ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerType;
using MassTransit;
using MassTransit.Definition;
using ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerExistDependency;
using ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ResetMemoryCashe;

namespace ManagementIt.WebHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region MassTransit

            var massTransitSection = Configuration.GetSection("MassTransit");
            var url = massTransitSection.GetValue<string>("Url");
            var host = massTransitSection.GetValue<string>("Host");
            var userName = massTransitSection.GetValue<string>("UserName");
            var password = massTransitSection.GetValue<string>("Password");

            services.AddMassTransit(x =>
            {
                x.AddBus(busFactory =>
                {
                    var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        cfg.Host($"rabbitmq://{url}{host}", configurator =>
                        {
                            configurator.Username(userName);
                            configurator.Password(password);
                        });
                        cfg.ConfigureEndpoints(busFactory, KebabCaseEndpointNameFormatter.Instance);
                        cfg.UseJsonSerializer();
                        cfg.UseHealthCheck(busFactory);
                    });
                    return bus;
                });
                x.AddConsumer<CreatePriorityConsumer>();
                x.AddConsumer<GetAllConsumer>();
                x.AddConsumer<PriorityByIdConsumer>();
                x.AddConsumer<UpdatePriorityConsumer>();
                x.AddConsumer<DeletePriorityConsumer>();
                
                x.AddConsumer<GetAllTypeConsumer>();
                x.AddConsumer<GetTypeByIdConsumer>();
                x.AddConsumer<CreateTypeConsumer>();
                x.AddConsumer<UpdateTypeConsumer>();
                x.AddConsumer<DeleteTypeConsumer>();
                
                x.AddConsumer<GetAllStateConsumer>();
                x.AddConsumer<GetByIdStateConsumer>();
                x.AddConsumer<CreateStateConsumer>();
                x.AddConsumer<UpdateStateConsumer>();
                x.AddConsumer<DeleteStateConsumer>();
                
                x.AddConsumer<GetAllApplicationConsumer>();
                x.AddConsumer<GetApplicationByIdConsumer>();
                x.AddConsumer<CreateApplicationConsumer>();
                x.AddConsumer<UpdateApplicationConsumer>();
                x.AddConsumer<DeleteApplicationConsumer>();
                x.AddConsumer<UpdateStateOrApplicationConsumer>();
                x.AddConsumer<ActivatedOnDeleteApplicationConsumer>();
                x.AddConsumer<GetApplicationByDeptIdConsumer>();
                x.AddConsumer<GetApplicationForOnDeleteConsumer>();
                x.AddConsumer<DeleteRangeApplicationConsumer>();
                x.AddConsumer<GetCreateApplicationConsumer>();
                x.AddConsumer<GetUpdateApplicationConsumer>();
                x.AddConsumer<UpdatePriorityOrApplicationConsumer>();
                x.AddConsumer<UpdateEmployeeFullNameConsumer>();
                x.AddConsumer<UpdateRoomNameConsumer>();
                x.AddConsumer<UpdateDepartmentNameConsumer>();
                x.AddConsumer<UpdateEmployeeOrApplicationConsumer>();
                x.AddConsumer<SetIniciatorOrApplicationConsumer>();

                x.AddConsumer<AllActionConsumer>();
                x.AddConsumer<ActionByIdConsumer>();
                x.AddConsumer<DeleteAllActionConsumer>();
                x.AddConsumer<ActionByEnumTypeConsumer>();
                x.AddConsumer<ActionByApplicationIdConsumer>();
                x.AddConsumer<DeleteSelectedActionConsumer>();

                x.AddConsumer<ExistDependencyConsumer>();

                x.AddConsumer<ClearMemoryCasheConsumer>();
            });

            services.AddMassTransitHostedService();

            #endregion

            #region PostgresDbContext

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
            });

            #endregion

            #region  AutoMapper

            var mapperConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            #endregion

            #region    DI

            services.AddTransient(typeof(IGenericRepository<>), typeof(EFGenericRepository<>));

            services.AddTransient<IApplicationTOITRepository, EFApplicationToItRepository>();
            services.AddTransient<IApplicationActionRepository, EFApplicationActionRepository>();
            services.AddTransient<IPriorityRepository, EFPriorityRepository>();
            services.AddTransient<IStateRepository, EFStateRepository>();


            services.AddTransient<IActionService, InterlayerAction>()
                    .AddTransient<IApplicationService, InterlayerAppToIt>()
                    .AddTransient<IPriorityService, InterlayerPriority>()
                    .AddTransient<IStateService, InterlayerState>()
                    .AddTransient<ITypeService, InterlayerType>();

            services.AddTransient<ILogService, LogService>();

            #endregion

            #region  Swagger

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ManagementIt.WebHost",
                    Version = "v1",
                    Description = "Swagger for ManagementIt with IdentityServer4"
                });

                config.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri("https://localhost:9001/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "ManagementIT.WebHost.Swagger", "Web API" }
                            }
                        }
                    }
                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }

                });
            });

            #endregion

            #region Настройка аутентификации

            services.AddAuthentication("Bearer")
                    .AddJwtBearer("Bearer", options =>
                    {
                        options.Authority = "https://localhost:9001";
                        options.SaveToken = true;
                        options.Audience = "ManagementIT.WebHost.Swagger";
                        options.RequireHttpsMetadata = false;
                        options.ClaimsIssuer = "role";
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateAudience = true,
                            NameClaimType = JwtClaimTypes.Name,
                            RoleClaimType = ClaimTypes.Role
                        };
                    })
                    .AddJwtBearer("Bearer1", options =>
                    {
                        options.Audience = "http://localhost:9001/";
                        options.Authority = "http://localhost:9001/";
                    });

            #endregion

            services.AddHttpClient();
            services.AddControllers();
            services.AddMemoryCache();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ManagementIt.WebHost v1");
                    c.DocumentTitle = "ManagementIT Swagger";
                    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
                    c.OAuthClientId("api_swagger");
                    c.OAuthClientSecret("client_secret_swagger");
                });
            }

            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
