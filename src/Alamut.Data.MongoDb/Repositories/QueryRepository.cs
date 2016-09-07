using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.Paging;
using Alamut.Data.Repository;
using Alamut.Helpers.Localization;
using Alamut.Utilities.Localization;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Alamut.Data.MongoDb.Repositories
{
    public class QueryRepository<TDocument> : IQueryRepository<TDocument> where TDocument : class, IEntity
    {
        protected readonly IMongoCollection<TDocument> Collection;
        
        // TODO : require test.
        //private static readonly Lazy<string> CollectionName =
        //    new Lazy<string>(() => BsonClassMap.LookupClassMap(typeof (TDocument)).Discriminator);

        public QueryRepository(IMongoDatabase database)
        {
            Collection = database.GetCollection<TDocument>(typeof(TDocument).Name);
        }

        public virtual IQueryable<TDocument> Queryable => Collection.AsQueryable();
       

        public virtual TDocument Get(string id)
        {
            return Collection.Find(m => m.Id == id).FirstOrDefault();
        }
        

        public virtual TDocument Get(Expression<Func<TDocument, bool>> predicate)
        {
            return Collection.Find(predicate).FirstOrDefault();

            //predicate.AndAlso(q => (q as IMultiLanguageEnity).Lang == Language.Current);
            //return Collection.Find(predicate).FirstOrDefault();
        }

        public virtual TResult Get<TResult>(string id, Expression<Func<TDocument, TResult>> projection)
        {
            return Collection.Find(m => m.Id == id)
                .Project(projection)
                .FirstOrDefault();
        }

        public virtual TResult Get<TResult>(Expression<Func<TDocument, bool>> predicate,
            Expression<Func<TDocument, TResult>> projection)
        {
            return Collection.Find(predicate)
                .Project(projection)
                .FirstOrDefault();
        }

        public virtual List<TDocument> GetAll()
        {
            return Collection.Find(new BsonDocument()).ToList();
        }

        public virtual List<TResult> GetAll<TResult>(Expression<Func<TDocument, TResult>> projection)
        {
            return Collection.Find(new BsonDocument())
                .Project(projection)
                .ToList();
        }

        public virtual List<TDocument> GetMany(Expression<Func<TDocument, bool>> predicate)
        {
            return Collection.Find(predicate).ToList();
        }

        public virtual List<TDocument> GetMany(IEnumerable<string> ids)
        {
            return Collection.Find(q => ids.Contains(q.Id)).ToList();
        }

        public virtual List<TResult> GetMany<TResult>(Expression<Func<TDocument, bool>> predicate,
            Expression<Func<TDocument, TResult>> projection)
        {
            return Collection
                .Find(predicate)
                .Project(projection)
                .ToList();
        }

        public virtual List<TResult> GetMany<TResult>(IEnumerable<string> ids, Expression<Func<TDocument, TResult>> projection)
        {
            return Collection
                .Find(q => ids.Contains(q.Id))
                .Project(projection)
                .ToList();
        }


        public virtual IPaginated<TDocument> GetPaginated(PaginatedCriteria criteria = null)
        {
            var internalCriteria = criteria ?? new PaginatedCriteria();

            var query = Collection.Find(new BsonDocument())
                .Skip(internalCriteria.StartIndex)
                .Limit(internalCriteria.PageSize);

            return new Paginated<TDocument>(query.ToEnumerable(),
                Collection.Find(new BsonDocument()).Count(),
                internalCriteria.CurrentPage,
                internalCriteria.PageSize);
        }

    }
}
