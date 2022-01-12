using System;
using System.Collections;
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
    public class GetAllTypeConsumer : IConsumer<ApplicationTypeViewModel>
    {
        private readonly ITypeService _typeService;
        private readonly IMapper _mapper;

        public GetAllTypeConsumer(ITypeService typeService, IMapper mapper)
        {
            _typeService = typeService ?? throw new ArgumentNullException(nameof(typeService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task Consume(ConsumeContext<ApplicationTypeViewModel> context)
        {
            var result = await _typeService.GetAllAsync();

            var response = new AllTypeResponse();
            if (!result.Success)
                response.Notification = new NotificationViewModel(result.Errors, e: result.AspNetException);
            else
            {
                response.Notification = new NotificationViewModel();
                response.model = _mapper.Map<IEnumerable<ApplicationTypeViewModel>>(result.Data);
            }
            await context.RespondAsync<AllTypeResponse>(response);
        }
    }

    public class GetAllTypeConsumerDefinition : ConsumerDefinition<GetAllTypeConsumer>
    {
        public GetAllTypeConsumerDefinition()
        {
            EndpointName = ApiShowConstants.AllType;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<GetAllTypeConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));
        }
    }
}