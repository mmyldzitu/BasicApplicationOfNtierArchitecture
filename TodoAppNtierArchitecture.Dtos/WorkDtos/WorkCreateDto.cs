using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAppNtierArchitecture.Dtos.Interfaces;

namespace TodoAppNtierArchitecture.Dtos.WorkDtos
{
   public class WorkCreateDto:IDto
    {
        public string Definition { get; set; }
        public bool isCompleted { get; set; }
    }
}
