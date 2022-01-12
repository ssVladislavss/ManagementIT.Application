using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationContracts.ViewModels.Application.ApplicationToItModels;
using AutoMapper;
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
    public class GetCreateApplicationConsumer : IConsumer<GetCreateApplicationViewModel>
    {
        private readonly IApplicationService _applicationService;
        private readonly IMapper _mapper;

        public GetCreateApplicationConsumer(IApplicationService applicationService, IMapper mapper)
        {
            _applicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<GetCreateApplicationViewModel> context)
        {
            var result = await _applicationService.GetCreateToItAsync();

            var response = new GetCreateApplicationResponse();

            if (!result.Success)
                response.Notification = new NotificationViewModel(result.Errors, e: result.AspNetException);
            else
            {
                response.Notification = new NotificationViewModel();
                response.Model = _mapper.Map<CreateApplicationToITViewModel>(result.Data);
            }

            await context.RespondAsync<GetCreateApplicationResponse>(response);
        }
    }

    public class GetCreateApplicationConsumerDefinition : ConsumerDefinition<GetCreateApplicationConsumer>
    {
        public GetCreateApplicationConsumerDefinition() => EndpointName = ApiShowConstants.GetCreateApplication;

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<GetCreateApplicationConsumer> consumerConfigurator) => endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));
    }
}