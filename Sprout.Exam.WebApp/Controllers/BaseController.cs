using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TRepository> : ControllerBase
    where TRepository : IRepository
    {
        public BaseController(TRepository repository)
        {
            _repository = repository;
        }


        protected TRepository _repository;
    }
}
