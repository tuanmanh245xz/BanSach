using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSach.DataAccess.Repository.IRepository;

namespace WebBanSach.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        
        private readonly ApplicationDbContext _db;
        internal DbSet<T> Dbset;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.Dbset = _db.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = Dbset;
            return query.ToList();
        }
        public T GetFirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = Dbset;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }
        public void Add(T entity)
        {
           Dbset.Add(entity);
        }
        public void Update(T entity)
        {
            _db.Set<T>().Update(entity);
            Save();
        }
        public void Remove(T entity)
        {
            Dbset.Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            Dbset.RemoveRange(entities);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
    
    
}
