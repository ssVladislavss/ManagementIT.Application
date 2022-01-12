using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationContracts.ViewModels.Application.StateModels;
using AutoMapper;
using Contracts.Constants;
using Contracts.Enums;
using Contracts.ResponseModels;
using Contracts.ViewModels.Application.StateModels;
using GreenPipes;
using ManagementIt.Core.Abstractions.Interlayer;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;

namespace ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerState
{
    public class GetByIdStateConsumer : IConsumer<GetStateByIdRequest>
    {
        private readonly IStateService _stateService;
        private readonly IMapper _mapper;

        public GetByIdStateConsumer(IStateService service, IMapper mapper)
        {
            _stateService = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<GetStateByIdRequest> context)
        {
            var result = await _stateService.GetByIdAsync(context.Message.Id);

            var response = new GetStateByIdResponse();

            if (!result.Success)
                response.Notification = new NotificationViewModel(result.Errors, e: result.AspNetException);
            else
            {
                response.Model = _mapper.Map<ApplicationStateViewModel>(result.Data);
                response.Notification = new NotificationViewModel();
            }

            await context.RespondAsync<GetStateByIdResponse>(response);
        }
    }

    public class GetByIdStateConsumerDefinition : ConsumerDefinition<GetByIdStateConsumer>
    {
        public GetByIdStateConsumerDefinition() => EndpointName = ApiShowConstants.ByIdState;

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<GetByIdStateConsumer> consumerConfigurator) =>
            endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));
    }
}