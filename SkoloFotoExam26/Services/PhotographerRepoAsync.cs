using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using System.Data;

namespace SkoloFotoExam26.Services
{
    public class PhotographerRepoAsync : ConnectionString, IPhotographerRepoAsync
    {

        private string _getPhotographer = "SELECT * FROM Photographer WHERE PhotographerID = @PhotographerID";

        public Task AddAsync(Photographer input)
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

        public Task<List<Photographer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Photographer> GetAsync(int toGet)
        {
            Photographer photographer = null;
            using SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using SqlCommand command = new SqlCommand(_getPhotographer, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@PhotographerID", toGet);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.Read())
                {
                    string firstName = reader.GetString("FirstName");
                    string lastName = reader.GetString("LastName");
                    string email = reader.GetString("E-mail");
                    string phoneNumber = reader.GetString("PhoneNumber");
                    string website = reader.GetString("Website");
                    string cvrNumber = reader.GetString("CVRNumber");
                    string city = reader.GetString("City");
                    string postCode = reader.GetString("PostCode");
                    string streetName = reader.GetString("StreetName");
                    int experienceInYears = reader.GetInt32("ExperienceInYears");
                    int maxTravelRadiusInKm = reader.GetInt32("MaxTravelRadiusInKm");
                    string instagram = reader.GetString("Instagram");
                    string facebook = reader.GetString("Facebook");
                    photographer = new Photographer(firstName, lastName, phoneNumber, email, website, cvrNumber, city, postCode,
                        streetName, experienceInYears, maxTravelRadiusInKm, instagram, facebook);
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Exception message: {sqlEx.Message}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception message: {ex.Message}");
            }
            return photographer;
        }

        public Task UpdateAsync(Photographer toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
