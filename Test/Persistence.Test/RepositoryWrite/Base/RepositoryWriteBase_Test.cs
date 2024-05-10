using Application.IRepositoryWrite.Base;
using Common;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Test.RepositoryWrite.Base
{
    public abstract class RepositoryWriteBase_Test<TEntity, TRepository>
        where TEntity : class, IEntity
        where TRepository : class, IRepositoryWriteBase<TEntity>
    {
        protected readonly TRepository _repository;
        private readonly TEntity _entity;
        private static PersistanceDBContext _dbContext = null;
        protected static PersistanceDBContext _DbContext
        {
            get
            {
                if (_dbContext is null)
                {
                    var option = new DbContextOptionsBuilder<PersistanceDBContext>().UseInMemoryDatabase(databaseName: "MyInMemoryDB").Options;
                    _dbContext = new PersistanceDBContext(option);
                    _dbContext.Database.EnsureCreated();
                }
                return _dbContext;
            }
        }
        public RepositoryWriteBase_Test(TRepository repository, TEntity entity)
        {

            this._repository = repository;
            _entity = entity;
            Task.WaitAll(addToMemory());


        }
        private async Task addToMemory()
        {
            TEntity? entity = await _repository.Find(_entity.Id);
            if (entity==null)
            {
                await _repository.Insert(_entity);
            }
        }
        [Fact]
        public async Task Find_GivenIncorrectId_NotFoundException()
        {
            TEntity? exception = await _repository.Find(0);
            Assert.Null(exception);
        }
        [Fact]
        public async Task Find_GivencorrectId_ResultOK()
        {
            TEntity? entity = await _repository.Find(_entity.Id);

            Assert.NotNull(entity);

        }

        [Fact]
        public async Task Find_GivenIncorrectExpression_NotFoundException()
        {
            TEntity? result = await _repository.Find(p => false);
            
            Assert.Null(result);
        }
        /// <exception cref="Xunit.Sdk.NotNullException"></exception>
        [Fact]
        public async Task Find_GivencorrectExpression_ResultOK()
        {
            TEntity? entity = await _repository.Find(p => p.Id==_entity.Id);
            
            Assert.NotNull(entity);
        }

        #region DeleteItem
       

        [Fact]
        public void DeleteItem_GivencorrectEntity_ResultOK()
        {
            Task result = _repository.DeleteItem(_entity);

            Assert.True(result.IsCompleted);
        }
        #endregion
        #region Insert
        
        [Fact]
        public async Task Insert_GivencorrectEntity_ResultOK()
        {
            await _repository.DeleteItem(_entity);
            Task result =  _repository.Insert(_entity);

            Assert.True(result.IsCompleted);
        }
        #endregion
        #region Update
       
        [Fact]
        public void Update_GivencorrectEntity_ResultOK()
        {
            Task result = _repository.Update(_entity);

            Assert.True(result.IsCompleted);
        }
        #endregion
    }
}
