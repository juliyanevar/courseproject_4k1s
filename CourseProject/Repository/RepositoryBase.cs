using CourseProject.Models;
using CourseProject.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CourseProject.Repository
{
    public abstract class RepositoryBase<T>: IRepositoryBase<T> where T : class
    {
        protected ApplicationContext ApplicationContext { get; set; }

        public RepositoryBase(ApplicationContext applicationContext)
        {
            ApplicationContext = applicationContext;
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await ApplicationContext.Set<T>().AsNoTracking().ToListAsync<T>();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await ApplicationContext.Set<T>().Where(expression).AsNoTracking().ToListAsync<T>();
        }

        public async Task<T> FindFirstByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await ApplicationContext.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task CreateAsync(T entity)
        {
            await ApplicationContext.Set<T>().AddAsync(entity);
            await ApplicationContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            ApplicationContext.Set<T>().Update(entity);
            await ApplicationContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            ApplicationContext.Set<T>().Remove(entity);
            await ApplicationContext.SaveChangesAsync();
        }
    }
}
