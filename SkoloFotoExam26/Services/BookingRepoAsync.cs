using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using System.Data;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace SkoloFotoExam26.Services
{
    public class BookingRepoAsync : IRepoAsync<Booking, int>
    {

        private string _getAll = "SELECT * FROM Booking";
        public Task AddAsync(Booking input)
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

        public async Task<List<Booking>> GetAllAsync()
        {
            List<Booking> bookings = new List<Booking>();
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getAll, connection);
                    command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        DateTime start = reader.GetDateTime("Start");
                        DateTime end = reader.GetDateTime("End");
                        
                        //Booking booking = new Booking(start, end, new PhotographingEvent(), 
                        //    new Photographer(), new SchoolClass());

                        //bookings.Add(booking);
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
                return bookings;
            }

        }

        public Task<Booking> GetAsync(int toGet)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Booking toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
