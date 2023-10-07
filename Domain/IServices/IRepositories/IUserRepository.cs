﻿using System.Linq.Expressions;

namespace Domain.IServices.IRepositories
{
    public interface IUserRepository<TEntity> where TEntity : class
    {

        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetByIdAsync(string id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);

        void Remove(TEntity entity);

        TEntity Update(TEntity entity);


    }
}
