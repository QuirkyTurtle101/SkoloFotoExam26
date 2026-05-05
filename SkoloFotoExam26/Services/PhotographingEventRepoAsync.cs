using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Services
{
    public class PhotographingEventRepoAsync : AkselConnectionString, IPhotographingEventRepoAsync
    {

        private string _addEvent = "INSERT INTO PhotographingEvent VALUES(@Start, @End, @SchoolSecretaryID, @PhotographerID)";

        public async Task AddAsync(PhotographingEvent input)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_addEvent, connection);
                    await command.Connection.OpenAsync();
                    command.Parameters.AddWithValue("@Start", input.Start);
                    command.Parameters.AddWithValue("@End", input.End);
                    command.Parameters.AddWithValue("@SchoolSecretaryID", input.SchoolSecretary.SchoolSecretaryID);
                    command.Parameters.AddWithValue("@PhotographerID", input.Photographer.PhotographerID);
                }
                catch (SqlException sqlExc)
                {
                    Console.WriteLine($"sql exception message: {sqlExc.Message}");
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"General exception: {ex.Message}");
                }
            }
        }


        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int toDelete)
        {
            throw new NotImplementedException();
        }

        public Task<List<PhotographingEvent>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PhotographingEvent> GetAsync(int toGet)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(PhotographingEvent toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
