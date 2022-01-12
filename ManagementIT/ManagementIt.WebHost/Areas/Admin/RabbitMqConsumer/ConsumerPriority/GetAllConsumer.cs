using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationContracts.ViewModels.Application.Priority;
using AutoMapper;
using Contracts.Constants;
using Contracts.Enums;
using Contracts.ResponseModels;
using GreenPipes;
using ManagementIt.Core.Abstractions.Interlayer;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;

namespace ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerPriority
{
    public class GetAllConsumer : IConsumer<ApplicationPriorityViewModel>
    {
        private readonly IPriorityService _priorityService;
        private readonly IMapper _mapper;

        public GetAllConsumer(IPriorityService priorityService, IMapper mapper)
        {
            _priorityService = priorityService ?? throw new ArgumentNullException(nameof(priorityService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task Consume(ConsumeContext<ApplicationPriorityViewModel> context)
        {
            var result = await _priorityService.GetAllAsync();

            var response = new AllPriorityResponse();
            if (!result.Success)
                response.Notification = new NotificationViewModel(result.Errors, e: result.AspNetException);
            else
            {
                response.Notification = new NotificationViewModel();
                response.priorities = _mapper.Map<IEnumerable<ApplicationPriorityViewModel>>(result.Data);
            }

            await context.RespondAsync<AllPriorityResponse>(response);
        }
    }

    public class GetAllConsumerDefinition : ConsumerDefinition<GetAllConsumer>
    {
        public GetAllConsumerDefinition()
        {
            EndpointName = ApiShowConstants.AllPriority;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<GetAllConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));
        }
    }
}