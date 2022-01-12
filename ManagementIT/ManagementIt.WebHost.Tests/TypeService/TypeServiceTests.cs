using AutoMapper;
using Contracts.Enums;
using FakeItEasy;
using ManagementIt.Core.Abstractions.Interlayer;
using ManagementIt.Core.Abstractions.TEntityRepository;
using ManagementIt.Core.Domain.ApplicationEntity;
using ManagementIt.Core.Models.AppModels;
using ManagementIt.Core.ResponseModels;
using ManagementIt.DataAccess.InterlayerRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ManagementIt.WebHost.Tests.TypeService
{
    public class TypeServiceTests
    {
        private readonly InterlayerType _typeService;
        private readonly IGenericRepository<ApplicationType> _typeRepository;
        private readonly IMapper _mapper;
        public TypeServiceTests()
        {
            _mapper = A.Fake<IMapper>();
            _typeRepository = A.Fake<IGenericRepository<ApplicationType>>();
            _typeService = new InterlayerType(_typeRepository, _mapper);
        }

        [Fact]
        public async Task InterlayerType_GetAll_Null_Returns_ManagementItActionResult_InternalServerError()
        {
            var returns = ManagementITActionResult<IEnumerable<ApplicationType>>
                .Fail(new[] { TypeOfErrors.InternalServerError }, null, null, " ");
            A.CallTo(() => _typeRepository.GetAllEntitiesAsync()).Returns(returns);

            var result = await _typeService.GetAllAsync();
            Assert.True(result.Errors?.Any(x => x == TypeOfErrors.InternalServerError));
        }

        [Fact]
        public async Task InterlayerType_GetAll_CountZero_Returns_ManagementItActionResult_NoContent()
        {
            var returns = ManagementITActionResult<IEnumerable<ApplicationType>>.Fail
                (new[] { TypeOfErrors.NoContent }, null, new List<ApplicationType>(), null);

            A.CallTo(() => _typeRepository.GetAllEntitiesAsync()).Returns(returns);

            var result = await _typeService.GetAllAsync();

            Assert.True(result.Errors?.Any(x => x == TypeOfErrors.NoContent));
        }

        [Fact]
        public async Task InterlayerType_GetAll_Success_Returns_ManagementItActionResult_IsSuccess()
        {
            var returns = ManagementITActionResult<IEnumerable<ApplicationType>>
                .IsSuccess(new List<ApplicationType> { new ApplicationType() });

            A.CallTo(() => _typeRepository.GetAllEntitiesAsync()).Returns(returns);

            var result = await _typeService.GetAllAsync();

            Assert.True(result.Success);
        }
    }
}
