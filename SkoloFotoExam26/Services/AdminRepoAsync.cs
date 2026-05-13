using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Services
{
    public class AdminRepoAsync : IRepoAsync<Administrator, int>, ILoginableRepo
    {
        #region Query strings
        private string _addAdmin = "INSERT INTO ";

        #endregion


        public Task AddAsync(Administrator input)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int toDelete)
        {
            throw new NotImplementedException();
        }

        public Task<List<Administrator>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Administrator> GetAsync(int toGet)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetForLogin(string email)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Administrator toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
