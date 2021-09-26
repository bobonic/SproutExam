using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Factories
{
    public class ContractualEmployee : EmployeeType
    {
        private readonly decimal salary;
        private decimal rate;
        private decimal days;
        private decimal tax;

        public ContractualEmployee(decimal rate, decimal days, decimal tax)
        {
            this.rate = rate;
            this.days = days;
            this.tax = tax;
        }

        public override decimal Salary
        {
            get
            {
                var salary = this.rate * this.days;

                return salary;
            }
        }
        public override decimal Rate
        {
            set { this.rate = value; }
        }

        public override decimal Days
        {
            set { this.days = value; }
        }

        public override decimal Tax
        {
            set { this.tax = value; }
        }
    }
}
