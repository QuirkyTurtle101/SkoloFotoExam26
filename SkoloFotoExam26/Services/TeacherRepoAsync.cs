using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using System.Net.Http.Headers;

namespace SkoloFotoExam26.Services
{
    public class TeacherRepoAsync : ConnectionString, ITeacherRepoAsync
    {
        private string _addTeacher = "INSERT INTO Teacher VALUES(@FirstName, @LastName, @E-mail, @PhoneNumber, @Initials, @SchoolID)";

        public async Task AddAsync(Teacher input)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                SqlCommand command = new SqlCommand(_addTeacher, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@FirstName", input.FirstName);
                command.Parameters.AddWithValue("@LastName", input.LastName);
                command.Parameters.AddWithValue("@E-mail", input.Email);
                command.Parameters.AddWithValue("@PhoneNumber", input.PhoneNumber);
                command.Parameters.AddWithValue("@Initials", input.Initials);
                command.Parameters.AddWithValue("@SchoolID", input.TheSchool.SchoolID);
            }
            catch (SqlException sqlEx)
    {
                Console.WriteLine($"SQL exception message: {sqlEx.Message}");
            }
            catch(Exception ex)
        {
                Console.WriteLine($"Exception message: {ex.Message}");
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

        public Task<List<Teacher>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Teacher> GetAsync(int toGet)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetForLogin(string email)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Teacher toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
