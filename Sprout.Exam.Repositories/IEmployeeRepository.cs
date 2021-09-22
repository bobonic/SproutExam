using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Repositories
{
    public interface IEmployeeRepository : IBaseRepository<Employee, EmployeeDto>
    {
    }
}
