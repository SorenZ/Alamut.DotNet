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
            this._localizationService = localizationService;
        }

        public override IQueryable<TDocument> Queryable => _localizationService.IsMulitLanguage
            ? Collection.AsQueryable().FilterByLanguage(_localizationService.CurrenttLanguage)
            : Collection.AsQueryable();


        public override void Create(TDocument entity)
        {
            if (_localizationService.IsMulitLanguage)
                entity.Lang = _localizationService.CurrenttLanguage;

            base.Create(entity);
        }

        public override TDocument Get(Expression<Func<TDocument, bool>> predicate)
            => this.Queryable.FirstOrDefault(predicate);


        public override TResult Get<TResult>(Expression<Func<TDocument, bool>> predicate,
            Expression<Func<TDocument, TResult>> projection)
            => this.Queryable.Where(predicate).Select(projection).FirstOrDefault();

        public override List<TDocument> GetAll() 
            => this.Queryable.ToList();

        public override List<TResult> GetAll<TResult>(Expression<Func<TDocument, TResult>> projection)
            => this.Queryable.Select(projection).ToList();

        public override List<TDocument> GetMany(Expression<Func<TDocument, bool>> predicate)
            => this.Queryable.Where(predicate).ToList();

        public override List<TResult> GetMany<TResult>(Expression<Func<TDocument, bool>> predicate,
            Expression<Func<TDocument, TResult>> projection)
            => this.Queryable.Where(predicate).Select(projection).ToList();

    }
}