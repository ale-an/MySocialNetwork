﻿using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories;

public class BaseRepository < T >: IRepository < T > where T: class 
{
    protected ApplicationDbContext _db;

    public DbSet < T > Set 
    {
        get;
        private set;
    }

    public BaseRepository(ApplicationDbContext db) 
    {
        _db = db;
        var set = _db.Set < T > ();
        set.Load();

        Set = set;
    }

    public void Create(T item) 
    {
        Set.Add(item);
        _db.SaveChanges();
    }

    public void Delete(T item) 
    {
        Set.Remove(item);
        _db.SaveChanges();
    }

    public T Get(int id) 
    {
        return Set.Find(id);
    }

    public IEnumerable < T > GetAll() 
    {
        return Set;
    }

    public void Update(T item) 
    {
        Set.Update(item);
        _db.SaveChanges();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}