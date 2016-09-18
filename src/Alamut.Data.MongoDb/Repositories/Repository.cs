using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.Paging;
using Alamut.Data.Repository;
using Alamut.Data.Structure;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Alamut.Data.MongoDb.Repositories
{
    public class Repository<TDocument> : QueryRepository<TDocument>,
        IRepository<TDocument> 
        where TDocument : class, IEntity
    {
        public Repository(IMongoDatabase database) : base(database)
        {
        }

        public virtual void Create(TDocument entity)
        {
            Collection.InsertOne(entity);
        }

        public virtual void AddRange(IEnumerable<TDocument> list)
        {
            Collection.InsertMany(list);
        }

        public virtual void Update(TDocument entity)
        {
            var filter = Builders<TDocument>.Filter
                .Eq(m => m.Id, entity.Id);

            Collection.ReplaceOne(filter, entity);
        }

        public virtual void UpdateOne<TField>(string id, 
            Expression<Func<TDocument, TField>> memberExpression, TField value)
        {
            var filter = Builders<TDocument>.Filter
                .Eq(m => m.Id, id);

            var update = Builders<TDocument>.Update
                .Set(memberExpression, value);
                

            var result = Collection.UpdateOne(filter, update);

            Debug.WriteLine(result);

        }

        public virtual void UpdateOne<TFilter, TField>(Expression<Func<TDocument, bool>> predicate, 
            Expression<Func<TDocument, TField>> memberExpression, TField value)
        {
            var update = Builders<TDocument>.Update
                .Set(memberExpression, value);

            Collection.UpdateOne(predicate, update);
        }

        public virtual void GenericUpdate(string id, Dictionary<string, dynamic> fieldset)
        {
            var filter = Builders<TDocument>.Filter
                .Eq(m => m.Id, id);

            var updateList = new List<UpdateDefinition<TDocument>>();

            foreach (var field in fieldset)
            {
                if (field.Value is IEnumerable && !(field.Value is string))
                    updateList.Add(Builders<TDocument>.Update.Set(field.Key, ((IEnumerable) field.Value).ToBson()));
                else
                    updateList.Add(Builders<TDocument>.Update.Set(field.Key, (BsonValue) field.Value ?? BsonNull.Value));
            }

            Collection.UpdateOne(filter, Builders<TDocument>.Update.Combine(updateList));
        }

        public virtual void AddToList<TValue>(string id, 
            Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value)
        {
            var filter = Builders<TDocument>.Filter
                .Eq(m => m.Id, id);

            var update = Builders<TDocument>.Update
                .AddToSet(memberExpression, value);

            Collection.UpdateOne(filter, update);
        }

        public virtual void RemoveFromList<TValue>(string id, 
            Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value)
        {
            var filter = Builders<TDocument>.Filter
                .Eq(m => m.Id, id);

            var update = Builders<TDocument>.Update
                .Pull(memberExpression, value);

            Collection.UpdateOne(filter, update);
        }

        public virtual ServiceResult Delete(string id)
        {
            var filter = Builders<TDocument>.Filter
                .Eq(m => m.Id, id);

            try
            {
                var result = Collection.DeleteOne(filter);

                return result.IsAcknowledged
                    ? ServiceResult.Okay($"{result.DeletedCount} item(s) successfully deleted.")
                    : ServiceResult.Okay("item(s) successfully deleted.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }
            
        }

        public virtual ServiceResult DeleteMany(Expression<Func<TDocument, bool>> predicate)
        {
            try
            {
                var result = Collection.DeleteMany(predicate);

                return result.IsAcknowledged
                    ? ServiceResult.Okay($"{result.DeletedCount} item(s) successfully deleted.")
                    : ServiceResult.Okay("item(s) successfully deleted.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }
            
        }

        //public virtual void SetDeleted(string id)
        //{
        //    Collection.UpdateOne(q => q.Id == id,
        //        new BsonDocument("$set", new BsonDocument(EntitySsot.IsDeleted, true)));
        //}
    }
}
