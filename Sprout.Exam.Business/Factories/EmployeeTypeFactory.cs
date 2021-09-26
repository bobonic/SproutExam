using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Factories
{
    public abstract class EmployeeTypeFactory
    {
        public abstract EmployeeType Calculate();
    }
}
