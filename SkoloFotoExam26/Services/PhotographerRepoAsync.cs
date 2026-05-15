using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using System.Data;

namespace SkoloFotoExam26.Services
{
    public class PhotographerRepoAsync : IRepoAsync<Photographer, int>, ILoginableRepo
    {
        #region Querys

        private string _addPhotographer = "INSERT INTO Photographer VALUES(@FirstName, @LastName, @Email, @PhoneNumber, @WebSite, @CVRNumber, @StreetName, @ExperienceInYears, @MaxTravelRadiusInKm, @Instagram, @Facebook, @ZipCode)";
        private string _getPhotographer = "SELECT * FROM Photographer JOIN ZipCodeLookup ON Photographer.ZipCode=ZipCodeLookup.ZipCode WHERE PhotographerID = @PhotographerID";
        private string _getAll = "SELECT * FROM Photographer JOIN ZipCodeLookup ON Photographer.ZipCode=ZipCodeLookup.ZipCode";
        private string _deletePhotographer = "DELETE FROM Photographer WHERE PhotographerID = @PhotographerID";
        private string _updatePhotographer = "UPDATE Photographer SET FirstName = @FirstName, LastName = @LastName, PhoneNumber = @PhoneNumber, StreetName = @StreetName, ZipCode  = @ZipCode, WebSite = @WebSite, CVRNumber = @CVRNumber, ExperienceInYears = @ExperienceInYears, MaxTravelRadiusInKm = @MaxTravelRadiusInKm, Instagram = @Instagram, Facebook = @Facebook WHERE PhotographerID = @ID";

        #endregion

        #region Methods
        public async Task AddAsync(Photographer input)
        {
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            using (SqlCommand command = new SqlCommand(_addPhotographer, connection))
            {
                try
                {
                    await connection.OpenAsync();

                    command.Parameters.AddWithValue("@FirstName", input.FirstName);
                    command.Parameters.AddWithValue("@LastName", input.LastName);
                    command.Parameters.AddWithValue("@Email", input.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", input.PhoneNumber);
                    command.Parameters.AddWithValue("@StreetName", input.Street);
                    command.Parameters.AddWithValue("@ZipCode", input.ZipCode);
                    command.Parameters.AddWithValue("@WebSite", input.Website);
                    command.Parameters.AddWithValue("@CVRNumber", input.CVRNumber);
                    command.Parameters.AddWithValue("@ExperienceInYears", input.ExperienceInYears);
                    command.Parameters.AddWithValue("@MaxTravelRadiusInKm", input.MaxTravelRadiusInKm);
                    command.Parameters.AddWithValue("@Instagram", input.Instagram);
                    command.Parameters.AddWithValue("@Facebook", input.Facebook);

                    int noOfRowsEffected = await command.ExecuteNonQueryAsync();

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

        public async Task DeleteAsync(int toDelete)
        {
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_deletePhotographer, connection);
                    await connection.OpenAsync();

                    command.Parameters.AddWithValue("@PhotographerID", toDelete);

                    int noOfRowsEffected = await command.ExecuteNonQueryAsync();

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
                        int photographerID = reader.GetInt32("PhotographerID");

                        string firstName = reader.GetString("FirstName");
                        string lastName = reader.GetString("LastName");
                        string email = reader.GetString("Email");
                        string phoneNumber = reader.GetString("PhoneNumber");
                        string website = reader.GetString("Website");
                        string cvrNumber = reader.GetString("CVRNumber");
                        string city = reader.GetString("City");
                        //<<<<<<< HEAD
                        int zipCode = reader.GetInt32("ZipCode");
                        //=======
                        string street = reader.GetString("StreetName");
                        //>>>>>>> 241955fe52e1fe544a0a929281ca984e2ea46d2c
                        int experienceInYears = reader.GetInt32("ExperienceInYears");
                        int maxTravelRadiusInKm = reader.GetInt32("MaxTravelRadiusInKm");
                        string instagram = reader.GetString("Instagram");
                        string facebook = reader.GetString("Facebook");
                        Photographer photographer = new Photographer(photographerID, firstName, lastName, phoneNumber, email, website, cvrNumber, city, zipCode,
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
                finally
                {
                    await connection.CloseAsync();
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
                    int photographerID = reader.GetInt32("PhotographerID");
                    string firstName = reader.GetString("FirstName");
                    string lastName = reader.GetString("LastName");
                    string email = reader.GetString("Email");
                    string phoneNumber = reader.GetString("PhoneNumber");
                    string website = reader.GetString("Website");
                    string cvrNumber = reader.GetString("CVRNumber");
                    string city = reader.GetString("City");
                    //<<<<<<< HEAD
                    int zipCode = reader.GetInt32("ZipCode");
                    //=======
                    string street = reader.GetString("StreetName");
                    int experienceInYears = reader.GetInt32("ExperienceInYears");
                    int maxTravelRadiusInKm = reader.GetInt32("MaxTravelRadiusInKm");
                    string instagram = reader.GetString("Instagram");
                    string facebook = reader.GetString("Facebook");
                    photographer = new Photographer(photographerID, firstName, lastName, phoneNumber, email, website, cvrNumber, city, zipCode,
                        street, experienceInYears, maxTravelRadiusInKm, instagram, facebook);
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
            finally
            {
                await connection.CloseAsync();
            }
            return photographer;
        }

        public Task<User> GetForLogin(string email)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Photographer toUpdate)
        {

            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            using (SqlCommand command = new SqlCommand(_updatePhotographer, connection))
            {
                try
                {
                    await command.Connection.OpenAsync();

                    command.Parameters.AddWithValue("@FirstName", toUpdate.FirstName);
                    command.Parameters.AddWithValue("@LastName", toUpdate.LastName);
                    command.Parameters.AddWithValue("@PhoneNumber", toUpdate.PhoneNumber);
                    command.Parameters.AddWithValue("@StreetName", toUpdate.Street);
                    command.Parameters.AddWithValue("@ZipCode", toUpdate.ZipCode);
                    command.Parameters.AddWithValue("@WebSite", toUpdate.Website);
                    command.Parameters.AddWithValue("@CVRNumber", toUpdate.CVRNumber);
                    command.Parameters.AddWithValue("@ExperienceInYears", toUpdate.ExperienceInYears);
                    command.Parameters.AddWithValue("@MaxTravelRadiusInKm", toUpdate.MaxTravelRadiusInKm);
                    command.Parameters.AddWithValue("@Instagram", toUpdate.Instagram);
                    command.Parameters.AddWithValue("@Facebook", toUpdate.Facebook);
                    command.Parameters.AddWithValue("@ID", toUpdate.ID);

                    await command.ExecuteNonQueryAsync();

                }
                catch (SqlException sqlex)
                {
                    Console.WriteLine("sql fejl: " + sqlex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {
                    await command.Connection.CloseAsync();
                }
            }
        }
        #endregion


    }
}
