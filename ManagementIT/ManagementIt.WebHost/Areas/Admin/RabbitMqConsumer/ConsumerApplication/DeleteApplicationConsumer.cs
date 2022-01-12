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
using ManagementIt.Core.Models.AppModels;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;

namespace ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerApplication
{
    public class DeleteApplicationConsumer : IConsumer<DeleteApplicationViewModel>
    {
        private readonly IApplicationService _applicationService;
        private readonly IMapper _mapper;

        public DeleteApplicationConsumer(IApplicationService applicationService, IMapper mapper)
        {
            _applicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<DeleteApplicationViewModel> context)
        {
            var model = _mapper.Map<DeleteApplicationModel>(context.Message);
            var result = await _applicationService.DeleteAsync(model, context.Message.IniciatorUserName);

            var response = result.Success
                ? new NotificationViewModel()
                : new NotificationViewModel(result.Errors, e: result.AspNetException);

            await context.RespondAsync<NotificationViewModel>(response);
        }
    }

    public class DeleteApplicationConsumerDefinition : ConsumerDefinition<DeleteApplicationConsumer>
    {
        public DeleteApplicationConsumerDefinition() => EndpointName = ApiShowConstants.DeleteApplication;

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<DeleteApplicationConsumer> consumerConfigurator) => endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));
    }
}