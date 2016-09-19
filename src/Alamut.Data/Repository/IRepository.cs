using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.Structure;

namespace Alamut.Data.Repository
{
    /// <summary>
    /// represend complete repository methods to query and manipulate the database
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    /// <remarks>
    /// it's recommended to use this repo in Service layer not UI
    /// </remarks>
    public interface IRepository<TDocument> : IQueryRepository<TDocument> 
        where TDocument : IEntity
    {
        /// <summary>
        /// create an item 
        /// and commit into database.
        /// </summary>
        /// <param name="entity"></param>
        ServiceResult<string> Create(TDocument entity);

        /// <summary>
        /// add list of item into database
        /// and commit into database.
        /// </summary>
        /// <param name="list"></param>
        ServiceResult AddRange(IEnumerable<TDocument> list);

        /// <summary>
        /// update item total value
        /// and commit into database.
        /// </summary>
        /// <param name="entity"></param>
        ServiceResult Update(TDocument entity);

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
        ServiceResult UpdateOne<TField>(string id, 
            Expression<Func<TDocument, TField>> memberExpression, 
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
        ServiceResult UpdateOne<TFilter, TField>(Expression<Func<TDocument, bool>> filterExpression, 
            Expression<Func<TDocument, TField>> memberExpression, TField value);

        /// <summary>
        /// update fieldset in the databse by provided id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fieldset"></param>
        ServiceResult GenericUpdate(string id, Dictionary<string, dynamic> fieldset);

        /// <summary>
        /// add an item to a list (if not exist)
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="id"></param>
        /// <param name="memberExpression"></param>
        /// <param name="value"></param>
        ServiceResult AddToList<TValue>(string id, Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value);

        /// <summary>
        /// remove an item from a list (all item if same)
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="id"></param>
        /// <param name="memberExpression"></param>
        /// <param name="value"></param>
        ServiceResult RemoveFromList<TValue>(string id, Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value);

        /// <summary>
        /// Deletes an item by id.
        /// </summary>
        /// <param name="id"></param>
        ServiceResult Delete(string id);

        /// <summary>
        /// Deletes multiple documents.
        /// </summary>
        /// <param name="predicate">represent expression to filter delete</param>
        ServiceResult DeleteMany(Expression<Func<TDocument, bool>> predicate);
    }
}
