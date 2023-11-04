﻿using Core.Abstract.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstract.DataAccess
{
    public interface IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);
        TEntity Get(Expression<Func<TEntity, bool>> filter);
        TEntity Add(TEntity entity);
        void AddAll(List<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteAll(List<TEntity> entities);
        void Update(TEntity entity);
        void UpdateAll(List<TEntity> entities);
    }
}
