using ApplicationContracts.ViewModels.Application.ApplicationToItModels;
using AutoMapper;
using Contracts.ResponseModels;
using ManagementIt.Core.Abstractions.Interlayer;
using ManagementIt.Core.Models.AppModels;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerApplication
{
    public class UpdateEmployeeOrApplicationConsumer : IConsumer<UpdateEmployeeOrApplicationRequest>
    {
        private readonly IApplicationService _applicationService;
        private readonly IMapper _mapper;

        public UpdateEmployeeOrApplicationConsumer(IApplicationService applicationService, IMapper mapper)
        {
            _applicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<UpdateEmployeeOrApplicationRequest> context)
        {
            var model = _mapper.Map<EditEmployeeModel>(context.Message);
            var result = await _applicationService.UpdateEmployeeAsync(model, context.Message.Iniciator);

            var response = result.Success 
                ? new NotificationViewModel() 
                : new NotificationViewModel(result.Errors, e: result.AspNetException);
            await context.RespondAsync<NotificationViewModel>(response);
        }
    }
}
