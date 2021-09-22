using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Helpers
{
    public class RegularEmployee : EmployeeType
    {
        private readonly decimal salary;
        private decimal rate;
        private decimal days;
        private decimal tax;

        public RegularEmployee(decimal rate, decimal days, decimal tax)
        {
            this.rate = rate;
            this.days = days;
            this.tax = tax;
        }

        public override decimal Salary
        {
            get 
            {
                var tax = this.rate * (this.tax / 100);
                var dailyRate = (this.rate / 22) * days;
                var salary = rate - (dailyRate + tax);

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
