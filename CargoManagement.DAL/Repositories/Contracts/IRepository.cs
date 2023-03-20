using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CargoManagement.DAL.Models;

namespace CargoManagement.DAL.Repositories.Contracts
{
    public interface IRepository<T> : IUnitOfWork where T : class
    {
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
    }
}
