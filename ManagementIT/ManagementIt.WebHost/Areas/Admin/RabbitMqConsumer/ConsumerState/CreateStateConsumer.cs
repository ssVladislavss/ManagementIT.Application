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
using ManagementIt.Core.Models.AppModels;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;

namespace ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerState
{
    public class CreateStateConsumer : IConsumer<CreateStateViewModel>
    {
        private readonly IStateService _stateService;

        public CreateStateConsumer(IStateService service) => _stateService = service ?? throw new ArgumentNullException(nameof(service));

        public async Task Consume(ConsumeContext<CreateStateViewModel> context)
        {
            var existNameResult = _stateService.ExistEntityByName(context.Message.Name);
            if (!existNameResult)
            {
                var model = new ApplicationStateModel(context.Message.Name,
                    context.Message.BGColor, context.Message.IsDefault);
                var result = await _stateService.AddAsync(model);

                var response = result.Success
                    ? new NotificationViewModel()
                    : new NotificationViewModel(result.Errors, e: result.AspNetException);

                await context.RespondAsync<NotificationViewModel>(response);
            }
            else await context.RespondAsync<NotificationViewModel>(new NotificationViewModel(new[] {TypeOfErrors.ExistNameEntity}));
        }
    }

    public class CreateStateConsumerDefinition : ConsumerDefinition<CreateStateConsumer>
    {
        public CreateStateConsumerDefinition() => EndpointName = ApiShowConstants.CreateState;

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<CreateStateConsumer> consumerConfigurator) =>
            endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));
    }
}