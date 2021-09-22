using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Helpers
{
    public abstract class EmployeeType
    {
        public abstract decimal Rate { set; }
        public abstract decimal Days {  set; }
        public abstract decimal Tax { set; }
        public abstract decimal Salary { get; }


    }
}
