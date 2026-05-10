using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Services
{
    public class SchoolClassRepoAsync : IRepoAsync<SchoolClass, int>
    {
        #region Querys

        #endregion

        public Task AddAsync(SchoolClass input)
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

        public Task<List<SchoolClass>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SchoolClass> GetAsync(int toGet)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(SchoolClass toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
