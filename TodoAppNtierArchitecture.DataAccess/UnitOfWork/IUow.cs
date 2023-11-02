using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAppNtierArchitecture.DataAccess.Interfaces;
using TodoAppNtierArchitecture.Entities.Concrete;

namespace TodoAppNtierArchitecture.DataAccess.UnitOfWork
{
    public interface IUow
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity;
        Task SaveChanges();
    }
}
