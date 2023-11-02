using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAppNtierArchitecture.DataAccess.Context;
using TodoAppNtierArchitecture.DataAccess.Interfaces;
using TodoAppNtierArchitecture.Entities.Concrete;

namespace TodoAppNtierArchitecture.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T: BaseEntity
    {
        private readonly TodoContext _todoContext;

        public Repository(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        public async Task Create(T entity)
        {
           await  _todoContext.Set<T>().AddAsync(entity);
        }

        public async Task<List<T>> GetAll()
        {
            return await _todoContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByFilter(System.Linq.Expressions.Expression<Func<T, bool>> filter, bool asNoTracking = false)
        {
            return asNoTracking ? await _todoContext.Set<T>().SingleOrDefaultAsync(filter) :
                await _todoContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter);
        }

        public async Task<T> GetById(object id)
        {
            return await _todoContext.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetQuery()
        {
            return _todoContext.Set<T>().AsQueryable();
        }

        public  void Remove(object id)
        {
            var silinecek=_todoContext.Set<T>().Find(id);
            _todoContext.Remove(silinecek);
        }

        public  void Update(T entity)
        {
            var guncellenecekVeri = _todoContext.Set<T>().Find(entity.Id);
            _todoContext.Entry(guncellenecekVeri).CurrentValues.SetValues(entity);
        }
    }
}
