using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Repositories;
using Sprout.Exam.Business.Factories;

using CommonEnum = Sprout.Exam.Common.Enums;
using Sprout.Exam.WebApp.Models;

namespace Sprout.Exam.WebApp.Controllers
{
    public class EmployeesController : BaseController<IEmployeeRepository>
    {
        public EmployeesController(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {

        }
        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _repository.GetAll();
            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _repository.GetById(id);
            result.Birthdate = DateTime.Parse(result.Birthdate).ToString("yyyy-MM-dd");
            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EmployeeDto input)
        {
            var result = await _repository.Update(input.Id, input);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest(false);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(EmployeeDto input)
        {
            var result = await _repository.Create(input);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest(false);
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.Delete(id);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest(false);
        }



        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate(CalculateRequestModel request)
        {
            EmployeeTypeFactory factory = null;


            var result = await _repository.GetById(request.Id);

            if (result == null) return NotFound();

            var type = (CommonEnum.EmployeeType)result.TypeId;

            switch (type)
            {
                case CommonEnum.EmployeeType.Regular:
                    factory = new RegularEmployeeFactory((decimal)EmployeeTypeRates.Regular, request.Days, 12);
                    break;

                case CommonEnum.EmployeeType.Contractual:
                    factory = new ContractualEmployeeFactory((decimal)EmployeeTypeRates.Contractual, request.Days);
                    break;
            }

            var item = factory.Calculate();
            return Ok(item.Salary);

        }

    }
}
