using AutoMapper;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.DataAccess;
using System;
using System.Data;

namespace Sprout.Exam.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee, EmployeeDto>, IEmployeeRepository
    {
        public EmployeeRepository(IDbConnection connection, IMapper mapper) : base(connection, mapper)
        {
        }
    }
}
