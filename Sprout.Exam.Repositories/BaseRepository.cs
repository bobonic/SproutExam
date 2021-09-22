using AutoMapper;
using Dapper.Contrib.Extensions;
using Sprout.Exam.Business.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Repositories
{
    public abstract class BaseRepository<TEntity, TDto>
              where TEntity : class
              where TDto : class, new()
    {

        protected IMapper _mapper;
        protected readonly IDbConnection _connection;

        public BaseRepository(IDbConnection connection, IMapper mapper)
        {
            _mapper = mapper;
            _connection = connection;
        }

        public async Task<bool> Create(TDto entity)
        {
            try
            {
                using (var conn = new SqlConnection(_connection.ConnectionString))
                {
                    conn.Open();
                    await conn.InsertAsync(_mapper.Map<TEntity>(entity));
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            using (var conn = new SqlConnection(_connection.ConnectionString))
            {
                conn.Open();

                var entity = await conn.GetAsync<TEntity>(id);
                if (entity != null)
                {
                    await conn.DeleteAsync(entity);
                }

                return true;
            }
        }

        public async Task<List<TDto>> GetAll()
        {
            using (var conn = new SqlConnection(_connection.ConnectionString))
            {
                conn.Open();
                var dtoList = new List<TDto>();
                var list = await conn.GetAllAsync<TEntity>();

                if (list != null)
                {
                    dtoList = _mapper.Map<List<TDto>>(list);
                }
                return dtoList;
            }
        }

        public async Task<TDto> GetById(int id)
        {
            var dtoItem = new TDto();

            using (var conn = new SqlConnection(_connection.ConnectionString))
            {
                conn.Open();

                var item = await conn.GetAsync<TEntity>(id);
                if (dtoItem != null)
                {
                    dtoItem = _mapper.Map<TDto>(item);
                }
            }
            return dtoItem;
        }

        public async Task<bool> Update(int id, TDto entity)
        {
            using (var conn = new SqlConnection(_connection.ConnectionString))
            {
                conn.Open();

                var item = await conn.GetAsync<TEntity>(id);

                if (item != null)
                {
                    _mapper.Map(entity, item, typeof(TDto), typeof(TEntity));
                    await conn.UpdateAsync(item);
                }

                return true;
            }

        }
    }
}
