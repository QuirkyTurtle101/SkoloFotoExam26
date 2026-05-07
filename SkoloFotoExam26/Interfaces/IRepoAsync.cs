using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Interfaces
{
    /// <summary>
    /// Interface for async repositories. Use this for repositories, particularly those accessing the MS-SQL database.
    /// </summary>
    /// <typeparam name="T">Type parameter for the model which a given implementation is intended to handle.</typeparam>
    /// <typeparam name="PK">Type parameter corresponding to the primary key of the SQL table this repository accesses. Should generally be int, except in special cases.</typeparam>
    public interface IRepoAsync<T, PK> : IRepoAsync
    {
        public Task<T> GetAsync(PK toGet);

        public Task<List<T>> GetAllAsync();

        public Task AddAsync(T input);

        public Task DeleteAsync(PK toDelete);

        public Task UpdateAsync(T toUpdate);
    }

    /// <summary>
    /// Interface for any type-agnostic operations we can do with IRepoAsync. Mostly exists to enable collections of different repositories.
    /// DO NOT IMPLEMENT THIS ANYWHERE ELSE OR I WILL BURN THE CLASSROOM DOWN
    /// </summary>
    public interface IRepoAsync
    {
        public Task<int> CountAsync();
    }

    /// <summary>
    /// Interface for user repositories, adding a method to allow the LoginHandler to get a specific user without knowing their exact UID. Since emails are PK on the login table, this should still be fine.
    /// </summary>
    public interface ILoginableRepo
    {
        public Task<User> GetForLogin(string email);
    }
}
