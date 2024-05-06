using Application.IRepositoryRead;
using Application.Mapping;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test.UseCases.Customers.Query
{
    public class QueryUseCaseTestBase<TEntity, TWriteRepo>
        where TEntity : class, IEntity
        where TWriteRepo : class, IRepositoryReadBase<TEntity>
    {
        protected readonly ITestOutputHelper _testOutputHelper;
        protected readonly Mock<TWriteRepo> _readRepoMock;
        protected readonly Mock<IMapper> _mapper;

        public QueryUseCaseTestBase(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _readRepoMock = new Mock<TWriteRepo>();
            _mapper = new Mock<IMapper>();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper.SetReturnsDefault(configurationProvider);
            _mapper.Setup(p => p.ConfigurationProvider.CreateMapper());

        }
    }
}
