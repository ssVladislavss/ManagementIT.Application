using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GetAllApplicationConsumer : IConsumer<ApplicationToItViewModel>
    {
        private readonly IApplicationService _applicationService;
        private readonly IMapper _mapper;

        public GetAllApplicationConsumer(IApplicationService service, IMapper mapper)
        {
            _applicationService = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<ApplicationToItViewModel> context)
        {
            var result = await _applicationService.GetAllAsync();

            var response = new AllApplicationResponse();
            if (!result.Success)
                response.Notification = new NotificationViewModel(result.Errors, e: result.AspNetException);
            else
            {
                response.Notification = new NotificationViewModel();
                response.Model = _mapper.Map<IEnumerable<ApplicationToItViewModel>>(result.Data);
            }

            await context.RespondAsync<AllApplicationResponse>(response);
        }
    }

    public class GetAllApplicationConsumerDefinition : ConsumerDefinition<GetAllApplicationConsumer>
    {
        public GetAllApplicationConsumerDefinition()
        {
            EndpointName = ApiShowConstants.AllApplication;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<GetAllApplicationConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));
        }
    }
}