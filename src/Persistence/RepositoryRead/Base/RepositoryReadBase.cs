using Application.Common;
using Application.IRepositoryRead;
using Application.Model;
using Common;
using Domain;
using Domain.ReadEntitis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNetCore.OData.Query;

namespace Persistence.RepositoryRead.Base
{
    public abstract class RepositoryReadBase<T> : IRepositoryReadBase<T> where T : class, IEntity
    {
        #region Property
        private readonly IMongoDatabase _dB;
        protected abstract string QueueName { get;}
        private IMongoCollection<T> collection { get; set; }
        private readonly MongoClient mongoClient;
        protected readonly IRabbitMQUtility _rabbitMQUtility;

        public IQueryable<T> Queryable => collection.AsQueryable();
        #endregion

        protected RepositoryReadBase(IOptions<MongoDatabaseOption> databaseSettings, IRabbitMQUtility rabbitMQUtility)
        {
            mongoClient = new MongoClient(databaseSettings.Value.ConnectionString ?? "mongodb://localhost:27017");
            this._dB = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName ?? "Test");
            collection = _dB.GetCollection<T>(typeof(T).Name);
            this._rabbitMQUtility = rabbitMQUtility;
            SetReciveMessageEvent();
        }
        protected virtual void SetReciveMessageEvent()
        {
            _rabbitMQUtility.RecieveMessage(new RabbitMQRecieveRequest(

                QueueName,
                QueueName,
                async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    RabbitMQMessageModel rabbitMQMessageModel = JsonConvert.DeserializeObject<RabbitMQMessageModel>(message);
                    if (rabbitMQMessageModel != null)
                    {
                        T entity = JsonConvert.DeserializeObject<T>(rabbitMQMessageModel.Body);
                        switch (rabbitMQMessageModel.ChangedType)
                        {
                            case (byte)Microsoft.EntityFrameworkCore.EntityState.Added:
                                await this.Add(entity, CancellationToken.None);
                                break;
                            case (byte)Microsoft.EntityFrameworkCore.EntityState.Modified:
                                await this.Update(entity, CancellationToken.None);
                                break;
                            case (byte)Microsoft.EntityFrameworkCore.EntityState.Deleted:
                                await this.Delete(entity, CancellationToken.None);
                                break;
                            default:
                                break;
                        }
                    }
                    Console.WriteLine($" [x] Received {message}");
                }
            ));
        }
        #region Manipulate
        /// <exception cref="FormatException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> Add(T entity, CancellationToken cancellationToken)
        {
            return await AddMeny(Enumerable.Repeat(entity, 1), cancellationToken);
        }

        /// <exception cref="FormatException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> AddMeny(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            if (entities is null) throw new ArgumentNullException(typeof(T).Name, string.Format(CommonMessage.NullException, typeof(T).Name));
            using (var session = mongoClient.StartSession())
            {
                try
                {
                    InsertManyOptions insertManyOptions = new InsertManyOptions();
                    insertManyOptions.IsOrdered = true;
                    await this.collection.InsertManyAsync(session, entities, insertManyOptions, cancellationToken);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
        /// <exception cref="FormatException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> Delete(T entity, CancellationToken cancellationToken)
        {
            if (entity is null) throw new ArgumentNullException(typeof(T).Name, string.Format(CommonMessage.NullException, typeof(T).Name));
            return await Delete(entity.Id, cancellationToken);
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                await this.collection.DeleteOneAsync(p => p.Id == id, cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        /// <exception cref="FormatException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> Update(T entity, CancellationToken cancellationToken)
        {
            if (entity is null) throw new ArgumentNullException(typeof(T).Name, string.Format(CommonMessage.NullException, typeof(T).Name));
            try
            {

                ReplaceOptions replaceOptions = new ReplaceOptions();
                replaceOptions.IsUpsert = true;
                await this.collection.ReplaceOneAsync<T>(p => p.Id == entity.Id, entity, replaceOptions, cancellationToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
        public async Task<T?> FindOne(int id)=>  Queryable.Where(p => p.Id == id).FirstOrDefault();

        public async Task<Tuple<List<T>, int>> ItemList(ODataQueryOptions<T> oDataQuery)
        {
            ODataQuerySettings settings = new ODataQuerySettings()
            {
                PageSize = 10,
            };

            IQueryable<T> results = oDataQuery.ApplyTo(Queryable, settings).Cast<T>();
            return new Tuple<List<T>, int>(
                results.ToList() ,
               results.Count()
                );
        }
    }
}
