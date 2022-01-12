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
    public class UpdatePriorityConsumer : IConsumer<UpdatePriorityViewModel>
    {
        private readonly IPriorityService _priorityService;

        public UpdatePriorityConsumer(IPriorityService priorityService) =>
            _priorityService = priorityService ?? throw new ArgumentNullException(nameof(priorityService));
        
        public async Task Consume(ConsumeContext<UpdatePriorityViewModel> context)
        {
            var existNameResult = _priorityService.ExistEntityByName(context.Message.Name, context.Message.Id);
            if (!existNameResult)
            {
                var model = new ApplicationPriorityModel(context.Message.Name, context.Message.Id, isDefault: context.Message.IsDefault);
                var result = await _priorityService.UpdateAsync(model);

                var response = !result.Success
                    ? new NotificationViewModel(result.Errors, e: result.AspNetException)
                    : new NotificationViewModel();
                await context.RespondAsync<NotificationViewModel>(response);
            }
            else
                await context.RespondAsync<NotificationViewModel>(
                    new NotificationViewModel(new[] {TypeOfErrors.ExistNameEntity}));
        }
    }

    public class UpdatePriorityConsumerDefinition : ConsumerDefinition<UpdatePriorityConsumer>
    {
        public UpdatePriorityConsumerDefinition() => EndpointName = ApiShowConstants.UpdatePriority;
        
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<UpdatePriorityConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));
        }
    }
}