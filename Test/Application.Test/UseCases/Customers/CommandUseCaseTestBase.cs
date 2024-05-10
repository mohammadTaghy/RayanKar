using Application.IRepositoryWrite.Base;
using Application.Mapping;
using AutoMapper;
using Domain;

namespace Application.Test.UseCases.Customers
{
    public class CommandUseCaseTestBase<TEntity, TWriteRepo>
        where TEntity : class, IEntity
        where TWriteRepo : class, IRepositoryWriteBase<TEntity>
    {
        protected readonly ITestOutputHelper _testOutputHelper;
        protected readonly Mock<TWriteRepo> _writeRepoMock;
        protected readonly Mock<IMapper> _mapper;

        public CommandUseCaseTestBase(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _writeRepoMock = new Mock<TWriteRepo>();
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
