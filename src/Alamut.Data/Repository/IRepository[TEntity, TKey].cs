using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.Structure;

namespace Alamut.Data.Repository
{
    /// <inheritdoc />
    /// <summary>
    /// represend complete repository methods to query and manipulate the database
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <remarks>
    /// it's recommended to use this repo in Service layer not UI
    /// </remarks>
    public interface IRepository<TEntity,TKey> : IQueryRepository<TEntity,TKey> 
        where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// create an item 
        /// and commit into database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="commit">save changes into database</param>
        ServiceResult<TKey> Create(TEntity entity, bool commit = true);

        /// <summary>
        /// add list of item into database
        /// and commit into database.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="commit"></param>
        ServiceResult AddRange(IEnumerable<TEntity> list, bool commit = true);

        /// <summary>
        /// update item total value
        /// and commit into database.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="commit"></param>
        ServiceResult Update(TEntity entity, bool commit = true);

        /// <summary>
        /// update an item (one field) by expression member selector by id
        /// and commit into database.
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="id"></param>
        /// <param name="memberExpression"></param>
        /// <param name="value"></param>
        /// <remarks>
        /// Even if multiple documents match the filter, only one will be updated because we used UpdateOne
        /// </remarks>
        ServiceResult UpdateOne<TField>(TKey id, 
            Expression<Func<TEntity, TField>> memberExpression, 
            TField value);

        /// <summary>
        /// update an item (one field) by expression member selector (select item by predicate)
        /// and commit into database.
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <typeparam name="TField"></typeparam>
        /// <param name="filterExpression"></param>
        /// <param name="memberExpression"></param>
        /// <param name="value"></param>
        /// <remarks>
        /// Even if multiple documents match the filter, only one will be updated because we used UpdateOne
        /// </remarks>
        ServiceResult UpdateOne<TFilter, TField>(Expression<Func<TEntity, bool>> filterExpression, 
            Expression<Func<TEntity, TField>> memberExpression, TField value);

        /// <summary>
        /// update fieldset in the databse by provided id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fieldset"></param>
        ServiceResult GenericUpdate(TKey id, Dictionary<string, dynamic> fieldset);

        /// <summary>
        /// add an item to a list (if not exist)
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="id"></param>
        /// <param name="memberExpression"></param>
        /// <param name="value"></param>
        ServiceResult AddToList<TValue>(TKey id, 
            Expression<Func<TEntity, IEnumerable<TValue>>> memberExpression, 
            TValue value);

        /// <summary>
        /// remove an item from a list (all item if same)
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="id"></param>
        /// <param name="memberExpression"></param>
        /// <param name="value"></param>
        ServiceResult RemoveFromList<TValue>(TKey id, 
            Expression<Func<TEntity, IEnumerable<TValue>>> memberExpression, 
            TValue value);

        /// <summary>
        /// Deletes an item by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commit"></param>
        ServiceResult Delete(TKey id, bool commit = true);

        /// <summary>
        /// Deletes multiple documents.
        /// </summary>
        /// <param name="predicate">represent expression to filter delete</param>
        /// <param name="commit"></param>
        ServiceResult DeleteMany(Expression<Func<TEntity, bool>> predicate
            , bool commit = true);
    }
}
