using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Core.Interfaces
{
    public  interface IRepository<Entity> where Entity : class
    {
        Task<Entity> GetByIdAsync(int id);
        IQueryable<Entity> GetAll();
        Task<bool> AddAsync(Entity entity);
        Task<bool> UpdateAsync(Entity entity);
        Task<bool> HardDeleteAsync(int id);
        Task<bool> SoftDeleteAsync(int id);
        Task SaveChangesAsync();
        Task<bool> ExistsAsync(int id);
        Task<IQueryable<Entity>> GetAsync(Expression<Func<Entity, bool>> predicate);
        Task<bool> Update(Entity entity);
        Task<bool> SaveIncludeAsync(Entity entity, params string[] Properties);
    }
}
