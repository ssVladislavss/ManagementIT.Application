using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ActionByIdConsumer : IConsumer<ActionByIdRequest>
    {
        private readonly IActionService _actionService;
        private readonly IMapper _mapper;

        public ActionByIdConsumer(IActionService actionService, IMapper mapper)
        {
            _actionService = actionService ?? throw new ArgumentNullException(nameof(actionService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<ActionByIdRequest> context)
        {
            var result = await _actionService.GetByIdAsync(context.Message.ActionId, GetActionType(context.Message.NumberType));

            var response = new ActionByIdResponse();
            if (!result.Success)
                response.Notification = new NotificationViewModel(result.Errors, e: result.AspNetException);
            else
            {
                response.Notification = new NotificationViewModel();
                response.Model = _mapper.Map<ActionViewModel>(result.Data);
            }

            await context.RespondAsync<ActionByIdResponse>(response);
        }

        public ActionType GetActionType(int numberType)
        {
            return numberType switch
            {
                1 => ActionType.Creation,
                2 => ActionType.Change,
                3 => ActionType.StateChange,
                4 => ActionType.Deletion,
                5 => ActionType.ChangeType,
                6 => ActionType.ChangePriority,
                7 => ActionType.ChangeRoom,
                8 => ActionType.ChangeDepartment,
                9 => ActionType.ChangeEmployee,
                10 => ActionType.AddingArchive,
                _ => 0,
            };
        }
    }

    public class ActionByIdConsumerDefinition : ConsumerDefinition<ActionByIdConsumer>
    {
        public ActionByIdConsumerDefinition()
        {
            EndpointName = ApiShowConstants.GetByIdAction;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<ActionByIdConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));
        }
    }
}