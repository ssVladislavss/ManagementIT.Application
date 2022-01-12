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
    public class DeleteTypeConsumer : IConsumer<DeleteTypeViewModel>
    {
        private readonly ITypeService _typeService;

        public DeleteTypeConsumer(ITypeService service) => _typeService = service ?? throw new ArgumentNullException(nameof(service));

        public async Task Consume(ConsumeContext<DeleteTypeViewModel> context)
        {
            var result = await _typeService.DeleteAsync(context.Message.Id);

            var response = result.Success
                ? new NotificationViewModel()
                : new NotificationViewModel(result.Errors, e: result.AspNetException);

            await context.RespondAsync<NotificationViewModel>(response);
        }
    }

    public class DeleteTypeConsumerDefinition : ConsumerDefinition<DeleteTypeConsumer>
    {
        public DeleteTypeConsumerDefinition() => EndpointName = ApiShowConstants.DeleteType;

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<DeleteTypeConsumer> consumerConfigurator) =>
            endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));

    }
}