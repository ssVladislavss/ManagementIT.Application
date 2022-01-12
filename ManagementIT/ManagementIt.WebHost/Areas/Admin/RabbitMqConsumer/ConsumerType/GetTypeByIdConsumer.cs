using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationContracts.ViewModels.Application.TypeModels;
using AutoMapper;
using Contracts.Constants;
using Contracts.Enums;
using Contracts.ResponseModels;
using GreenPipes;
using ManagementIt.Core.Abstractions.Interlayer;
using ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerPriority;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;

namespace ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerType
{
    public class GetTypeByIdConsumer : IConsumer<TypeByIdRequest>
    {
        private readonly ITypeService _typeService;
        private readonly IMapper _mapper;

        public GetTypeByIdConsumer(ITypeService typeService, IMapper mapper)
        {
            _typeService = typeService ?? throw new ArgumentNullException(nameof(typeService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task Consume(ConsumeContext<TypeByIdRequest> context)
        {
            var result = await _typeService.GetByIdAsync(context.Message.Id);

            var response = new TypeByIdResponse();
            if (!result.Success)
                response.Notification = new NotificationViewModel(result.Errors, e: result.AspNetException);
            else
            {
                response.Notification = new NotificationViewModel();
                response.Model = _mapper.Map<ApplicationTypeViewModel>(result.Data);
            }
            await context.RespondAsync<TypeByIdResponse>(response);
        }
    }

    public class GetTypeByIdConsumerDefinition : ConsumerDefinition<GetTypeByIdConsumer>
    {
        public GetTypeByIdConsumerDefinition() => EndpointName = ApiShowConstants.ByIdType;
        
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<GetTypeByIdConsumer> consumerConfigurator) =>
            endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));
        
    }
}