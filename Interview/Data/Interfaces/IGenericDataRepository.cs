using Interview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Interview.Data.Interfaces
{
    public interface IGenericDataRepository<TEntity> where TEntity : class, IIdable
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(string id);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        bool Delete(string id);


    }
}