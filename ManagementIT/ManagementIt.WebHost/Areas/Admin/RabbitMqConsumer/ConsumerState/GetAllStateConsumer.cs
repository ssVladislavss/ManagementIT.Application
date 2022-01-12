using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationContracts.ViewModels.Application.StateModels;
using AutoMapper;
using Contracts.Constants;
using Contracts.Enums;
using Contracts.ResponseModels;
using GreenPipes;
using ManagementIt.Core.Abstractions.Interlayer;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;

namespace ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerState
{
    public class GetAllStateConsumer : IConsumer<ApplicationStateViewModel>
    {
        private readonly IStateService _stateService;
        private readonly IMapper _mapper;

        public GetAllStateConsumer(IStateService service, IMapper mapper)
        {
            _stateService = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<ApplicationStateViewModel> context)
        {
            var result = await _stateService.GetAllAsync();

            var response = new AllStateResponse();

            if (!result.Success)
                response.Notification = new NotificationViewModel(result.Errors, e: result.AspNetException);
            else
            {
                response.Model = _mapper.Map<IEnumerable<ApplicationStateViewModel>>(result.Data);
                response.Notification = new NotificationViewModel();
            }

            await context.RespondAsync<AllStateResponse>(response);
        }
    }

    public class GetAllStateConsumerDefinition : ConsumerDefinition<GetAllStateConsumer>
    {
        public GetAllStateConsumerDefinition() => EndpointName = ApiShowConstants.AllState;

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<GetAllStateConsumer> consumerConfigurator) =>
            endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));

    }
}