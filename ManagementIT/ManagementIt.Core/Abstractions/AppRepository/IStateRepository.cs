using ManagementIt.Core.Abstractions.TEntityRepository;
using ManagementIt.Core.Domain.ApplicationEntity;
using ManagementIt.Core.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementIt.Core.Abstractions.AppRepository
{
    public interface IStateRepository : IGenericRepository<ApplicationState>
    {
        Task<ManagementITActionResult<ApplicationState>> GetStateOrDefault();
    }
}
