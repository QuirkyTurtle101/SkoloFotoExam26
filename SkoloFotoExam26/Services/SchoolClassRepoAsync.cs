using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using System.Data;

namespace SkoloFotoExam26.Services
{
    public class SchoolClassRepoAsync : IRepoAsync<SchoolClass, int>
    {
        #region Query strings
        private string _addSchoolClass = "INSERT INTO SchoolClass VALUES(@ClassName, @SchoolID)";
        private string _getAll = "SELECT * FROM SchoolClass";
        private string _getSchoolClass = "SELECT * FROM SchoolClass WHERE SchoolClassID = @SchoolClassID";
        private string _delete = "DELETE FROM SchoolClass WHERE SchoolClassID = @SchoolClassID";
        private string _update = "UPDATE SchoolClass SET ClassName = @ClassName, SchoolID = @SchoolID WHERE SchoolClassID = @SchoolClassID";
        #endregion

        private IRepoAsync<School, int> _schoolRepo;

        public SchoolClassRepoAsync(IRepoAsync<School, int> schoolRepo)
        {
            _schoolRepo = schoolRepo;
        }

        public async Task AddAsync(SchoolClass input)
        {
            using SqlConnection connection = new SqlConnection(Secret.connectionString);
            try
            {
                SqlCommand command = new SqlCommand(_addSchoolClass, connection);
                await command.Connection.OpenAsync();
                {
                    command.Parameters.AddWithValue("ClassName", input.ClassName);
                    command.Parameters.AddWithValue("SchoolID", input.School.SchoolID);
                    await command.ExecuteNonQueryAsync();
                }

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL exception message: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception message: {ex.Message}");
            }
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int toDelete)
        {
            using SqlConnection connection = new SqlConnection(Secret.connectionString);
            try
            {
                SqlCommand command = new SqlCommand(_delete, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@SchoolClassID", toDelete);
                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL exception message: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception message: {ex.Message}");
            }
        }

        public async Task<List<SchoolClass>> GetAllAsync()
        {
            List<SchoolClass> schoolClasses = new List<SchoolClass>();
            using SqlConnection connection = new SqlConnection(Secret.connectionString);
            
            try 
            {
                SqlCommand command = new SqlCommand(_getAll, connection);
                await command.Connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    string className = reader.GetString("ClassName");
                    int schoolID = reader.GetInt32("SchoolID");
                    int schoolClassID = reader.GetInt32("SchoolClassID");
                    School school = await _schoolRepo.GetAsync(schoolID);

                    SchoolClass schoolClass = new SchoolClass(schoolClassID, className, school);
                    schoolClasses.Add(schoolClass);
                }

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL exception message: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception message: {ex.Message}");
            }
            return schoolClasses;

        }

        public async Task<SchoolClass> GetAsync(int toGet)
        {
            SchoolClass schoolClass = null;
            using SqlConnection connection = new SqlConnection(Secret.connectionString);

            try
            {
                SqlCommand command = new SqlCommand(_getSchoolClass, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@SchoolClassID", toGet);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    string className = reader.GetString("ClassName");
                    int schoolID = reader.GetInt32("SchoolID");
                    int schoolClassID = reader.GetInt32("SchoolClassID");
                    School school = await _schoolRepo.GetAsync(schoolID);
                    schoolClass = new SchoolClass(schoolClassID, className, school);
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL exception message: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception message: {ex.Message}");
            }
            return schoolClass;
        }

        public async Task UpdateAsync(SchoolClass toUpdate)
        {
            using SqlConnection connection = new SqlConnection(Secret.connectionString);
            try
            {
                SqlCommand command = new SqlCommand(_update, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@ClassName", toUpdate.ClassName);
                command.Parameters.AddWithValue("@SchoolID", toUpdate.School.SchoolID);
                command.Parameters.AddWithValue("@SchoolClassID", toUpdate.SchoolClassID);
                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL exception message: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception message: {ex.Message}");
            }
        }
    }
}
