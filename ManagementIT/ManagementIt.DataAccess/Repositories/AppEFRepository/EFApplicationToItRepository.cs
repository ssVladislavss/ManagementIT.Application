using ManagementIt.Core.Abstractions.AppRepository;
using ManagementIt.Core.Domain.ApplicationEntity;
using ManagementIt.Core.ResponseModels;
using ManagementIt.DataAccess.DataBase;
using ManagementIt.DataAccess.Repositories.TRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ManagementIt.Core.Abstractions.MongoAbstractions;
using ManagementIt.Core.Constants;
using ManagementIt.Core.Domain.MongoEntity;
using Contracts.Enums;
using Microsoft.Extensions.Caching.Memory;

namespace ManagementIt.DataAccess.Repositories.AppEFRepository
{
    public class EFApplicationToItRepository : EFGenericRepository<ApplicationToIt>, IApplicationTOITRepository
    {

        public EFApplicationToItRepository(AppDbContext context, ILogService service) : base(context, service)
        {
            Includes = new Expression<Func<ApplicationToIt, object>>[]
            {
                dependency => dependency.Type,
                dependency => dependency.Priority,
                dependency => dependency.State,
            };
        }

        public async Task<ManagementITActionResult<IEnumerable<ApplicationToIt>>> GetAllApplicationToItAsync()
        {
            try
            {
                IQueryable<ApplicationToIt> set = _context.ApplicationsToIt;
                if (Includes != null)
                {
                    set = Includes.Aggregate(set, (current, includeProp) => current.Include(includeProp));
                }

                var response = await set.ToListAsync();
                return ManagementITActionResult<IEnumerable<ApplicationToIt>>.IsSuccess(response);
            }
            catch (Exception e)
            {
                return ManagementITActionResult<IEnumerable<ApplicationToIt>>
                    .Fail(new[] { TypeOfErrors.InternalServerError}, null, null,
                    $"Ошибка при поиске списка всех заявок в базе данных || < {typeof(ApplicationToIt)} > || {e.InnerException} >");
            }
        }

        public async Task<ManagementITActionResult<IEnumerable<ApplicationToIt>>> GetAppByApplicationTypeIdAsync(int typeId)
        {
            try
            {
                IQueryable<ApplicationToIt> set = _context.ApplicationsToIt;
                if (Includes != null)
                {
                    set = Includes.Aggregate(set, (current, includeProp) => current.Include(includeProp));
                }

                var response = await set.Where(x => x.Type.Id == typeId).ToListAsync();
                return ManagementITActionResult<IEnumerable<ApplicationToIt>>.IsSuccess(response);
            }
            catch (Exception e)
            {
                return ManagementITActionResult<IEnumerable<ApplicationToIt>>
                    .Fail(new[] { TypeOfErrors.InternalServerError}, null, null,
                    $"Ошибка при поиске списка заявок по типу || TypeID < {typeId} > || < {typeof(ApplicationToIt)} > || {e.InnerException} >");
            }
        }

        public async Task<ManagementITActionResult<IEnumerable<ApplicationToIt>>> GetToItForOnDeleteAsync()
        {
            try
            {
                IQueryable<ApplicationToIt> set = _context.ApplicationsToIt;
                if (Includes != null)
                {
                    set = Includes.Aggregate(set, (current, includeProp) => current.Include(includeProp));
                }
                var response = await set.Where(x => x.OnDelete == true).ToListAsync();
                return ManagementITActionResult<IEnumerable<ApplicationToIt>>.IsSuccess(response);
            }
            catch (Exception e)
            {
                return ManagementITActionResult<IEnumerable<ApplicationToIt>>
                    .Fail(new[] { TypeOfErrors.InternalServerError}, null, null,
                    $"Ошибка при поиске списка всех заявок стоящих на удалении || < {typeof(ApplicationToIt)} > || {e.InnerException} >");
            }
        }

        public async Task<ManagementITActionResult<IEnumerable<ApplicationToIt>>> GetAppByDepartamentIdAsync(int depId)
        {
            try
            {
                IQueryable<ApplicationToIt> set = _context.ApplicationsToIt;
                if (Includes != null)
                {
                    set = Includes.Aggregate(set, (current, includeProp) => current.Include(includeProp));
                }
                var response = await set.Where(x => x.DepartamentId == depId).ToListAsync();
                return ManagementITActionResult<IEnumerable<ApplicationToIt>>.IsSuccess(response);
            }
            catch (Exception e)
            {
                return ManagementITActionResult<IEnumerable<ApplicationToIt>>
                    .Fail(new[] { TypeOfErrors.InternalServerError}, null, null,
                    $"Ошибка при поиске списка заявок департамента || DepartamentID < {depId} > || < {typeof(ApplicationToIt)} > || {e.InnerException} >");
            }
        }

        public async Task<ManagementITActionResult<ApplicationToIt>> GetApplicationToItByIdAsync(int applicationToItId)
        {
            try
            {
                IQueryable<ApplicationToIt> set = _context.ApplicationsToIt;
                if (Includes != null)
                {
                    set = Includes.Aggregate(set, (current, includeProp) => current.Include(includeProp));
                }
                var response = await set.FirstOrDefaultAsync(x => x.Id == applicationToItId);

                return ManagementITActionResult<ApplicationToIt>.IsSuccess(response);
            }
            catch (Exception e)
            {
                return ManagementITActionResult<ApplicationToIt>
                    .Fail(new[] { TypeOfErrors.InternalServerError}, null, null,
                    $"Ошибка при поиске заявки || ID < {applicationToItId} > || < {typeof(ApplicationToIt)} > || {e.InnerException} >");
            }
        }

        public async Task<ManagementITActionResult> ExistDependencyEntity(int roomId = 0, int departmentId = 0, int employeeId = 0)
        {
            var result = false;
            if (roomId != 0) result = _context.ApplicationsToIt.Any(x => x.RoomId == roomId && x.OnDelete == false);
            else if(departmentId != 0) result = _context.ApplicationsToIt.Any(x => x.DepartamentId == departmentId && x.OnDelete == false);
            else result = _context.ApplicationsToIt.Any(x => x.EmployeeId == employeeId && x.OnDelete == false);
            if (result)
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.DeletionEntityError }, null);
            return ManagementITActionResult.IsSuccess();
        }

        public async Task<ManagementITActionResult<IEnumerable<ApplicationToIt>>> GetAppByEmployeeIdAsync(int employeeId)
        {
            try
            {
                IQueryable<ApplicationToIt> set = _context.ApplicationsToIt;
                if (Includes != null)
                {
                    set = Includes.Aggregate(set, (current, includeProp) => current.Include(includeProp));
                }
                var response = await set.Where(x => x.EmployeeId == employeeId).ToListAsync();

                return ManagementITActionResult<IEnumerable<ApplicationToIt>>.IsSuccess(response);
            }
            catch (Exception e)
            {
                return ManagementITActionResult<IEnumerable<ApplicationToIt>>
                    .Fail(new[] { TypeOfErrors.InternalServerError }, null, null,
                    $"Ошибка при поиске заявок по ID сотрудника || ID < {employeeId} > || < {typeof(ApplicationToIt)} > || {e.InnerException} >");
            }
        }

        public async Task<ManagementITActionResult<IEnumerable<ApplicationToIt>>> GetAppByRoomIdAsync(int roomId)
        {
            try
            {
                IQueryable<ApplicationToIt> set = _context.ApplicationsToIt;
                if (Includes != null)
                {
                    set = Includes.Aggregate(set, (current, includeProp) => current.Include(includeProp));
                }

                var response = await set.Where(x => x.RoomId == roomId).ToListAsync();
                return ManagementITActionResult<IEnumerable<ApplicationToIt>>.IsSuccess(response);
            }
            catch (Exception e)
            {
                return ManagementITActionResult<IEnumerable<ApplicationToIt>>
                    .Fail(new[] { TypeOfErrors.InternalServerError }, null, null,
                    $"Ошибка при поиске заявок по ID помещения || ID < {roomId} > || < {typeof(ApplicationToIt)} > || {e.InnerException} >");
            }
        }

        public async Task<ManagementITActionResult<IEnumerable<ApplicationToIt>>> GetAppByIniciatorIdAsync(int iniciatorId)
        {
            try
            {
                IQueryable<ApplicationToIt> set = _context.ApplicationsToIt;
                if (Includes != null)
                {
                    set = Includes.Aggregate(set, (current, includeProp) => current.Include(includeProp));
                }
                var response = await set.Where(x => x.IniciatorId == iniciatorId).ToListAsync();

                return ManagementITActionResult<IEnumerable<ApplicationToIt>>.IsSuccess(response);
            }
            catch (Exception e)
            {
                return ManagementITActionResult<IEnumerable<ApplicationToIt>>
                    .Fail(new[] { TypeOfErrors.InternalServerError }, null, null,
                    $"Ошибка при поиске заявок по ID инициатора создания || ID < {iniciatorId} > || < {typeof(ApplicationToIt)} > || {e.InnerException} >");
            }
        }
    }
}
