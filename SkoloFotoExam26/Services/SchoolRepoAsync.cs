using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Services
{
    public class SchoolRepoAsync : SofieConnectionString, IRepoAsync<School, int>
    {
        public Task AddAsync(School input)
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

        public Task<List<School>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<School> GetAsync(int toGet)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(School toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
