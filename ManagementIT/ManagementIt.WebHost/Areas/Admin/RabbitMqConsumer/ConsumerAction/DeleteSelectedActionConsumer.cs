using ApplicationContracts.ViewModels.Application.ActionModels;
using Contracts.ResponseModels;
using ManagementIt.Core.Abstractions.Interlayer;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerAction
{
    public class DeleteSelectedActionConsumer : IConsumer<DeleteSelectActionRequest>
    {
        private readonly IActionService _actionService;

        public DeleteSelectedActionConsumer(IActionService actionService) => 
            _actionService = actionService ?? throw new ArgumentNullException(nameof(actionService));
        
        public async Task Consume(ConsumeContext<DeleteSelectActionRequest> context)
        {
            var result = await _actionService.DeleteSelected(context.Message.IdsAction);
            var response = result.Success
                ? new NotificationViewModel()
                : new NotificationViewModel(result.Errors, e: result.AspNetException);

            await context.RespondAsync<NotificationViewModel>(response);
        }
    }
}
