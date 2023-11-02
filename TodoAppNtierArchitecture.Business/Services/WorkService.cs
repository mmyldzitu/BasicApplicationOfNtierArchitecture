using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAppNtierArchitecture.Business.Interfaces;
using TodoAppNtierArchitecture.DataAccess.UnitOfWork;
using TodoAppNtierArchitecture.Dtos.WorkDtos;
using TodoAppNtierArchitecture.Entities.Concrete;

namespace TodoAppNtierArchitecture.Business.Services
{
    public class WorkService : IWorkService
    {
        private readonly IUow _uow;

        public WorkService(IUow uow)
        {
            _uow = uow;
        }

        public async Task Create(WorkCreateDto dto)
        {
           await _uow.GetRepository<Work>().Create(new()
            {
                Definition = dto.Definition,
                isCompleted = dto.isCompleted
            });
            await _uow.SaveChanges();
        }

        public async  Task<List<WorkListDto>> GetAll()
        {
            var list=await _uow.GetRepository<Work>().GetAll();
            var workList = new List<WorkListDto>();
            if (list !=null && list.Count > 0)
            {
                foreach(var work in list)
                {
                    workList.Add(new()
                    {
                        Definition = work.Definition,
                        Id = work.Id,
                        isCompleted = work.isCompleted
                    });
                }
            }
            return workList;
        }

        public async Task<WorkListDto> GetById(int id)
        {
            var data = await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id);
            return new()
            {
                
                Definition = data.Definition,
                isCompleted = data.isCompleted
            };
        }

        public async Task Remove(int id)
        {
            _uow.GetRepository<Work>().Remove(id);
           await _uow.SaveChanges();
        }

        public async  Task Update(WorkUpdateDto dto)
        {
            
             _uow.GetRepository<Work>().Update(new()
            {
                Definition = dto.Definition,
                Id = dto.Id,
                isCompleted = dto.isCompleted
            });
             await _uow.SaveChanges();
        }
    }
}
