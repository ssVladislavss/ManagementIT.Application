using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationContracts.ViewModels.Application.ActionModels;
using AutoMapper;
using Contracts.Constants;
using Contracts.Enums;
using Contracts.ResponseModels;
using GreenPipes;
using ManagementIt.Core.Abstractions.Interlayer;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;

namespace ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerAction
{
    public class AllActionConsumer : IConsumer<ActionViewModel>
    {
        private readonly IActionService _actionService;
        private readonly IMapper _mapper;

        public AllActionConsumer(IActionService actionService, IMapper mapper)
        {
            _actionService = actionService ?? throw new ArgumentNullException(nameof(actionService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<ActionViewModel> context)
        {
            var result = await _actionService.GetAllAsync();

            var response = new AllActionResponse();
            if (!result.Success)
                response.Notification = new NotificationViewModel(result.Errors, e: result.AspNetException);
            else
            {
                response.Notification = new NotificationViewModel();
                response.Model = _mapper.Map<IEnumerable<ActionViewModel>>(result.Data);
            }

            await context.RespondAsync<AllActionResponse>(response);
        }
    }

    public class AllActionConsumerDefinition : ConsumerDefinition<AllActionConsumer>
    {
        public AllActionConsumerDefinition()
        {
            EndpointName = ApiShowConstants.GetAllAction;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<AllActionConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));
        }
    }
}