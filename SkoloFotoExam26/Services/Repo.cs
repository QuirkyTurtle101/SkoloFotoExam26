using SkoloFotoExam26.Interfaces;

namespace SkoloFotoExam26.Services
{
    public class Repo<K, V> : IRepo<K, V>
    {
        private Dictionary<K, V> _repo;

        public Repo()
        {
            _repo = new Dictionary<K, V>();
        }

        public int Count { get { return _repo.Values.Count; } }

        public List<V> GetAll()
        {
            return _repo.Values.ToList(); 
        }

        public void AddKeyValue(K theKey, V theValue)
        {
            if (!_repo.ContainsKey(theKey))
            {
                _repo.Add(theKey, theValue);
            }
        }

        public void Delete(K theKey)
        {
            if (_repo.ContainsKey(theKey))
            {
                _repo.Remove(theKey);
            }
        }

        public V Get(K theKey)
        {
            if (_repo.ContainsKey(theKey))
            {
                return _repo[theKey];
            }
            throw new Exception("Value not found.");
        }

        public bool Update(K theKey, V theValue)
        {
            if (_repo.ContainsKey(theKey))
            {
                _repo[theKey] = theValue;
                return true;
            }
            return false;
        }
        
        public void PrintAll()
        {
            foreach (V item in _repo.Values)
            {
                Console.WriteLine(item);
            }
        }
    }
}
