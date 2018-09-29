using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.Paging;

namespace Alamut.Data.Repository
{
    /// <summary>
    /// represent readonly repository method to fetch data from database
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <remarks>
    /// - it's recommended to use this repo in UI layer
    /// </remarks>
    public interface IQueryRepository<TEntity, in TKey> 
        where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// provide a queryable source of elements
        /// be carefur, the Queryable has not been tracked in sql repositories
        /// </summary>
        IQueryable<TEntity> Queryable { get; }

        /// <summary>
        /// get an item by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(TKey id);

        /// <summary>
        /// get an item by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// get an item (selected fields bye projection) by id
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        TResult Get<TResult>(TKey id);

        /// <summary>
        /// get an item (selected fields bye projection) by predicate
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TResult Get<TResult>(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// get all items 
        /// </summary>
        /// could be true, false, null
        /// null -> not important 
        /// <returns></returns>
        List<TEntity> GetAll();

        /// <summary>
        /// get a list of projected item
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        List<TResult> GetAll<TResult>();


        /// <summary>
        /// get a list of items by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// get a list of items by ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<TEntity> GetMany(IEnumerable<TKey> ids);

        /// <summary>
        /// get a list of items (selected fields) by predicate
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<TResult> GetMany<TResult>(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// get selected item(by projecction) 
        /// filterd by ids
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<TResult> GetMany<TResult>(IEnumerable<TKey> ids);


        /// <summary>
        /// get items paginated by criteria
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IPaginated<TEntity> GetPaginated(PaginatedCriteria criteria = null);

    }
}