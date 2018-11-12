using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using SportFixtures.Data.Entities;

namespace SportFixtures.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets all 
        /// </summary>
        /// <param name="filter">Lambda expresion to filter by. Defalut null.</param>
        /// <param name="orderBy">Parameter to order by. Defalut null.</param>
        /// <param name="includeProperties">Entity Property to include from dtabase.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(
                Expression<Func<TEntity, bool>> filter = null,
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                string includeProperties = "");

        /// <summary>
        /// Find object by its id.
        /// </summary>
        /// <param name="id">Entity.Id</param>
        /// <returns></returns>
        TEntity GetById(object id);

        /// <summary>
        /// Add to data base
        /// </summary>
        /// <param name="entityToCreate">Entity to persist</param>
        void Insert(TEntity entityToCreate);

        /// <summary>
        /// Delete object by using its Id
        /// </summary>
        /// <param name="id">Entity.Id</param>
        void Delete(object id);

        /// <summary>
        /// Delete object by sending the object itself.
        /// </summary>
        /// <param name="entityToDelete">Database object</param>
        void Delete(TEntity entityToDelete);

        /// <summary>
        /// Modify object
        /// </summary>
        /// <param name="entityToUpdate">Modified Object</param>
        void Update(TEntity entityToUpdate);

        /// <summary>
        /// Saves changes
        /// </summary>
        void Save();

        void Attach(TEntity entity);

        void Dispose();
    }
}
