using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationContracts.ViewModels.Application.Priority;
using Contracts.Constants;
using Contracts.Enums;
using Contracts.ResponseModels;
using GreenPipes;
using ManagementIt.Core.Abstractions.Interlayer;
using ManagementIt.Core.Models.AppModels;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;

namespace ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerPriority
{
    public class DeletePriorityConsumer : IConsumer<DeletePriorityViewModel>
    {
        private readonly IPriorityService _priorityService;

        public DeletePriorityConsumer(IPriorityService priorityService) => _priorityService = priorityService ?? throw new ArgumentNullException(nameof(priorityService));
        

        public async Task Consume(ConsumeContext<DeletePriorityViewModel> context)
        {
            var result = await _priorityService.DeleteAsync(context.Message.Id);

            var response = !result.Success ? new NotificationViewModel(result.Errors, e: result.AspNetException) : new NotificationViewModel();
            await context.RespondAsync<NotificationViewModel>(response);
        }
    }

    public class DeletePriorityConsumerDefinition : ConsumerDefinition<DeletePriorityConsumer>
    {
        public DeletePriorityConsumerDefinition() => EndpointName = ApiShowConstants.DeletePriority;

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<DeletePriorityConsumer> consumerConfigurator) => endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));
    }
}