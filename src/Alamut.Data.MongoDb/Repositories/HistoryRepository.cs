﻿using System.Collections.Generic;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using MongoDB.Driver;

namespace Alamut.Data.MongoDb.Repositories
{
    /// <summary>
    /// generic implementation of IHistoryRepository
    /// </summary>
    /// <typeparam name="THistoryDocument"></typeparam>
    public class HistoryRepository<THistoryDocument> : IHistoryRepository<THistoryDocument>
        where THistoryDocument : IHistoryEntity
    {
        protected readonly IMongoCollection<THistoryDocument> Collection;

        public HistoryRepository(IMongoDatabase database)
        {
            Collection = database.GetCollection<THistoryDocument>(typeof(THistoryDocument).Name);
        }

        public virtual void Push(THistoryDocument entity) 
        {
            Collection.InsertOne(entity);
        }

        public virtual TModel Pull<TModel>(string id) where TModel : class
        {
            var entity = Collection.Find(q => q.Id == id)
                .FirstOrDefault();

            if(entity == null)
                 return null;

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(entity.ModelValue);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<TModel>(json);
        }

        public virtual dynamic Pull(string historyId)
        {
            return Collection.Find(q => q.Id == historyId)
                .Project(s => s.ModelValue)
                .FirstOrDefault();
        }

        public virtual List<THistoryDocument> GetMany(string entityName, string modelName, string entityId)
        {
            return Collection.Find(q => q.EntityName == entityName 
                && q.ModelName == modelName
                && q.EntityId == entityId)
                .ToList();
        }

        public virtual List<THistoryDocument> GetMany(string entityName, string entityId)
        {
            return Collection.Find(q => q.EntityName == entityName
                && q.EntityId == entityId)
                .ToList();
        }
    }

    /// <summary>
    /// default implementation of IHistoryRepository with BaseHistory model
    /// </summary>
    public class HistoryRepository : IHistoryRepository
    {
        protected readonly IMongoCollection<BaseHistory> Collection;

        public HistoryRepository(IMongoDatabase database)
        {
            Collection = database.GetCollection<BaseHistory>(typeof(BaseHistory).Name);
        }

        public void Push(BaseHistory entity)
        {
            Collection.InsertOne(entity);
        }

        public TModel Pull<TModel>(string id) where TModel : class
        {
            var entity = Collection.Find(q => q.Id == id)
                .FirstOrDefault();

            if (entity == null)
                return null;

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(entity.ModelValue);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<TModel>(json);
        }

        public dynamic Pull(string historyId)
        {
            return Collection.Find(q => q.Id == historyId)
                .Project(s => s.ModelValue)
                .FirstOrDefault();
        }

        public List<BaseHistory> GetMany(string entityName, string modelName, string entityId)
        {
            return Collection.Find(q => q.EntityName == entityName
                && q.ModelName == modelName
                && q.EntityId == entityId)
                .ToList();
        }

        public List<BaseHistory> GetMany(string entityName, string entityId)
        {
            return Collection.Find(q => q.EntityName == entityName
                && q.EntityId == entityId)
                .ToList();
        }
    }
}
