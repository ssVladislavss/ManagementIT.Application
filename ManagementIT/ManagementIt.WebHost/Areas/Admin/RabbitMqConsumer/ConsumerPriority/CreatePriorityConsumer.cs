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
    public class CreatePriorityConsumer : IConsumer<CreateOrEditApplicationPriorityViewModel>
    {
        private readonly IPriorityService _priorityService;

        public CreatePriorityConsumer(IPriorityService priorityService) =>
            _priorityService = priorityService ?? throw new ArgumentNullException(nameof(priorityService));
        
        public async Task Consume(ConsumeContext<CreateOrEditApplicationPriorityViewModel> context)
        {
            var existNameResult = _priorityService.ExistEntityByName(context.Message.Name);
            NotificationViewModel response = null;
            if (!existNameResult)
            {
                var model = new ApplicationPriorityModel(name: context.Message.Name, isDefault: context.Message.IsDefault);
                var result = await _priorityService.AddAsync(model);

                if (!result.Success)
                    response = new NotificationViewModel(result.Errors, e: result.AspNetException);
                response = new NotificationViewModel();
                await context.RespondAsync<NotificationViewModel>(response);
            }
            else
                await context.RespondAsync<NotificationViewModel>(new NotificationViewModel(new[] {TypeOfErrors.ExistNameEntity}));
        }
    }

    public class PriorityConsumerDefinition : ConsumerDefinition<CreatePriorityConsumer>
    {
        public PriorityConsumerDefinition()
        {
            EndpointName = ApiShowConstants.CreatePriority;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<CreatePriorityConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));
        }
    }
}