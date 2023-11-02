using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAppNtierArchitecture.Dtos.WorkDtos;

namespace TodoAppNtierArchitecture.Business.ValidationRules
{
    public class WorkCreateDtoValidator : AbstractValidator<WorkCreateDto>
    {
        public WorkCreateDtoValidator()
        {
            RuleFor(x => x.Definition).NotEmpty().WithMessage("Definition is required").Must(nottobemuhammed).WithMessage("Definition 'muhammed' olamaz");
        }

        private bool nottobemuhammed(string arg)
        {
            return arg != "muhammed" && arg != "MUHAMMED";
        }
    }
}
