using HolidayOptimizations.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HolidayOptimizations.StorageRepository.DataRepositoryInterface
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        /// <summary>
        /// Insert entity in db
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert(T entity);

        /// <summary>
        /// Insert entities in db
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        void Insert(List<T> entities);

        /// <summary>
        /// Asynchronously Insert entities in db
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        void InsertAsync(List<T> entities);

        /// <summary>
        /// Delete an entity from db
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Delete(T entity);

        /// <summary>
        /// Delete db entities that match the conditions
        /// </summary>
        /// <param name="where"></param>
        /// <param name="paramValues"></param>
        /// <returns></returns>
        void Delete(string @where, object paramValues);

        /// <summary>
        /// Search for entity
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<T> GetAll(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Return an entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        /// Return an entity that match the criteria
        /// </summary>
        /// <param name="select"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
     }
}
