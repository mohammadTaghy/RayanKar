using Application.IRepositoryRead;
using Application.UseCases.Customers.Query.Customers;
using Common;
using Domain;
using Microsoft.AspNetCore.OData.Query;
using SharedProject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Test.RepositoryRead.Base
{
    public abstract class RepositoryReadBase_Test<TEntity, TRepository>
        where TEntity : class, IEntity
        where TRepository : class, IRepositoryReadBase<TEntity>
    {
        protected readonly TRepository _repository;
        private readonly TEntity _entity;

        protected RepositoryReadBase_Test(TRepository repository, TEntity entity)
        {

            this._repository = repository;
            _entity = entity;
            Task.WaitAll(AddToDB());
        }
        private async Task AddToDB()
        {
            TEntity? entity = await _repository.FindOne(_entity.Id);
            if (entity==null)
            {
                await _repository.Add(_entity, CancellationToken.None);
            }
        }

        #region Find
        /// <exception cref="Xunit.Sdk.ThrowsException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="Xunit.Sdk.EqualException"></exception>
        [Fact]
        public async Task FindOne_GivenIncorrectId_NotFoundException()
        {
            var result = await _repository.FindOne(0);

            Assert.Null(result);
        }
        /// <exception cref="Xunit.Sdk.NotNullException"></exception>
        [Fact]
        public async Task FindOne_GivencorrectId_ResultOK()
        {
            TEntity? entity = await _repository.FindOne(_entity.Id);

            Assert.NotNull(entity);

        }
        #endregion
        #region FindAll
        /// <exception cref="Xunit.Sdk.TrueException"></exception>
        [Fact]
        public async Task FindAll_GivenPageIndexAndSize_ReturnFals()
        {
            var result = await _repository.ItemList(GetOdataQueryOption("https://localhost:8080/?$top=10&$skip=0&$orderby=Id&$count=true"));

            Assert.True(result.Item2 > 0);
        }
        protected abstract ODataQueryOptions<TEntity> GetOdataQueryOption(string url);
        #endregion

        #region DeleteItem
        /// <exception cref="Xunit.Sdk.ThrowsException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="Xunit.Sdk.EqualException"></exception>
        [Fact]
        public async Task DeleteItem_GivenNullEntity_ArgumentNullException()
        {

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _repository.Delete(null, CancellationToken.None));

            Assert.Equal(
                string.Format($"{CommonMessage.NullException} (Parameter '{typeof(TEntity).Name}')", typeof(TEntity).Name),
                exception.Message);
        }

        /// <exception cref="Xunit.Sdk.TrueException"></exception>
        [Fact]
        public async Task DeleteItem_GivencorrectEntity_ResultOK()
        {
            bool result = await _repository.Delete(_entity, CancellationToken.None);

            Assert.True(result);
        }
        #endregion
        #region Add
        /// <exception cref="Xunit.Sdk.TrueException"></exception>
        [Fact]
        public async Task Add_GivenCorrectEntity_ResultOK()
        {
            await _repository.Delete(_entity, CancellationToken.None);
            bool result = await _repository.Add(_entity, CancellationToken.None);

            Assert.True(result);
        }
        #endregion
        #region AddMeny
        /// <exception cref="Xunit.Sdk.EqualException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="Xunit.Sdk.ThrowsException"></exception>
        [Fact]
        public async Task AddMeny_GivenNullEntity_ArgumentNullException()
        {

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _repository.AddMeny(null, CancellationToken.None));

            Assert.Equal(
                string.Format($"{CommonMessage.NullException} (Parameter '{typeof(TEntity).Name}')", typeof(TEntity).Name),
                exception.Message);
        }
        /// <exception cref="Xunit.Sdk.TrueException"></exception>
        [Fact]
        public async Task AddMeny_GivenCorrectEntity_ResultOK()
        {

            await _repository.Delete(_entity, CancellationToken.None);
            bool result = await _repository.AddMeny(Enumerable.Repeat(_entity, 1), CancellationToken.None);

            Assert.True(result);
        }
        #endregion
        #region Update
        /// <exception cref="Xunit.Sdk.ThrowsException"></exception>
        /// <exception cref="Xunit.Sdk.EqualException"></exception>
        /// <exception cref="FormatException"></exception>
        [Fact]
        public async Task Update_GivenNullEntity_ArgumentNullException()
        {

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _repository.Update(null, CancellationToken.None));

            Assert.Equal(
                string.Format($"{CommonMessage.NullException} (Parameter '{typeof(TEntity).Name}')", typeof(TEntity).Name),
                exception.Message);
        }
        /// <exception cref="Xunit.Sdk.TrueException"></exception>
        [Fact]
        public async Task Update_GivenCorrectEntity_ResultOK()
        {
            bool result = await _repository.Update(_entity, CancellationToken.None);

            Assert.True(result);
        }
        #endregion

    }
}
