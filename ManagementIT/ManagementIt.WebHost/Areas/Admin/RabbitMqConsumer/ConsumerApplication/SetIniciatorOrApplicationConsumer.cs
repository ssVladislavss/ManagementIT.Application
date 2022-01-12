using ApplicationContracts.ViewModels.Application.ApplicationToItModels;
using AutoMapper;
using Contracts.ResponseModels;
using ManagementIt.Core.Abstractions.Interlayer;
using ManagementIt.Core.Models.AppModels;
using MassTransit;
using System.Threading.Tasks;

namespace ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerApplication
{
    public class SetIniciatorOrApplicationConsumer : IConsumer<SetIniciatorOrApplicationRequest>
    {
        private readonly IApplicationService _applicationService;
        private readonly IMapper _mapper;

        public SetIniciatorOrApplicationConsumer(IApplicationService applicationService, IMapper mapper)
        {
            _applicationService = applicationService ?? throw new System.ArgumentNullException(nameof(applicationService));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<SetIniciatorOrApplicationRequest> context)
        {
            var model = _mapper.Map<SetIniciatorOrApplicationModel>(context.Message);
            var result = await _applicationService.SetIniciatorAsync(model);

            var response = result.Success 
                ? new NotificationViewModel() 
                : new NotificationViewModel(result.Errors, e: result.AspNetException);
            await context.RespondAsync<NotificationViewModel>(response);
        }
    }
}
