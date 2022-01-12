using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.Enums;
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
    public class InterlayerType : ITypeService
    {
        private readonly IGenericRepository<ApplicationType> _typeRepository;
        private readonly IMapper _mapper;

        public InterlayerType(IGenericRepository<ApplicationType> typeRepository, IMapper mapper)
        {
            _typeRepository = typeRepository ?? throw new ArgumentNullException(nameof(typeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ManagementITActionResult<IEnumerable<ApplicationTypeModel>>> GetAllAsync()
        {
            var priorities = await _typeRepository.GetAllEntitiesAsync();
            if (priorities.AspNetException != null)
                return ManagementITActionResult<IEnumerable<ApplicationTypeModel>>
                    .Fail(priorities.Errors, priorities.ErrorDescription, null, priorities.AspNetException);
            if (!priorities.Data.Any())
                return ManagementITActionResult<IEnumerable<ApplicationTypeModel>>
                    .Fail(new []{ TypeOfErrors.NoContent }, "Типов заявок нет", null, "Запрос выполнен || Данных нет");
            
            var response = _mapper.Map<IEnumerable<ApplicationTypeModel>>(priorities.Data);
            return ManagementITActionResult<IEnumerable<ApplicationTypeModel>>.IsSuccess(response);
        }

        public async Task<ManagementITActionResult<IEnumerable<ApplicationTypeModel>>> GetByName(string name)
        {
            var priorities = await _typeRepository.GetEntitiesByNameAsync(name);
            if (!priorities.Any())
            {
                //await _service.Create(LogMessage.GetLogMessage($"{ManagementItConstants.ConstantType.controller}",
                //    $"Запрос выполнен. Данных нет", NotificationType.Error, principal?.Identity?.Name));
                return ManagementITActionResult<IEnumerable<ApplicationTypeModel>>
                    .Fail(new[] { TypeOfErrors.NoContent }, $"По названию [{name}], ничего не найдено", null);
            }

            var response = _mapper.Map<IEnumerable<ApplicationTypeModel>>(priorities);
            return ManagementITActionResult<IEnumerable<ApplicationTypeModel>>.IsSuccess(response);
        }

        public async Task<ManagementITActionResult<ApplicationTypeModel>> GetByIdAsync(int id)
        {
            var result = await _typeRepository.GetEntityByIdAsync(id);
            if (result.AspNetException != null) return ManagementITActionResult<ApplicationTypeModel>.Fail(result.Errors, result.ErrorDescription, null, result.AspNetException);
            if (result.Data == null)
                return ManagementITActionResult<ApplicationTypeModel>
                    .Fail(new[] { TypeOfErrors.NotFound }, $"Запрос выполнен || Тип с ID <{id}> не найден", null);
            
            var response = _mapper.Map<ApplicationTypeModel>(result.Data);
            return ManagementITActionResult<ApplicationTypeModel>.IsSuccess(response);
        }

        public async Task<ManagementITActionResult> AddAsync(ApplicationTypeModel model)
        {
            var entity = _mapper.Map<ApplicationType>(model);
            return await _typeRepository.AddEntityAsync(entity);
        }

        public async Task<ManagementITActionResult> UpdateAsync(ApplicationTypeModel model)
        {
            var entity = await _typeRepository.GetEntityByIdAsync(model.Id);
            if (entity.AspNetException != null) return entity;

            if (entity.Data == null)
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotFound }, null, $"Ошибка при обновлении типа || данные не найдены || ID < {model.Id} >");
            
            entity.Data.Name = model.Name;
            return await _typeRepository.UpdateEntityAsync(entity.Data);
        }

        public bool ExistEntityByName(string name, int? Tid = null)
        {
            return _typeRepository.ExistEntityByName(name, Tid);
        }

        public async Task<ManagementITActionResult> DeleteAsync(int typeId)
        {
            var entity = await _typeRepository.GetEntityByIdAsync(typeId);
            if (entity.AspNetException != null) return entity;

            if (entity.Data == null)
                return ManagementITActionResult.Fail(new[] { TypeOfErrors.NotFound }, null, $"Ошибка при удалении типа || данные не найдены || ID < {typeId} >");
            
            return await _typeRepository.DeleteEntityAsync(entity.Data);
        }
    }
}
