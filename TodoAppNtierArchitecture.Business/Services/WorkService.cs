﻿using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAppNtierArchitecture.Business.Extensions;
using TodoAppNtierArchitecture.Business.Interfaces;
using TodoAppNtierArchitecture.Business.ValidationRules;
using TodoAppNtierArchitecture.Common.ResponseObjects;
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

        public async Task<IResponse<WorkCreateDto>> Create(WorkCreateDto dto)
        {
            var validationResult = _createDtoValidator.Validate(dto);
            if (validationResult.IsValid)
            {
                await _uow.GetRepository<Work>().Create(_mapper.Map<Work>(dto));
                await _uow.SaveChanges();
                return new Response<WorkCreateDto>(ResponseType.Success, dto);
            }
            else
            { 
                return new Response<WorkCreateDto>(ResponseType.ValidationError, dto, validationResult.ConvertToCustomValidationError());
            }
        }

        public async  Task<IResponse<List<WorkListDto>>> GetAll()
        {

            var data= _mapper.Map<List<WorkListDto>>(await _uow.GetRepository<Work>().GetAll());
            return new Response<List<WorkListDto>>(ResponseType.Success, data);
        }

        public async Task<IResponse<IDto>> GetById<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id));
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id}'ye ait data bulunamadı");
            }
            return new Response<IDto>(ResponseType.Success, data);
           
        }

        public async Task<IResponse> Remove(int id)
        {
           var removedEntity=await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id);
            if (removedEntity != null)
            {
                _uow.GetRepository<Work>().Remove(removedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            return new Response($"{id}'ye ait data bulunamadı",ResponseType.NotFound );

        }

        public async  Task<IResponse<WorkUpdateDto>> Update(WorkUpdateDto dto)
        {
            var validationResult = _updateDtoValidator.Validate(dto);
            if (validationResult.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<Work>().Find(dto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<Work>().Update(_mapper.Map<Work>(dto),updatedEntity);
                    await _uow.SaveChanges();
                    return new Response<WorkUpdateDto>(ResponseType.Success, dto);
                }
                return new Response<WorkUpdateDto>(ResponseType.NotFound, $"{dto.Id}'ye ait data bulunamadı");
            }
            else
            {
               
                return new Response<WorkUpdateDto>(ResponseType.ValidationError, dto, validationResult.ConvertToCustomValidationError());
            }
             
        }
    }
}
