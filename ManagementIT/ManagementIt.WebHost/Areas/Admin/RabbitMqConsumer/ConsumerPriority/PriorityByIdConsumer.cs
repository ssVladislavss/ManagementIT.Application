using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationContracts.ViewModels.Application.Priority;
using AutoMapper;
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
    public class PriorityByIdConsumer : IConsumer<PriorityByIdRequest>
    {
        private readonly IPriorityService _priorityService;
        private readonly IMapper _mapper;

        public PriorityByIdConsumer(IPriorityService priorityService, IMapper mapper)
        {
            _priorityService = priorityService ?? throw new ArgumentNullException(nameof(priorityService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<PriorityByIdRequest> context)
        {
            var result = await _priorityService.GetByIdAsync(context.Message.Id);
            var response = new PriorityByIdResponse();
            if (!result.Success)
                response.Notification = new NotificationViewModel(result.Errors, e: result.AspNetException);
            else
            {
                response.model = _mapper.Map<ApplicationPriorityViewModel>(result.Data);
                response.Notification = new NotificationViewModel();
            }
            await context.RespondAsync<PriorityByIdResponse>(response);
        }
    }

    public class PriorityByIdConsumerDefinition : ConsumerDefinition<PriorityByIdConsumer>
    {
        public PriorityByIdConsumerDefinition()
        {
            EndpointName = ApiShowConstants.ByIdPriority;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<PriorityByIdConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));
        }
    }
}