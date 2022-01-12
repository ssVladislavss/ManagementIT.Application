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
    public class GetUpdateApplicationConsumer : IConsumer<GetUpdateApplicationRequest>
    {
        private readonly IApplicationService _applicationService;
        private readonly IMapper _mapper;

        public GetUpdateApplicationConsumer(IApplicationService applicationService, IMapper mapper)
        {
            _applicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<GetUpdateApplicationRequest> context)
        {
            var result = await _applicationService.GetUpdateToItAsync(context.Message.ApplicationId);

            var response = new GetUpdateApplicationResponse();
            if (!result.Success)
                response.Notification = new NotificationViewModel(result.Errors, e: result.AspNetException);
            else
            {
                response.Notification = new NotificationViewModel();
                response.Model = _mapper.Map<UpdateApplicationViewModel>(result.Data);
            }

            await context.RespondAsync<GetUpdateApplicationResponse>(response);
        }
    }

    public class GetUpdateApplicationConsumerDefinition : ConsumerDefinition<GetUpdateApplicationConsumer>
    {
        public GetUpdateApplicationConsumerDefinition() => EndpointName = ApiShowConstants.GetUpdateApplication;

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<GetUpdateApplicationConsumer> consumerConfigurator) => endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));
    }
}