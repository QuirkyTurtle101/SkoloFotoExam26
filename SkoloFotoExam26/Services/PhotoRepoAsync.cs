using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Services
{
    public class PhotoRepoAsync : IRepoAsync<Photo, int>
    {
        #region Query strings
        
        #endregion

        public Task AddAsync(Photo input)
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

        public Task<List<Photo>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Photo> GetAsync(int toGet)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Photo toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
