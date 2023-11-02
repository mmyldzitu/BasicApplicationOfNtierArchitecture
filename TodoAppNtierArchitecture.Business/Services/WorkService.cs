using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAppNtierArchitecture.Business.Interfaces;
using TodoAppNtierArchitecture.Business.ValidationRules;
using TodoAppNtierArchitecture.DataAccess.UnitOfWork;
using TodoAppNtierArchitecture.Dtos.Interfaces;
using TodoAppNtierArchitecture.Dtos.WorkDtos;
using TodoAppNtierArchitecture.Entities.Concrete;

namespace TodoAppNtierArchitecture.Business.Services
{

    public class WorkService : IWorkService
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        private readonly IValidator<WorkUpdateDto> _updateDtoValidator;
        private readonly IValidator<WorkCreateDto> _createDtoValidator;

        public WorkService(IUow uow, IMapper mapper, IValidator<WorkUpdateDto> updateDtoValidator, IValidator<WorkCreateDto> createDtoValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _updateDtoValidator = updateDtoValidator;
            _createDtoValidator = createDtoValidator;
        }

        public async Task Create(WorkCreateDto dto)
        {
            var validationResult = _createDtoValidator.Validate(dto);
            if (validationResult.IsValid)
            {
                await _uow.GetRepository<Work>().Create(_mapper.Map<Work>(dto));
                await _uow.SaveChanges();
            }
           
        }

        public async  Task<List<WorkListDto>> GetAll()
        {

            return _mapper.Map<List<WorkListDto>>(await _uow.GetRepository<Work>().GetAll());
        }

        public async Task<IDto> GetById<IDto>(int id)
        {
            return _mapper.Map<IDto>(await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id));
           
        }

        public async Task Remove(int id)
        {
            _uow.GetRepository<Work>().Remove(id);
           await _uow.SaveChanges();
        }

        public async  Task Update(WorkUpdateDto dto)
        {
            var validationResult = _updateDtoValidator.Validate(dto);
            if (validationResult.IsValid)
            {
                _uow.GetRepository<Work>().Update(_mapper.Map<Work>(dto));
                await _uow.SaveChanges();
            }
             
        }
    }
}
