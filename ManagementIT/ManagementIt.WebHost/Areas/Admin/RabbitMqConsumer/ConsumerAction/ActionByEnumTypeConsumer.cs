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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerAction
{
    public class ActionByEnumTypeConsumer : IConsumer<ActionByEnumTypeRequest>
    {
        private readonly IActionService _actionService;
        private readonly IMapper _mapper;

        public ActionByEnumTypeConsumer(IActionService actionService, IMapper mapper)
        {
            _actionService = actionService ?? throw new ArgumentNullException(nameof(actionService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<ActionByEnumTypeRequest> context)
        {
            var result = await _actionService.GetByActionTypeAsync(GetActionType(context.Message.NumberType));
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

    public class ActionByEnumTypeConsumerDefinition : ConsumerDefinition<ActionByEnumTypeConsumer>
    {
        public ActionByEnumTypeConsumerDefinition()
        {
            EndpointName = ApiShowConstants.GetByEnumTypeAction;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<ActionByEnumTypeConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));
        }
    }
}
