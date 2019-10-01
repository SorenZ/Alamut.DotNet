using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Abstractions.Structure;

namespace Alamut.Data.Service
{
    /// <inheritdoc />
    /// <summary>
    /// provide base CRUD service contract 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface ICrudService<TEntity, TKey> : IService<TEntity, TKey> 
        where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// create an item by mapping model into entity and 
        /// add it in database.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Result<TKey> Create<TModel>(TModel model);

        /// <summary>
        /// update an item by id
        /// mapping model into entity (use new properties in model & old properties in entity)
        /// update database
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Result Update<TModel>(TKey id, TModel model);


        /// <summary>
        /// update an item (one field) by expression member selector by id
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="id"></param>
        /// <param name="memberExpression"></param>
        /// <param name="value"></param>
        /// <remarks>
        /// Even if multiple documents match the filter, only one will be updated because we used UpdateOne
        /// </remarks>
        Result UpdateOne<TField>(TKey id,
            Expression<Func<TEntity, TField>> memberExpression,
            TField value);

        /// <summary>
        /// delete item by Id
        /// </summary>
        /// <param name="id">entity or document Id</param>
        /// <returns></returns>
        Result Delete(TKey id);

        /// <summary>
        /// get single result and map to demanded type
        /// </summary>
        /// <typeparam name="TResult">type of result</typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>TResult should already mapped to current Entity</remarks>
        TResult Get<TResult>(TKey id);

        /// <summary>
        /// get list of entity and map to demanded type
        /// by ids
        /// </summary>
        /// <typeparam name="TResult">type of result</typeparam>
        /// <param name="ids">entity.id</param>
        /// <returns></returns>
        /// <remarks>TResult should already mapped to current Entity</remarks>
        List<TResult> GetMany<TResult>(IEnumerable<TKey> ids);

        /// <summary>
        /// get list of entity and map to demanded type
        /// by predicate
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <remarks>TResult should already mapped to current Entity</remarks>
        List<TResult> GetMany<TResult>(Expression<Func<TEntity, bool>> predicate);
    }
}