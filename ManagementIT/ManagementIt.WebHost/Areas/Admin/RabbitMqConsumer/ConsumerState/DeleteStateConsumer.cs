using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationContracts.ViewModels.Application.StateModels;
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
    public class DeleteStateConsumer : IConsumer<DeleteStateViewModel>
    {
        private readonly IStateService _stateService;

        public DeleteStateConsumer(IStateService service) => _stateService = service ?? throw new ArgumentNullException(nameof(service));

        public async Task Consume(ConsumeContext<DeleteStateViewModel> context)
        {
            var result = await _stateService.DeleteAsync(context.Message.Id);

            var response = result.Success
                ? new NotificationViewModel()
                : new NotificationViewModel(result.Errors, e: result.AspNetException);

            await context.RespondAsync<NotificationViewModel>(response);
        }
    }

    public class DeleteStateConsumerDefinition : ConsumerDefinition<DeleteStateConsumer>
    {
        public DeleteStateConsumerDefinition() => EndpointName = ApiShowConstants.DeleteState;

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<DeleteStateConsumer> consumerConfigurator) =>
            endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));
    }
}