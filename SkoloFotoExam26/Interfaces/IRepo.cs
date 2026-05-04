using SkoloFotoExam26.Services;

namespace SkoloFotoExam26.Interfaces
{
    public interface IRepo<K, V>
    {
        int Count { get ; }

        List<V> GetAll();

        void AddKeyValue(K theKey, V theValue);

        void Delete(K theKey);

        V Get(K theKey);

        public bool Update(K theKey, V theValue);

        public void PrintAll();


    }
}
