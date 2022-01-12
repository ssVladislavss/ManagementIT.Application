using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.Enums;
using ManagementIt.Core.Abstractions.AppRepository;
using ManagementIt.Core.Abstractions.Interlayer;
using ManagementIt.Core.Abstractions.MongoAbstractions;
using ManagementIt.Core.Abstractions.TEntityRepository;
using ManagementIt.Core.Constants;
using ManagementIt.Core.Domain.ApplicationEntity;
using ManagementIt.Core.Domain.MongoEntity;
using ManagementIt.Core.Models.AppModels;
using ManagementIt.Core.ResponseModels;

namespace ManagementIt.DataAccess.InterlayerRepositories
{
    public class InterlayerPriority : IPriorityService
    {
        private readonly IPriorityRepository _appPriorRep;
        private readonly IMapper _mapper;
        private readonly ILogService _service;

        public InterlayerPriority(IPriorityRepository appPriorRep, IMapper mapper, ILogService service)
        {
            _appPriorRep = appPriorRep ?? throw new ArgumentNullException(nameof(appPriorRep));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public virtual async Task<ManagementITActionResult<IEnumerable<ApplicationPriorityModel>>> GetAllAsync()
        {
            var priorities = await _appPriorRep.GetAllEntitiesAsync();
            if (priorities.AspNetException != null)
            {
                return ManagementITActionResult<IEnumerable<ApplicationPriorityModel>>
                    .Fail(new[] { TypeOfErrors.NoContent }, "Приоритетов заявок нет", null, priorities.AspNetException);
            }
            if (!priorities.Data.Any())
            {
                return ManagementITActionResult<IEnumerable<ApplicationPriorityModel>>
                    .Fail(new [] {TypeOfErrors.NoContent }, "Приоритетов заявок нет", null, e: "Запрос выполнен. Данных нет");
            }

            var response = _mapper.Map<IEnumerable<ApplicationPriorityModel>>(priorities.Data);
            return ManagementITActionResult<IEnumerable<ApplicationPriorityModel>>.IsSuccess(response);
        }

        public virtual async Task<ManagementITActionResult<IEnumerable<ApplicationPriorityModel>>> GetByName(string name)
        {
            var priorities = await _appPriorRep.GetEntitiesByNameAsync(name);
            if (!priorities.Any())
            {
                //await _service.Create(LogMessage.GetLogMessage($"{ManagementItConstants.ConstantPriority.controller}",
                //    $"Запрос выполнен. Данных нет", NotificationType.Error, principal?.Identity?.Name));

                return ManagementITActionResult<IEnumerable<ApplicationPriorityModel>>
                    .Fail(new[] { TypeOfErrors.NoContent }, $"По названию [{name}], ничего не найдено", null, "Запрос выполнен. Данных нет");
            }

            var response = _mapper.Map<IEnumerable<ApplicationPriorityModel>>(priorities);
            return ManagementITActionResult<IEnumerable<ApplicationPriorityModel>>.IsSuccess(response);
        }

        public virtual async Task<ManagementITActionResult<ApplicationPriorityModel>> GetByIdAsync(int id)
        {
            var result = await _appPriorRep.GetEntityByIdAsync(id);
            if (result.AspNetException != null)
                return ManagementITActionResult<ApplicationPriorityModel>.Fail(result.Errors, result.ErrorDescription, null, result.AspNetException);
            if (result.Data == null)
                return ManagementITActionResult<ApplicationPriorityModel>.Fail
                    (new[] { TypeOfErrors.NotFound }, null, null, $"Приоритет не найден || ID < {id} >");

            var response = _mapper.Map<ApplicationPriorityModel>(result.Data);
            return ManagementITActionResult<ApplicationPriorityModel>.IsSuccess(response);
        }

        public virtual async Task<ManagementITActionResult> AddAsync(ApplicationPriorityModel model)
        {
            var entity = _mapper.Map<ApplicationPriority>(model);
            if (entity.IsDefault)
            {
                var exist = await _appPriorRep.GetDefaultPriority();
                if (exist.Data != null)
                {
                    exist.Data.IsDefault = false;
                    var result = await _appPriorRep.UpdateEntityAsync(exist.Data);
                    if (result.AspNetException != null) return result;
                    else if(!result.Success)
                        return ManagementITActionResult.Fail(new[] { TypeOfErrors.DeletionEntityError }, null,
                            $"Запрос на создание дефолтного приоритета || Ошибка при изменении имеющегося приоритета по умолчанию || " +
                            $"ID имеющегося приоритета по умолчанию: < {exist.Data.Id} > , Name: < {exist.Data.Name} > || " +
                            $"Входной параметр Name: < {model.Name} >");
                }  
            }
            return await _appPriorRep.AddEntityAsync(entity);
        }

        public virtual async Task<ManagementITActionResult> UpdateAsync(ApplicationPriorityModel model)
        {
            var entity = await _appPriorRep.GetEntityByIdAsync(model.Id);
            if (entity.AspNetException != null) return entity;
            if (entity.Data == null) return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotFound }, null,
                 $"Запрос на изменение данных приоритета || Данные не найдены || ID: < {model.Id} > || Name: < {model.Name} >");
            if (model.IsDefault)
            {
                var exist = await _appPriorRep.GetDefaultPriority();
                if (exist.AspNetException != null) return exist;
                else if(exist.Data != null)
                {
                    exist.Data.IsDefault = false;
                    var result = await _appPriorRep.UpdateEntityAsync(exist.Data);
                    if (result.AspNetException != null) return result;
                    else if (!result.Success)
                        return ManagementITActionResult.Fail(new[] { TypeOfErrors.UpdateEntityError }, null,
                            $"Запрос на изменение дефолтного приоритета || Ошибка при изменении имеющегося приоритета по умолчанию || " +
                            $"ID имеющегося приоритета по умолчанию: < {exist.Data.Id} > , Name: < {exist.Data.Name} > || " +
                            $"Входной параметр Name: < {model.Name} >");
                }
            }
            entity.Data.Name = model.Name;
            if(entity.Data.Id == model.Id) entity.Data.IsDefault = model.IsDefault;
            return await _appPriorRep.UpdateEntityAsync(entity.Data);
        }

        public virtual bool ExistEntityByName(string name, int? Tid = null) => _appPriorRep.ExistEntityByName(name, Tid);
        
        public virtual async Task<ManagementITActionResult> DeleteAsync(int priorityId)
        {
            if (priorityId == 1 || priorityId == 2 || priorityId == 3) return ManagementITActionResult
                     .Fail(new[] { TypeOfErrors.DeletionEntityError }, null,
                     $"Запрос на удаление приоритета || Модель: < {typeof(ApplicationPriority)} > || ID: < {priorityId} > || Вы не можете удалить этот приоритет, он является фиксированным");
            var entity = await _appPriorRep.GetEntityByIdAsync(priorityId);
            
            if (entity.AspNetException != null) return entity;
            if (entity.Data == null) return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotFound }, null,
                $"Запрос на удалении модели || Модель не найдена || Модель < {typeof(ApplicationPriority)} > || Входной параметр ID: < {priorityId} >");
            if(entity.Data.IsDefault) return ManagementITActionResult.Fail(new[] { TypeOfErrors.DeletionEntityError }, null,
                $"Запрос на удалении модели || Нельзя удалить приоритет по умолчанию || Модель < {typeof(ApplicationPriority)} > || Входной параметр ID: < {priorityId} >");

            return await _appPriorRep.DeleteEntityAsync(entity.Data);
        }
    }
}
