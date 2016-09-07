using System.Linq;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Helpers.Localization;
using Alamut.Utilities.Localization;
using MongoDB.Driver;

namespace Alamut.Data.MongoDb.Repositories
{
    /// <summary>
    /// provide repository with multi-lingual  features
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public class MultiLanguageRepository<TDocument> : Repository<TDocument>,
        IRepository<TDocument>
        where TDocument : class, IEntity, IMultiLanguageEnity
    {

        public MultiLanguageRepository(IMongoDatabase database) : base(database)
        {
        }

        public override IQueryable<TDocument> Queryable => Collection.AsQueryable().FilterByLanguage(Language.Current);

        //public virtual TDocument Get(Expression<Func<TDocument, bool>> predicate)
        //{
        //    return Collection.Find(predicate).FirstOrDefault();

        //    //predicate.AndAlso(q => (q as IMultiLanguageEnity).Lang == Language.Current);
        //    //return Collection.Find(predicate).FirstOrDefault();
        //}
    }


    public static class languageExt
    {
        public static Expression<TDelegate> AndAlso<TDelegate>(this Expression<TDelegate> left,
            Expression<TDelegate> right)
        {
            // NOTICE: Combining BODIES:
            return Expression.Lambda<TDelegate>(Expression.AndAlso(left.Body, right.Body), left.Parameters);
        }
    }
}