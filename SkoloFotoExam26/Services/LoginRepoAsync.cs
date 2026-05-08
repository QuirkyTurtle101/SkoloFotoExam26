using SkoloFotoExam26.Models;
using SkoloFotoExam26.Interfaces;

namespace SkoloFotoExam26.Services
{
    public class LoginRepoAsync : IRepoAsync<LoginInfo, string>
    {
        public Task AddAsync(LoginInfo input)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string toDelete)
        {
            throw new NotImplementedException();
        }

        public Task<List<LoginInfo>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<LoginInfo> GetAsync(string toGet)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(LoginInfo toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
