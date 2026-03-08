using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations
{
    public class GenericRepository<T>:IRepository<T> where T: class
    {
        private readonly Context context;
        public GenericRepository(Context context)
        {
            this.context = context;
        }
        public virtual void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }
        public virtual void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }
        public virtual T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }
        public virtual void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }

        public virtual IQueryable<T> Query()
        {
            return context.Set<T>();
        }
    }
        
    
}
