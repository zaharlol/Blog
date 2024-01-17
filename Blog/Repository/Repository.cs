using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Blog.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext _db;

        public Repository(DataContext db)
        {
            _db = db;
        }

        public void Create(T item)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public T Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Update(T item)
        {
            throw new System.NotImplementedException();
        }
    }
}
