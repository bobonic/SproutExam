using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Helpers
{
    public class ContractualEmployeeFactory : EmployeeTypeFactory
    {
        private decimal rate;
        private decimal days;
        private decimal tax;

        public ContractualEmployeeFactory(decimal rate, decimal days)
        {
            this.rate = rate;
            this.days = days;
        }

        public override EmployeeType Calculate()
        {
            return new ContractualEmployee(rate, days, tax);
        }
    }
}
