using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotrADatabase.Models
{
    public class DotrADbRepostory<T> where T : class
    {
        private DotrADbContext _context;

        //這是後來需要加的部分，讓子類別可以使用 _context 欄位
        protected DotrADbContext Context
        { get { return _context; } }
        // ....
        public DotrADbRepostory(DotrADbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }
            _context = context;
        }
        public void Create(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }
    }
}
