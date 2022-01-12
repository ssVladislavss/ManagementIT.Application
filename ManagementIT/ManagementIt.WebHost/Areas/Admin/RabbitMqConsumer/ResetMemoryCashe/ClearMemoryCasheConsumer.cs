using MassTransit;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using ApplicationContracts.ViewModels.Cashe;
using System.Collections.Generic;
using System.Collections;

namespace ManagementIt.WebHost.Areas.Admin.RabbitMqConsumer.ResetMemoryCashe
{
    public class ClearMemoryCasheConsumer : IConsumer<ClearMemoryCasheRequest>
    {
        private readonly IMemoryCache _cashe;

        public ClearMemoryCasheConsumer(IMemoryCache cashe) => _cashe = cashe ?? throw new ArgumentNullException(nameof(cashe));
        
        public Task Consume(ConsumeContext<ClearMemoryCasheRequest> context)
        {
            return Task.CompletedTask;
        }
    }
}
