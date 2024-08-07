﻿using DAL.Models;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class RepositoryGeneric<T> : IRepositoryGeneric<T> where T : class
    {
        public JewelryAuctionContext _context;
        public DbSet<T> _dbSet;
        

        public RepositoryGeneric(JewelryAuctionContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        
        public async Task<int> AddAsync(T entity)
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync();
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public DbSet<T> Entities()
        {
            return _context.Set<T>();
        }
        
        public async Task<bool> RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateAsync(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        
    }
}
