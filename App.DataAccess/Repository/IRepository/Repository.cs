﻿using App.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.DataAccess.Repository.IRepository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    private DbSet<T> dbSet;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        this.dbSet=_db.Set<T>();
    }

    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public IEnumerable<T> GetAll(string? includeProperties=null)
    {
        IQueryable<T> query = dbSet;
        if(includeProperties != null)
        {
            foreach (var item in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(item);
            }
        }
        return query.ToList();
    }

    public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null)
    {
        IQueryable<T> query = dbSet;
        if(filter != null)
        {
            query = query.Where(filter);
        }
        return query.FirstOrDefault();
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        dbSet.RemoveRange(entities);
    }

}
