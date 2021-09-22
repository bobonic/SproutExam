using Sprout.Exam.Business.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Repositories
{
    public interface IBaseRepository<TEntity, TDto> : IRepository
        where TEntity : class
        where TDto : class

    {
        Task<List<TDto>> GetAll();

        Task<TDto> GetById(int id);

        Task<bool> Create(TDto entity);

        Task<bool> Update(int id, TDto entity);

        Task<bool> Delete(int id);

    }
}
