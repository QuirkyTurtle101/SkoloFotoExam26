using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using System.Data;

namespace SkoloFotoExam26.Services
{
    public class PhotographingEventRepoAsync : IRepoAsync<PhotographingEvent, int>
    {

        private string _addEvent = "INSERT INTO PhotographingEvent VALUES(@Start, @End, @SchoolSecretaryID, @PhotographerID)";
        private string _getAll = "SELECT * FROM PhotographingEvent";
        private string _getEvent = "SELECT * FROM PhotographingEvent WHERE PhotographingEventID = @PhotographingEventID";

        //private string _getAll = "SELECT SchoolSecretary.FirstName, SchoolSecretary.LastName, SchoolSecretary.SchoolSecretaryID, SchoolSecretary.Email, SchoolSecretary.PhoneNumber, SchoolSecretary.Initials, School.SchoolID, School.Name, School.StreetName, School.ZipCode, School.SchoolType, ZipCodeLookup.City FROM SchoolSecretary JOIN School on School.SchoolID = SchoolSecretary.SchoolID JOIN ZipCodeLookup ON School.ZipCode = ZipCodeLookup.ZipCode";

        private IRepoAsync<SchoolSecretary, int> _schoolSecretaryRepo;
        private IRepoAsync<Photographer, int> _photographerRepo;
        public PhotographingEventRepoAsync(IRepoAsync<SchoolSecretary, int> schoolSecretaryRepo, IRepoAsync<Photographer, int> photographerRepo)
        {
            _schoolSecretaryRepo = schoolSecretaryRepo;
            _photographerRepo = photographerRepo;
        }

        public async Task AddAsync(PhotographingEvent input)
        {
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_addEvent, connection);
                    await command.Connection.OpenAsync();
                    command.Parameters.AddWithValue("@Start", input.Start);
                    command.Parameters.AddWithValue("@End", input.End);
                    command.Parameters.AddWithValue("@SchoolSecretaryID", input.SchoolSecretary.SchoolSecretaryID);
                    command.Parameters.AddWithValue("@PhotographerID", input.Photographer.PhotographerID);

                    await command.ExecuteNonQueryAsync();
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

        public async Task<List<PhotographingEvent>> GetAllAsync()
        {
            List<PhotographingEvent> photographingEvents = new List<PhotographingEvent>();
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getAll, connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        int photographingEventID = reader.GetInt32("PhotographingEventID");
                        DateTime start = reader.GetDateTime("Start");
                        DateTime end = reader.GetDateTime("End");
                        int schoolSecretaryID = reader.GetInt32("SchoolSecretaryID");
                        int photographerID = reader.GetInt32("PhotographerID");

                        SchoolSecretary schoolSecretary = await _schoolSecretaryRepo.GetAsync(schoolSecretaryID);
                        Photographer photographer = await _photographerRepo.GetAsync(photographerID);
                        PhotographingEvent photographingEvent = new PhotographingEvent(photographingEventID, start, end, 
                            schoolSecretary, photographer);

                        photographingEvents.Add(photographingEvent);
                    }

                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine($"SQL Exception message: {sqlEx.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception message: {ex.Message}");
                }
            }
            return photographingEvents;
        }

        public async Task<PhotographingEvent> GetAsync(int toGet)
        {
            PhotographingEvent photographingEvent = null;
            using SqlConnection connection = new SqlConnection(Secret.connectionString);
            try
            {
                using SqlCommand command = new SqlCommand(_getEvent, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@PhotographingEventID", toGet);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.Read())
                {
                    int photographingEventID = reader.GetInt32("PhotographingEventID");
                    DateTime start = reader.GetDateTime("Start");
                    DateTime end = reader.GetDateTime("End");
                    int schoolSecretaryID = reader.GetInt32("SchoolSecretaryID");
                    int photographerID = reader.GetInt32("PhotographerID");

                    SchoolSecretary schoolSecretary = await _schoolSecretaryRepo.GetAsync(schoolSecretaryID);
                    Photographer photographer = await _photographerRepo.GetAsync(photographerID);
                    photographingEvent = new PhotographingEvent(photographingEventID, start, end,
                        schoolSecretary, photographer);

                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Exception message: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception message: {ex.Message}");
            }
            return photographingEvent;
        }

        public Task UpdateAsync(PhotographingEvent toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
