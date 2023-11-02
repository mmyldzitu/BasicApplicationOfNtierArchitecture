using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoAppNtierArchitecture.Dtos.WorkDtos
{
   public  class WorkUpdateDto
    {
        public int Id { get; set; }
        public string Definition { get; set; }
        public bool isCompleted { get; set; }
    }
}
