using Contracts.Enums;
using ManagementIt.Core.Abstractions.TEntityRepository;
using ManagementIt.Core.Domain.ApplicationEntity;
using ManagementIt.Core.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementIt.Core.Abstractions.AppRepository
{
    public interface IApplicationActionRepository : IGenericRepository<ApplicationAction>
    {
        Task<ManagementITActionResult<IEnumerable<ApplicationAction>>> GetActionByToItIdAsync(int apptoitId);
        Task<ManagementITActionResult<ApplicationAction>> GetActionByIdAsync(int actionId, ActionType action = 0);
        Task<ManagementITActionResult<ApplicationAction>> GetActionByToItIdAndActionTypeAsync(int appId, ActionType action);
        Task<ManagementITActionResult<IEnumerable<ApplicationAction>>> GetActionByEnumTypeAsync(ActionType action);
    }
}
