using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Helpers
{
    public class RegularEmployeeFactory : EmployeeTypeFactory
    {
        private decimal rate;
        private decimal days;
        private decimal tax;

        public RegularEmployeeFactory(decimal rate, decimal days, decimal tax)
        {
            this.rate = rate;
            this.days = days;
            this.tax = tax;
        }

        public override EmployeeType Calculate()
        {
            return new RegularEmployee(rate, days, tax);
        }
    }
}
