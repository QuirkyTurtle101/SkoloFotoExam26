namespace SkoloFotoExam26.Interfaces
{
    /// <summary>
    /// Interface for async repositories. Use this for repositories, particularly those accessing the MS-SQL database.
    /// </summary>
    /// <typeparam name="T">Type parameter for the model which a given implementation is intended to handle.</typeparam>
    /// <typeparam name="PK">Type parameter corresponding to the primary key of the SQL table this repository accesses. Should generally be int, except in special cases.</typeparam>
    public interface IRepoAsync<T, PK>
    {
        public Task<int> CountAsync();

        public Task<T> GetAsync(PK toGet);

        public Task<List<T>> GetAllAsync();

        public Task AddAsync(T input);

        public Task DeleteAsync(PK toDelete);

        public Task UpdateAsync(T toUpdate);
    }
}
