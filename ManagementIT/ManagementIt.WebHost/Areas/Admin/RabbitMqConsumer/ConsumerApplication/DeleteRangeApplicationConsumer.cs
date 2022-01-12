using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationContracts.ViewModels.Application.ApplicationToItModels;
using Contracts.Constants;
using Contracts.Enums;
using Contracts.ResponseModels;
using GreenPipes;
using ManagementIt.Core.Abstractions.Interlayer;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;

namespace ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerApplication
{
    public class DeleteRangeApplicationConsumer : IConsumer<DeleteRange>
    {
        private readonly IApplicationService _applicationService;

        public DeleteRangeApplicationConsumer(IApplicationService service) => _applicationService = service ?? throw new ArgumentNullException(nameof(service));
        public async Task Consume(ConsumeContext<DeleteRange> context)
        {
            var result = await _applicationService.DeleteRangeToItByOnDeleteAsync();

            var response = result.Success
                ? new NotificationViewModel()
                : new NotificationViewModel(result.Errors, e: result.AspNetException);

            await context.RespondAsync<NotificationViewModel>(response);
        }
    }

    public class DeleteRangeApplicationConsumerDefinition : ConsumerDefinition<DeleteRangeApplicationConsumer>
    {
        public DeleteRangeApplicationConsumerDefinition() => EndpointName = ApiShowConstants.DeleteRangeApplication;

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<DeleteRangeApplicationConsumer> consumerConfigurator) => endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));
    }
}