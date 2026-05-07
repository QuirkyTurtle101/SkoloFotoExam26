using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Services
{
    public class TeacherRepoAsync : IRepoAsync<Teacher, int>, ILoginableRepo
    {
        public Task AddAsync(Teacher input)
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

        public Task<List<Teacher>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Teacher> GetAsync(int toGet)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetForLogin(string email)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Teacher toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
