using MassTransit;
using System;
using System.Threading.Tasks;
using ApplicationContracts.ViewModels.Application.ActionModels;
using ManagementIt.Core.Abstractions.Interlayer;
using System.Security.Claims;
using Contracts.ResponseModels;
using MassTransit.Definition;
using Contracts.Constants;
using MassTransit.ConsumeConfigurators;
using GreenPipes;

namespace ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerAction
{
    public class DeleteAllActionConsumer : IConsumer<DeleteRangeActionRequest>
    {
        private readonly IActionService _actionService;

        public DeleteAllActionConsumer(IActionService actionService)
        {
            _actionService = actionService ?? throw new ArgumentNullException(nameof(actionService));
        }

        public async Task Consume(ConsumeContext<DeleteRangeActionRequest> context)
        {
            var result = await _actionService.DeleteAll();

            var response = result.Success ? new NotificationViewModel() : new NotificationViewModel(result.Errors, e: result.AspNetException);
            await context.RespondAsync<NotificationViewModel>(response);
        }
    }

    public class DeleteAllActionConsumerDefinition : ConsumerDefinition<DeleteAllActionConsumer>
    {
        public DeleteAllActionConsumerDefinition()
        {
            EndpointName = ApiShowConstants.DeleteAllAction;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<DeleteAllActionConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));
        }
    }
}
