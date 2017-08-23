//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using Alamut.Data.Entity;
//using Alamut.Data.Repository;
//using Alamut.Data.Structure;
//using Alamut.Utilities.Localization;
//using Microsoft.Extensions.Options;
//using MongoDB.Driver;

//namespace Alamut.Data.MongoDb.Repositories
//{
//    /// <summary>
//    /// provide repository with multi-lingual  features
//    /// </summary>
//    /// <typeparam name="TDocument"></typeparam>
//    public class MultiLanguageRepository<TDocument> : Repository<TDocument>,
//        IRepository<TDocument>
//        where TDocument : class, IEntity, IMultiLanguageEnity
//    {
//        private readonly LocalizationConfig _localizationConfig;

//        public MultiLanguageRepository(IMongoDatabase database, LocalizationConfig localizationConfig) : base(database)
//        {
//            _localizationConfig = localizationConfig;
//        }

//        public override IQueryable<TDocument> Queryable => _localizationConfig.IsMultiLanguage
//            ? Collection.AsQueryable().FilterByLanguage(_localizationConfig.CurrentLanguage)
//            : Collection.AsQueryable();


//        public override ServiceResult<string> Create(TDocument entity)
//        {
//            if (_localizationConfig.IsMultiLanguage && string.IsNullOrEmpty(entity.Lang))
//                entity.Lang = _localizationConfig.CurrentLanguage;

//            return base.Create(entity);
//        }

//        public override TDocument Get(Expression<Func<TDocument, bool>> predicate)
//            => this.Queryable.FirstOrDefault(predicate);


//        public override TResult Get<TResult>(Expression<Func<TDocument, bool>> predicate,
//            Expression<Func<TDocument, TResult>> projection)
//            => this.Queryable.Where(predicate).Select(projection).FirstOrDefault();

//        public override List<TDocument> GetAll() 
//            => this.Queryable.ToList();

//        public override List<TResult> GetAll<TResult>(Expression<Func<TDocument, TResult>> projection)
//            => this.Queryable.Select(projection).ToList();

//        public override List<TDocument> GetMany(Expression<Func<TDocument, bool>> predicate)
//            => this.Queryable.Where(predicate).ToList();

//        public override List<TResult> GetMany<TResult>(Expression<Func<TDocument, bool>> predicate,
//            Expression<Func<TDocument, TResult>> projection)
//            => this.Queryable.Where(predicate).Select(projection).ToList();

//    }
//}