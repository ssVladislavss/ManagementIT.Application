using ApplicationContracts.ViewModels.Application.ApplicationToItModels;
using AutoMapper;
using Contracts.ResponseModels;
using ManagementIt.Core.Abstractions.Interlayer;
using ManagementIt.Core.Models.AppModels;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerApplication
{
    public class UpdatePriorityOrApplicationConsumer : IConsumer<EditPriorityOrApplicationViewModel>
    {
        private readonly IApplicationService _applicationService;
        private readonly IMapper _mapper;

        public UpdatePriorityOrApplicationConsumer(IApplicationService applicationService, IMapper mapper)
        {
            _applicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<EditPriorityOrApplicationViewModel> context)
        {
            var model = _mapper.Map<EditPriorityModel>(context.Message);
            var result = await _applicationService.UpdatePriorityAsync(model, context.Message.IniciatorUserName);

            var response = result.Success
                ? new NotificationViewModel()
                : new NotificationViewModel(result.Errors, e: result.AspNetException);

            await context.RespondAsync<NotificationViewModel>(response);
        }
    }
}
