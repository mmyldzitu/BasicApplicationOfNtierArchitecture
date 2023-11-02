using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAppNtierArchitecture.Business.Interfaces;
using TodoAppNtierArchitecture.Business.Services;
using TodoAppNtierArchitecture.DataAccess.Context;
using TodoAppNtierArchitecture.DataAccess.UnitOfWork;

namespace TodoAppNtierArchitecture.Business.DependencyResolvers.Microsoft
{
   public static class DependencyExtensions
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(opt =>
            {
                opt.UseSqlServer("server=LAPTOP-5VAB06RE\\SQLEXPRESS;database=Todo2Db; integrated security=true;");
                opt.LogTo(Console.WriteLine, LogLevel.Information);
            });
            services.AddScoped<IUow, Uow>();
            services.AddScoped<IWorkService, WorkService>();

        }
    }
}
