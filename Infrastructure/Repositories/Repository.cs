using Core.Entities;
using HotelReservation.Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Infrastructure.Repositories
{
    public class Repository<Entity> : IRepository<Entity> where Entity : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Entity> _dbSet;
        private readonly string[] immutableProps = { nameof(BaseEntity.Id), nameof(BaseEntity.CreatedBy), nameof(BaseEntity.UpdatedAt) };
        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Entity>();
        }
        public async Task<Entity> GetByIdAsync(int id)
        {
            var entity = await _dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
            return entity;
        }
        public IQueryable<Entity> GetAll()
        {
            //var entities = _dbSet.A;
            var entities = _dbSet.AsQueryable();
            return entities;
        }
        public async Task<bool> AddAsync(Entity entity)
        {
            if (entity == null)
            {
                return false;
            }
            await _dbSet.AddAsync(entity);
            return true;
        }
        public async Task<bool> UpdateAsync(Entity entity)
        {
            if (entity == null) return false;
            _dbSet.Update(entity);
            return true;
        }
        public async Task<bool> HardDeleteAsync(int id)
        {
            if (id == 0) return false;
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;
            _dbSet.Remove(entity);
            return true;

        }
        public async Task<bool> SoftDeleteAsync(int id)
        {
            if (id == 0) return false;
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;
            entity.IsDeleted = true;
            await SaveIncludeAsync(entity, nameof(entity.IsDeleted));
            return true;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public Task<bool> ExistsAsync(int id)
        {
            return _dbSet.Where(i => i.Id == id).AnyAsync();
        }
        public async Task<IQueryable<Entity>> GetAsync(Expression<Func<Entity, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }
        public async Task<bool> Update(Entity entity)
        {
            if (entity == null)
            {
                return false;
            }
            _dbSet.Update(entity);
            return true;
        }
        public async Task<bool> SaveIncludeAsync(Entity entity, params string[] properties)
        {
            try
            {
                var localEntity = _dbSet.Local.FirstOrDefault(e => e.Id == entity.Id);
                EntityEntry entry;

                if (localEntity is null)
                {
                    _dbSet.Attach(entity);
                    entry = _context.Entry(entity);
                }
                else
                {
                    entry = _context.Entry(localEntity);
                }

                if (entry == null)
                {
                    return false;
                }


                foreach (var property in entry.Properties)
                {
                    if (properties.Contains(property.Metadata.Name) && !immutableProps.Contains(property.Metadata.Name))
                    {
                        property.CurrentValue = entity.GetType().GetProperty(property.Metadata.Name)?.GetValue(entity);
                        property.IsModified = true;
                    }
                }

                entity.UpdatedAt = DateTime.UtcNow;
                entry.Property(nameof(entity.UpdatedBy)).IsModified = true;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
