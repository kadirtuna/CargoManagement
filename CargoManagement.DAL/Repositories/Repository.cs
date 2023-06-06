using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CargoManagement.DAL.DataContext;
using CargoManagement.DAL.Models;
using CargoManagement.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CargoManagement.DAL.Repositories
{
    public class Repository<T> : IRepository<T>, IUnitOfWork where T : class
    {
        CargoManagementDbContext _dbContext;

        public Repository(CargoManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Insert(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }
        public Task Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask; 
        }
        public Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task CommitAsync(bool state = true)
        {
            await _dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
