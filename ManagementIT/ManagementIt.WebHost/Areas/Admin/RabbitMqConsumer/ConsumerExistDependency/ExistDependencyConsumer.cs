using ApplicationContracts.ViewModels.Application.ExistDependency;
using Contracts.Constants;
using GreenPipes;
using ManagementIt.Core.Abstractions.Interlayer;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ConsumerExistDependency
{
    public class ExistDependencyConsumer : IConsumer<ExistDependenceEntityRequest>
    {
        private readonly IApplicationService _applicationService;

        public ExistDependencyConsumer(IApplicationService applicationService) =>
            _applicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));

        public async Task Consume(ConsumeContext<ExistDependenceEntityRequest> context)
        {
            var result = await _applicationService.ExistDependencyEntity(context.Message.RoomId, context.Message.DepartmentId, context.Message.EmployeeId);
            var response = new ExistDepandencyEntityResponse { Exists = result.Success };
            await context.RespondAsync<ExistDepandencyEntityResponse>(response);
        }
    }

    public class ExistDependencyConsumerDefinition : ConsumerDefinition<ExistDependencyConsumer>
    {
        public ExistDependencyConsumerDefinition() => EndpointName = ApiShowConstants.OnDeleteApplication;

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<ExistDependencyConsumer> consumerConfigurator) => endpointConfigurator.UseRetry(x => x.Intervals(100, 500, 1000));
    }
}
