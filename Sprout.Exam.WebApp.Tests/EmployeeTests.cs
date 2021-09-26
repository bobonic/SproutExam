using Sprout.Exam.Business.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.WebApp.Tests
{
    public class EmployeeTests
    {
        [Fact]
        public void ShouldCalculateRegularSalary_Test()
        {
            EmployeeTypeFactory factory = new RegularEmployeeFactory(20000, 1, 12);
            var result = factory.Calculate();

            Assert.Equal(decimal.Parse("16690.91"), result.Salary, 2);
        }

        [Fact]
        public void ShouldCalculateContractualSalary_Test()
        {
            EmployeeTypeFactory factory = new ContractualEmployeeFactory(500, 14);
            var result = factory.Calculate();

            Assert.Equal(decimal.Parse("7000"), result.Salary, 2);
        }
    }
}
