using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ManagementIt.Core.Models.AppModels;
using ManagementIt.Core.ResponseModels;

namespace ManagementIt.Core.Abstractions.Interlayer
{
    public interface ITypeService
    {
        Task<ManagementITActionResult<IEnumerable<ApplicationTypeModel>>> GetAllAsync();
        Task<ManagementITActionResult<IEnumerable<ApplicationTypeModel>>> GetByName(string name);
        Task<ManagementITActionResult<ApplicationTypeModel>> GetByIdAsync(int id);
        Task<ManagementITActionResult> AddAsync(ApplicationTypeModel model);
        Task<ManagementITActionResult> UpdateAsync(ApplicationTypeModel model);
        bool ExistEntityByName(string name, int? Tid = null);
        Task<ManagementITActionResult> DeleteAsync(int typeId);
    }
}