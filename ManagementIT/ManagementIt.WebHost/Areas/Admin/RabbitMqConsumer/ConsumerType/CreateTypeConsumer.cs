using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationContracts.ViewModels.Application.TypeModels;
using Contracts.Constants;
using Contracts.Enums;
using Contracts.ResponseModels;
using GreenPipes;
using ManagementIt.Core.Abstractions.Interlayer;
using ManagementIt.Core.Models.AppModels;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;

namespace ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerType
{
    public class CreateTypeConsumer : IConsumer<CreateTypeViewModel>
    {
        private readonly ITypeService _typeService;

        public CreateTypeConsumer(ITypeService service) => _typeService = service ?? throw new ArgumentNullException(nameof(service));
        
        public async Task Consume(ConsumeContext<CreateTypeViewModel> context)
        {
            var existNameResult = _typeService.ExistEntityByName(context.Message.Name);
            if (!existNameResult)
            {
                var model = new ApplicationTypeModel(context.Message.Name);
                var result = await _typeService.AddAsync(model);

                var response = result.Success
                    ? new NotificationViewModel()
                    : new NotificationViewModel(result.Errors, e: result.AspNetException);

                await context.RespondAsync<NotificationViewModel>(response);
            }
            else await context.RespondAsync<NotificationViewModel>(new NotificationViewModel(new[] {TypeOfErrors.ExistNameEntity}));
        }
    }

    public class CreateTypeConsumerDefinition : ConsumerDefinition<CreateTypeConsumer>
    {
        public CreateTypeConsumerDefinition() => EndpointName = ApiShowConstants.CreateType;

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<CreateTypeConsumer> consumerConfigurator) =>
            endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));

    }
}