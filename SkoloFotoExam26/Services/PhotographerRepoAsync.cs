using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using System.Data;

namespace SkoloFotoExam26.Services
{
    public class PhotographerRepoAsync : IRepoAsync<Photographer, int>, ILoginableRepo
    {
        #region Querys

        private string _addPhotographer = "INSERT INTO Photographer VALUES(@FirstName, @LastName, @Email, @PhoneNumber,@WebSite, @CVRNumber, @StreetName, @ExperienceInYears, @MaxTravelRadiusInKm, @Instagram, @Facebook)";
        private string _getPhotographer = "SELECT * FROM Photographer WHERE PhotographerID = @PhotographerID";
        private string _getAll = "SELECT * FROM Photographer";

        #endregion
        public async Task AddAsync(Photographer input)
        {
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_addPhotographer, connection);
                    await connection.OpenAsync();

                    command.Parameters.AddWithValue("@FirstName", input.FirstName);
                    command.Parameters.AddWithValue("@LastName", input.LastName);
                    command.Parameters.AddWithValue("@Email", input.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", input.PhoneNumber);
                    command.Parameters.AddWithValue("@StreetName", input.Street);
                    command.Parameters.AddWithValue("@WebSite", input.Website);
                    command.Parameters.AddWithValue("@CVRNumber", input.CVRNumber);
                    command.Parameters.AddWithValue("@ExperienceInYears", input.ExperienceInYears);
                    command.Parameters.AddWithValue("@MaxTravelRadiusInKm", input.MaxTravelRadiusInKm);
                    command.Parameters.AddWithValue("@Instagram", input.Instagram);
                    command.Parameters.AddWithValue("@Facebook", input.Facebook);

                    int noOfRowsEffected = await command.ExecuteNonQueryAsync();

                    await connection.CloseAsync();
                }
                catch (SqlException sqlex)
                {
                    throw;
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    await connection.CloseAsync();
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

        public async Task<List<Photographer>> GetAllAsync()
        {
            List<Photographer> photographers = new List<Photographer>();
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getAll, connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        string firstName = reader.GetString("FirstName");
                        string lastName = reader.GetString("LastName");
                        string email = reader.GetString("E-mail");
                        string phoneNumber = reader.GetString("PhoneNumber");
                        string website = reader.GetString("Website");
                        string cvrNumber = reader.GetString("CVRNumber");
                        string city = reader.GetString("City");
                        //<<<<<<< HEAD
                        string postCode = reader.GetString("PostCode");
                        //=======
                        string street = reader.GetString("Street");
                        //>>>>>>> 241955fe52e1fe544a0a929281ca984e2ea46d2c
                        int experienceInYears = reader.GetInt32("ExperienceInYears");
                        int maxTravelRadiusInKm = reader.GetInt32("MaxTravelRadiusInKm");
                        string instagram = reader.GetString("Instagram");
                        string facebook = reader.GetString("Facebook");
                        Photographer photographer = new Photographer(firstName, lastName, phoneNumber, email, website, cvrNumber, city, postCode,
                            street, experienceInYears, maxTravelRadiusInKm, instagram, facebook);

                        photographers.Add(photographer);
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
            return photographers;
        }

        public async Task<Photographer> GetAsync(int toGet)
        {
            Photographer photographer = null;
            using SqlConnection connection = new SqlConnection(Secret.connectionString);
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
//<<<<<<< HEAD
                    string postCode = reader.GetString("PostCode");
//=======
                    string street = reader.GetString("Street");
//>>>>>>> 241955fe52e1fe544a0a929281ca984e2ea46d2c
                    int experienceInYears = reader.GetInt32("ExperienceInYears");
                    int maxTravelRadiusInKm = reader.GetInt32("MaxTravelRadiusInKm");
                    string instagram = reader.GetString("Instagram");
                    string facebook = reader.GetString("Facebook");
                    photographer = new Photographer(firstName, lastName, phoneNumber, email, website, cvrNumber, city, postCode,
                        street, experienceInYears, maxTravelRadiusInKm, instagram, facebook);
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

        public Task<User> GetForLogin(string email)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Photographer toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
