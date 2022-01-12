using ManagementIt.Core.Domain;
using ManagementIt.Core.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementIt.Core.Abstractions.TEntityRepository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<ManagementITActionResult<IEnumerable<T>>> GetAllEntitiesAsync();
        Task<ManagementITActionResult<IEnumerable<T>>> GetEntitiesByIdsAsync(List<int> ids);
        Task<IEnumerable<T>> GetEntitiesByNameAsync(string name);
        Task<ManagementITActionResult<T>> GetEntityByNameAsync(string name);
        Task<ManagementITActionResult<T>> GetEntityByIdAsync(int id);
        Task<ManagementITActionResult> AddEntityAsync(T entity);
        Task<ManagementITActionResult> UpdateEntityAsync(T entity);
        Task<ManagementITActionResult> UpdateRangeEntityAsync(IEnumerable<T> entities);
        Task<ManagementITActionResult> DeleteEntityAsync(T entity);
        Task<ManagementITActionResult> DeleteRangeAsync(IEnumerable<T> entities);

        bool ExistEntityByName(string name, int? Tid = null);
    }
}
