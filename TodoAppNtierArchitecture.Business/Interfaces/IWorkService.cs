using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAppNtierArchitecture.Dtos.Interfaces;
using TodoAppNtierArchitecture.Dtos.WorkDtos;

namespace TodoAppNtierArchitecture.Business.Interfaces
{
    public interface IWorkService
    {
        Task<List<WorkListDto>> GetAll();
        Task Create(WorkCreateDto dto);
        Task<IDto> GetById<IDto>(int id);
        Task Remove(int id);
        Task Update(WorkUpdateDto dto);
    }
}
