using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.SSOT;
using Alamut.Data.Structure;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Alamut.Data.MongoDb.Repositories
{
    public class Repository<TDocument> : QueryRepository<TDocument>,
        IRepository<TDocument,string> 
        where TDocument : class, IEntity<string>
    {
        public Repository(IMongoDatabase database) : base(database)
        { }

        public virtual ServiceResult<string> Create(TDocument entity)
        {
            try
            {
                Collection.InsertOne(entity);
                return ServiceResult<string>.Okay(entity.Id, Messages.ItemCreated);
            }
            catch (Exception ex)
            {
                return ServiceResult<string>.Exception(ex);
            }
        }

        public virtual ServiceResult AddRange(IEnumerable<TDocument> list)
        {
            try
            {
                Collection.InsertMany(list);
                return ServiceResult.Okay(Messages.ItemsCreated);
            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }
        }

        public virtual ServiceResult Update(TDocument entity)
        {
            var filter = Builders<TDocument>.Filter
                .Eq(m => m.Id, entity.Id);

            try
            {
                var result = Collection.ReplaceOne(filter, entity);

                return ServiceResult.Okay(Messages.ItemUpdated);

                // TODO : use it in log system
                //return result.IsAcknowledged && result.IsModifiedCountAvailable
                //    ? ServiceResult.Okay($"{result.ModifiedCount} item successfully updated.")
                //    : ServiceResult.Okay("item successfully updated.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }
        }

        public ServiceResult<string> Create(TDocument entity, bool commit = true)
        {
            throw new NotImplementedException();
        }

        public ServiceResult AddRange(IEnumerable<TDocument> list, bool commit = true)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Update(TDocument entity, bool commit = true)
        {
            throw new NotImplementedException();
        }

        public virtual ServiceResult UpdateOne<TField>(string id, Expression<Func<TDocument, TField>> memberExpression, TField value)
        {
            var filter = Builders<TDocument>.Filter
                .Eq(m => m.Id, id);

            var update = Builders<TDocument>.Update
                .Set(memberExpression, value);

            try
            {
                var result = Collection.UpdateOne(filter, update);

                return ServiceResult.Okay(Messages.ItemUpdated);
                //return result.IsAcknowledged && result.IsModifiedCountAvailable
                //    ? ServiceResult.Okay($"{result.ModifiedCount} item successfully updated.")
                //    : ServiceResult.Okay("item successfully updated.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }

        }

        public virtual ServiceResult UpdateOne<TFilter, TField>(Expression<Func<TDocument, bool>> predicate, Expression<Func<TDocument, TField>> memberExpression, TField value)
        {
            var update = Builders<TDocument>.Update
                .Set(memberExpression, value);

            try
            {
                var result = Collection.UpdateOne(predicate, update);
                return ServiceResult.Okay(Messages.ItemUpdated);

                //return result.IsAcknowledged && result.IsModifiedCountAvailable
                //    ? ServiceResult.Okay($"{result.ModifiedCount} item successfully updated.")
                //    : ServiceResult.Okay("item successfully updated.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }
        }

        [Obsolete("it's not recommended")]
        public virtual ServiceResult GenericUpdate(string id, Dictionary<string, dynamic> fieldset)
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

            
            try
            {
                var result = Collection.UpdateOne(filter, Builders<TDocument>.Update.Combine(updateList));
                return ServiceResult.Okay(Messages.ItemUpdated);

                //return result.IsAcknowledged && result.IsModifiedCountAvailable
                //    ? ServiceResult.Okay($"{result.ModifiedCount} item(s) successfully updated.")
                //    : ServiceResult.Okay("item(s) successfully updated.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }
        }

        public virtual ServiceResult AddToList<TValue>(string id, Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value)
        {
            var filter = Builders<TDocument>.Filter
                .Eq(m => m.Id, id);

            var update = Builders<TDocument>.Update
                .AddToSet(memberExpression, value);

            try
            {
                var result = Collection.UpdateOne(filter, update);
                return ServiceResult.Okay(Messages.ItemUpdated);

                //return result.IsAcknowledged && result.IsModifiedCountAvailable
                //    ? ServiceResult.Okay($"{result.ModifiedCount} item successfully updated.")
                //    : ServiceResult.Okay("item successfully updated.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }
        }

        public virtual ServiceResult RemoveFromList<TValue>(string id, 
            Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, 
            TValue value)
        {
            var filter = Builders<TDocument>.Filter
                .Eq(m => m.Id, id);

            var update = Builders<TDocument>.Update
                .Pull(memberExpression, value);

            try
            {
                var result = Collection.UpdateOne(filter, update);
                return ServiceResult.Okay(Messages.ItemUpdated);

                //return result.IsAcknowledged && result.IsModifiedCountAvailable
                //    ? ServiceResult.Okay($"{result.ModifiedCount} item successfully updated.")
                //    : ServiceResult.Okay("item successfully updated.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }
        }

        public ServiceResult Delete(string id, bool commit = true)
        {
            throw new NotImplementedException();
        }

        public ServiceResult DeleteMany(Expression<Func<TDocument, bool>> predicate, bool commit = true)
        {
            throw new NotImplementedException();
        }

        public virtual ServiceResult Delete(string id)
        {
            var filter = Builders<TDocument>.Filter
                .Eq(m => m.Id, id);

            try
            {
                var result = Collection.DeleteOne(filter);
                return ServiceResult.Okay(Messages.ItemDeleted);

                //return result.IsAcknowledged
                //    ? ServiceResult.Okay($"{result.DeletedCount} item(s) successfully deleted.")
                //    : ServiceResult.Okay("item(s) successfully deleted.");
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
                return ServiceResult.Okay(Messages.ItemDeleted);

                //return result.IsAcknowledged
                //    ? ServiceResult.Okay($"{result.DeletedCount} item(s) successfully deleted.")
                //    : ServiceResult.Okay("item(s) successfully deleted.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }
            
        }

    }
}
