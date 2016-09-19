using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.NoSql;
using Alamut.Data.Paging;

namespace Alamut.Data.Repository
{
    /// <summary>
    /// represent readonly repository method to fetch data from database
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    /// <remarks>
    /// - it's recommended to use this repo in UI layer
    /// </remarks>
    public interface IQueryRepository<TDocument> where TDocument : IEntity
    {
        /// <summary>
        /// provide a queryable source of elements
        /// be carefur, the Queryable not been tracked in sql repositories
        /// </summary>
        IQueryable<TDocument> Queryable { get; }

        /// <summary>
        /// get an item by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TDocument Get(string id);

        /// <summary>
        /// get an item by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TDocument Get(Expression<Func<TDocument, bool>> predicate);

        /// <summary>
        /// get an item (selected fields bye projection) by id
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="id"></param>
        /// <param name="projection"></param>
        /// <returns></returns>
        TResult Get<TResult>(string id, Expression<Func<TDocument, TResult>> projection);

        /// <summary>
        /// get an item (selected fields bye projection) by predicate
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="projection"></param>
        /// <returns></returns>
        TResult Get<TResult>(Expression<Func<TDocument, bool>> predicate,
            Expression<Func<TDocument, TResult>> projection);

        /// <summary>
        /// get all items 
        /// </summary>
        /// could be true, false, null
        /// null -> not important 
        /// <returns></returns>
        List<TDocument> GetAll();

        /// <summary>
        /// get a list of projected item
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="projection"></param>
        /// <returns></returns>
        List<TResult> GetAll<TResult>(Expression<Func<TDocument, TResult>> projection);


        /// <summary>
        /// get a list of items by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<TDocument> GetMany(Expression<Func<TDocument, bool>> predicate);

        /// <summary>
        /// get a list of items by ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<TDocument> GetMany(IEnumerable<string> ids);

        /// <summary>
        /// get a list of items (selected fields) by predicate
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="projection"></param>
        /// <returns></returns>
        List<TResult> GetMany<TResult>(Expression<Func<TDocument, bool>> predicate,
            Expression<Func<TDocument, TResult>> projection);

        /// <summary>
        /// get selected item(by projecction) 
        /// filterd by ids
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ids"></param>
        /// <param name="projection"></param>
        /// <returns></returns>
        List<TResult> GetMany<TResult>(IEnumerable<string> ids,
            Expression<Func<TDocument, TResult>> projection);


        /// <summary>
        /// get items paginated by criteria
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IPaginated<TDocument> GetPaginated(PaginatedCriteria criteria = null);

    }
}