using ApplicationContracts.ViewModels.Application.ActionModels;
using AutoMapper;
using Contracts.ResponseModels;
using ManagementIt.Core.Abstractions.Interlayer;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerAction
{
    public class ActionByApplicationIdConsumer : IConsumer<ActionByApplicationIdRequest>
    {
        public IActionService _actionService;
        public IMapper _mapper;

        public ActionByApplicationIdConsumer(IActionService actionService, IMapper mapper)
        {
            _actionService = actionService ?? throw new ArgumentNullException(nameof(actionService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<ActionByApplicationIdRequest> context)
        {
            var result = await _actionService.GetByToItIdAsync(context.Message.ApplicationId);
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
}
