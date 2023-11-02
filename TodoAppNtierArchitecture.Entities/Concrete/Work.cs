using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoAppNtierArchitecture.Entities.Concrete
{
    public class Work:BaseEntity
    {
        
        public string Definition { get; set; }
        public bool isCompleted { get; set; }
    }
}
