using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAppNtierArchitecture.Business.Interfaces;
using TodoAppNtierArchitecture.Business.Mappings.AutoMapper;
using TodoAppNtierArchitecture.Business.Services;
using TodoAppNtierArchitecture.Business.ValidationRules;
using TodoAppNtierArchitecture.DataAccess.Context;
using TodoAppNtierArchitecture.DataAccess.UnitOfWork;
using TodoAppNtierArchitecture.Dtos.WorkDtos;

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

            var configuration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new WorkProfile());

            });
            var mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);
            services.AddScoped<IUow, Uow>();
            services.AddScoped<IWorkService, WorkService>();
            services.AddTransient<IValidator<WorkCreateDto>, WorkCreateDtoValidator>();
            services.AddTransient<IValidator<WorkUpdateDto>, WorkUpdateDtoValidator>();

        }
    }
}
