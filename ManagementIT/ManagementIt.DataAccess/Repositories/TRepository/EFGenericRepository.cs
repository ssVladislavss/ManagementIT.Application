using ManagementIt.Core.Abstractions.TEntityRepository;
using ManagementIt.Core.Domain;
using ManagementIt.Core.ResponseModels;
using ManagementIt.DataAccess.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.Enums;
using ManagementIt.Core.Abstractions.MongoAbstractions;
using ManagementIt.Core.Constants;
using ManagementIt.Core.Domain.MongoEntity;
using Microsoft.Extensions.Caching.Memory;

namespace ManagementIt.DataAccess.Repositories.TRepository
{
    public class EFGenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        protected Expression<Func<T, object>>[] Includes;
        protected readonly ILogService _service;

        public EFGenericRepository(AppDbContext context, ILogService service)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<ManagementITActionResult> AddEntityAsync(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                var result = await _context.SaveChangesAsync();
                return ManagementITActionResult.IsSuccess();
            }
            catch (Exception e)
            {
                return
                    ManagementITActionResult.Fail(new[] { TypeOfErrors.InternalServerError }, null, e: $"Ошибка добавления модели || Модель < {typeof(T)} > || ID < {entity?.Id} > || Name < {entity?.Name} > || {e.InnerException} >");
            }
        }

        public async Task<ManagementITActionResult> DeleteEntityAsync(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Deleted;
                var result = await _context.SaveChangesAsync();
                return ManagementITActionResult.IsSuccess();
            }
            catch (Exception e)
            {
                return
                    ManagementITActionResult.Fail(new[] { TypeOfErrors.DeletionEntityError }, null, $"Ошибка удаления модели || Модель < {typeof(T)} > || ID < {entity?.Id} > || Name < {entity?.Name} > || Описание: < {e.InnerException} >");
            }
        }

        public virtual bool ExistEntityByName(string name, int? Tid = null)
        {
            var response = false;
            if (Tid.HasValue)
            {
                response = _context.Set<T>().Where(x => x.Id != Tid).Any(x => x.Name == name);
            }
            else
            {
                response = _context.Set<T>().Any(x => x.Name == name);
            }
            return response;
        }

        public async Task<ManagementITActionResult<IEnumerable<T>>> GetAllEntitiesAsync()
        {
            try
            {
                var response = await _context.Set<T>().ToListAsync();
                return ManagementITActionResult<IEnumerable<T>>.IsSuccess(response);
            }
            catch (Exception e)
            {
                return ManagementITActionResult<IEnumerable<T>>.Fail(new[] { TypeOfErrors.InternalServerError }, null, null,
                    $"Ошибка при поиске списка моделей || Модель: < {typeof(T)} > || Описание < {e.InnerException} >");
            }
        }

        public virtual async Task<ManagementITActionResult<T>> GetEntityByNameAsync(string name)
        {
            try
            {
                var result = await _context.Set<T>().FirstOrDefaultAsync(x => x.Name == name);
                return ManagementITActionResult<T>.IsSuccess(result);
            }
            catch (Exception e)
            {
                return ManagementITActionResult<T>.Fail(new[] { TypeOfErrors.InternalServerError }, null, null,
                    $"Ошибка при поиске модели || Модель: < {typeof(T)} >  || Входной параметр name: < {name} > || Описание: < {e.InnerException} >");
            }
        }

        public async Task<ManagementITActionResult<T>> GetEntityByIdAsync(int id)
        {
            try
            {
                var response = await _context.Set<T>().FindAsync(id);
                return ManagementITActionResult<T>.IsSuccess(response);
            }
            catch (Exception e)
            {
                return
                    ManagementITActionResult<T>.Fail(new[] { TypeOfErrors.InternalServerError }, null, null, $"Ошибка при поиске модели || Модель: < {typeof(T)} > || Входной параметр ID: < {id} > || Описание: < {e.InnerException} >");
            }
        }

        public virtual async Task<IEnumerable<T>> GetEntitiesByNameAsync(string name)
        {
            try
            {
                var response = await _context.Set<T>().Where(x => x.Name == name).ToListAsync();
                return response;
            }
            catch (Exception e)
            {
                await _service.Create(LogMessage.GetLogMessage(ManagementItConstants.ConstantDbContext.Context,
                    $"Ошибка при поиске списка моделей || Модель: < {typeof(T)} > || Входной параметр name: < {name} > || Описание: < {e.InnerException} >",
                    NotificationType.Error));
                return null;
            }

        }

        public async Task<ManagementITActionResult> UpdateEntityAsync(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                var result = await _context.SaveChangesAsync();
                return ManagementITActionResult.IsSuccess();
            }
            catch (Exception e)
            {
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.InternalServerError }, null, $"Ошибка при изменении модели || Модель: < {typeof(T)} >  || ID < {entity.Id} > || Name < {entity.Name} > || Описание: < {e.InnerException} >");
            }
        }

        public async Task<ManagementITActionResult> DeleteRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                _context.Set<T>().RemoveRange(entities);
                var result = await _context.SaveChangesAsync();
                return ManagementITActionResult.IsSuccess();
            }
            catch (Exception e)
            {
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.DeletionEntityError }, null, $"Ошибка при удалении списка моделей || Модель: < {typeof(T)} > || Входной параметр entities: < {typeof(IEnumerable<T>)} > || Описание: < {e.InnerException} >");
            }
        }

        public async Task<ManagementITActionResult> UpdateRangeEntityAsync(IEnumerable<T> entities)
        {
            try
            {
                _context.UpdateRange(entities);
                var result = await _context.SaveChangesAsync();
                return ManagementITActionResult.IsSuccess();
            }
            catch (Exception e)
            {
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.InternalServerError }, null, $"Ошибка при изменении списка моделей || Модель: < {typeof(T)} > || Описание: < {e.InnerException} >");
            }
        }

        public async Task<ManagementITActionResult<IEnumerable<T>>> GetEntitiesByIdsAsync(List<int> ids)
        {
            try
            {
                var response = await _context.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync();
                return ManagementITActionResult<IEnumerable<T>>.IsSuccess(response);
            }
            catch (Exception e)
            {
                var log = string.Empty;
                foreach (var id in ids)
                {
                    log += id.ToString() + ", ";
                }

                return ManagementITActionResult<IEnumerable<T>>.Fail(new[] { TypeOfErrors.InternalServerError }, null, null,
                    $"Ошибка при поиске списка моделей || Модель: < {typeof(T)} > ||" +
                    $" Входной параметр: < {typeof(List<int>)} > || Ids: < {log} >" +
                    $" Описание < {e.InnerException} >");
            }
        }
    }
}
