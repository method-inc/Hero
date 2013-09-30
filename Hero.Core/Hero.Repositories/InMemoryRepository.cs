using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hero.Repositories
{
    public class InMemoryRepository
    {
        private readonly IList<object> _repository = new List<object>();

        public IQueryable<T> Get<T>() where T : class
        {
            return _repository.OfType<T>().ToList().AsQueryable();
        }

        public void Create<T>(T item) where T : class
        {
            if (!Get<T>().Any(e => e.Equals(item)))
                _repository.Add(item);
        }

        public void Create<T>(IEnumerable<T> items) where T : class
        {
            foreach (var item in items)
                Create(item);
        }

        public void Update<T>(T item) where T : class
        {
            int i = _repository.IndexOf(item);
            _repository.RemoveAt(i);
            _repository.Add(item);
        }

        public void Update<T>(IEnumerable<T> items) where T : class
        {
            foreach (var item in items)
                Update(item);
        }

        public void Delete<T>(T item) where T : class
        {
            _repository.Remove(item);
        }

        public void Delete<T>(IEnumerable<T> items) where T : class
        {
            foreach (var item in items)
                Delete(item);
        }

        public void Refresh<T>(T item) where T : class
        {
            Update(item);
        }
    }
}
