using ApplicationContracts.ViewModels.Application.ApplicationToItModels;
using Contracts.ResponseModels;
using ManagementIt.Core.Abstractions.Interlayer;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerApplication
{
    public class UpdateRoomNameConsumer : IConsumer<UpdateRoomNameRequest>
    {
        private readonly IApplicationService _applicationService;

        public UpdateRoomNameConsumer(IApplicationService applicationService) =>
            _applicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
        
        public async Task Consume(ConsumeContext<UpdateRoomNameRequest> context)
        {
            var result = await _applicationService.UpdateRoomNameAsync(context.Message.RoomId, context.Message.RoomName);
            var response = result.Success
                ? new NotificationViewModel()
                : new NotificationViewModel(result.Errors, e: result.AspNetException);
            await context.RespondAsync<NotificationViewModel>(response);
        }
    }
}
