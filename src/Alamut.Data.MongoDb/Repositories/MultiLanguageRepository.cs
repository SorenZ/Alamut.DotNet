using System;
using System.Collections.Generic;
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
        private readonly ILocalizationService _localizationService;

        public MultiLanguageRepository(IMongoDatabase database, ILocalizationService localizationService) : base(database)
        {
            _localizationService = localizationService;
        }

        public override IQueryable<TDocument> Queryable => _localizationService.IsMulitLanguage
            ? Collection.AsQueryable().FilterByLanguage(_localizationService.CurrenttLanguage)
            : Collection.AsQueryable();


        public override void Create(TDocument entity)
        {
            if (_localizationService != null && _localizationService.IsMulitLanguage)
                entity.Lang = _localizationService.CurrenttLanguage;

            base.Create(entity);
        }

        public override TDocument Get(Expression<Func<TDocument, bool>> predicate) => _localizationService.IsMulitLanguage
            ? base.Get(predicate.AndAlso(q => q.Lang == _localizationService.CurrenttLanguage))
            : base.Get(predicate );

        public override TResult Get<TResult>(Expression<Func<TDocument, bool>> predicate, 
            Expression<Func<TDocument, TResult>> projection) => _localizationService.IsMulitLanguage 
            ? base.Get(predicate.AndAlso(q => q.Lang == _localizationService.CurrenttLanguage), projection)
            : base.Get(predicate, projection);

        public override List<TDocument> GetAll()
        {
            throw new NotImplementedException("this method not implemented in multilinguage repository.");
        }

        public override List<TResult> GetAll<TResult>(Expression<Func<TDocument, TResult>> projection)
        {
            throw new NotImplementedException("this method not implemented in multilinguage repository.");
        }

        public override List<TDocument> GetMany(Expression<Func<TDocument, bool>> predicate)
        {
            throw new NotImplementedException("this method not implemented in multilinguage repository.");
        }

        public override List<TResult> GetMany<TResult>(Expression<Func<TDocument, bool>> predicate, Expression<Func<TDocument, TResult>> projection)
        {
            throw new NotImplementedException("this method not implemented in multilinguage repository.");
        }
        
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