using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationContracts.ViewModels.Application.TypeModels;
using Contracts.Constants;
using Contracts.Enums;
using Contracts.ResponseModels;
using GreenPipes;
using ManagementIt.Core.Abstractions.Interlayer;
using ManagementIt.Core.Models.AppModels;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;

namespace ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerType
{
    public class UpdateTypeConsumer : IConsumer<UpdateTypeViewModel>
    {
        private readonly ITypeService _typeService;

        public UpdateTypeConsumer(ITypeService service) => _typeService = service ?? throw new ArgumentNullException(nameof(service));

        public async Task Consume(ConsumeContext<UpdateTypeViewModel> context)
        {
            var existNameResult = _typeService.ExistEntityByName(context.Message.Name, context.Message.Id);
            if (!existNameResult)
            {
                var model = new ApplicationTypeModel(context.Message.Name, context.Message.Id);
                var result = await _typeService.UpdateAsync(model);

                var response = result.Success
                    ? new NotificationViewModel()
                    : new NotificationViewModel(result.Errors, e: result.AspNetException);

                await context.RespondAsync<NotificationViewModel>(response);
            }
            else await context.RespondAsync<NotificationViewModel>(new NotificationViewModel(new[] {TypeOfErrors.ExistNameEntity}));

        }
    }

    public class UpdateTypeConsumerDefinition : ConsumerDefinition<UpdateTypeConsumer>
    {
        public UpdateTypeConsumerDefinition() => EndpointName = ApiShowConstants.UpdateType;

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<UpdateTypeConsumer> consumerConfigurator) =>
            endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));

    }
}