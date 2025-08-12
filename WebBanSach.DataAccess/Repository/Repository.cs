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
        //includePr: include properties to be included in the query, e.g. "Category,CoverType"
        public IEnumerable<T> GetAll(string? includePr = null)
        {
            IQueryable<T> query = Dbset;
            if (includePr != null)
            {
                foreach (var includeProperty in includePr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.ToList();
        }
        public T GetFirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> filter, string? includePr = null)
        {
            IQueryable<T> query = Dbset;
            if (includePr != null)
            {
                foreach (var includeProperty in includePr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
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
