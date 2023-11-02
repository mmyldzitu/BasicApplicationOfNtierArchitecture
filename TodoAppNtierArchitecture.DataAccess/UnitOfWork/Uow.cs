using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAppNtierArchitecture.DataAccess.Context;
using TodoAppNtierArchitecture.DataAccess.Interfaces;
using TodoAppNtierArchitecture.DataAccess.Repositories;
using TodoAppNtierArchitecture.Entities.Concrete;

namespace TodoAppNtierArchitecture.DataAccess.UnitOfWork
{
    public class Uow : IUow
    {
        private readonly TodoContext _todoContext;

        public Uow(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return new Repository<T>(_todoContext);
        }

        public async Task SaveChanges()
        {
            await _todoContext.SaveChangesAsync();
        }
    }
}
