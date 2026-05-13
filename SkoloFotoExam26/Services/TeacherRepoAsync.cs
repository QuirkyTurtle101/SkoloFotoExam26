using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using System.Data;
using System.Net.Http.Headers;

namespace SkoloFotoExam26.Services
{
    public class TeacherRepoAsync : IRepoAsync<Teacher, int>, ILoginableRepo
    {
        private string _addTeacher = "INSERT INTO Teacher VALUES(@FirstName, @LastName, @Email, @PhoneNumber, @Initials, @SchoolID)";
        private string _getAll = "SELECT * FROM Teacher";
        private string _getTeacher = "SELECT * FROM Teacher WHERE TeacherID = @TeacherID";
        private string _delete = "DELETE FROM Teacher WHERE TeacherID = @TeacherID";
        private string _update = "UPDATE Teacher SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @PhoneNumber, Initials = @Initials, SchoolID = @SchoolID WHERE TeacherID = @TeacherID";
        private string _getTeacherForLogin = "SELECT * FROM Teacher WHERE Email = @Email";

        private IRepoAsync<School, int> _schoolRepo;
        public TeacherRepoAsync(IRepoAsync<School, int> schoolRepo)
        {
            _schoolRepo = schoolRepo;
        }
        public async Task AddAsync(Teacher input)
        {
            using SqlConnection connection = new SqlConnection(Secret.connectionString);
            try
            {
                SqlCommand command = new SqlCommand(_addTeacher, connection);
                await command.Connection.OpenAsync();
                
                command.Parameters.AddWithValue("@FirstName", input.FirstName);
                command.Parameters.AddWithValue("@LastName", input.LastName);
                command.Parameters.AddWithValue("@Email", input.Email);
                command.Parameters.AddWithValue("@PhoneNumber", input.PhoneNumber);
                command.Parameters.AddWithValue("@Initials", input.Initials);
                command.Parameters.AddWithValue("@SchoolID", input.TheSchool.SchoolID);
                await command.ExecuteNonQueryAsync();
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

        public async Task DeleteAsync(int toDelete)
        {
            using SqlConnection connection = new SqlConnection(Secret.connectionString);
            try
            {
                SqlCommand command = new SqlCommand(_delete, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@TeacherID", toDelete);
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

        public async Task<List<Teacher>> GetAllAsync()
        {
            List<Teacher> teachers = new List<Teacher>();
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getAll, connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int teacherID = reader.GetInt32("TeacherID");
                        string initials = reader.GetString("Initials");
                        string firstName = reader.GetString("FirstName");
                        string lastName = reader.GetString("LastName");
                        string phoneNumber = reader.GetString("PhoneNumber");
                        string email = reader.GetString("Email");
                        int schoolID = reader.GetInt32("SchoolID");
                        School school = await _schoolRepo.GetAsync(schoolID);
                        Teacher teacher = new Teacher(teacherID, initials, firstName, lastName, phoneNumber, email, school);
                        teachers.Add(teacher);
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
            return teachers;
        }
        

        public async Task<Teacher> GetAsync(int toGet)
        {
            Teacher teacher = null;
            using SqlConnection connection = new SqlConnection(Secret.connectionString);
            try
            {
                using SqlCommand command = new SqlCommand(_getTeacher, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@TeacherID", toGet);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    int teacherID = reader.GetInt32("TeacherID");
                    string initials = reader.GetString("Initials");
                    string firstName = reader.GetString("FirstName");
                    string lastName = reader.GetString("LastName");
                    string phoneNumber = reader.GetString("PhoneNumber");
                    string email = reader.GetString("Email");
                    int schoolID = reader.GetInt32("SchoolID");
                    School school = await _schoolRepo.GetAsync(schoolID);
                    teacher = new Teacher(teacherID, initials, firstName, lastName, phoneNumber, email, school);
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
            return teacher;
        }

        public async Task<User> GetForLogin(string email)
        {
            Teacher teacher = null;
            using SqlConnection connection = new SqlConnection(Secret.connectionString);
            try
            {
                using SqlCommand command = new SqlCommand(_getTeacherForLogin, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@Email", email);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    int teacherID = reader.GetInt32("TeacherID");
                    string initials = reader.GetString("Initials");
                    string firstName = reader.GetString("FirstName");
                    string lastName = reader.GetString("LastName");
                    string phoneNumber = reader.GetString("PhoneNumber");
                    string emailResult = reader.GetString("Email");
                    int schoolID = reader.GetInt32("SchoolID");
                    School school = await _schoolRepo.GetAsync(schoolID);
                    teacher = new Teacher(teacherID, initials, firstName, lastName, phoneNumber, emailResult, school);
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
            return teacher;
        }

        public async Task UpdateAsync(Teacher toUpdate)
        {
            using SqlConnection connection = new SqlConnection(Secret.connectionString);
            try
            {
                SqlCommand command = new SqlCommand(_update, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@FirstName", toUpdate.FirstName);
                command.Parameters.AddWithValue("@LastName", toUpdate.LastName);
                command.Parameters.AddWithValue("@Email", toUpdate.Email);
                command.Parameters.AddWithValue("@PhoneNumber", toUpdate.PhoneNumber);
                command.Parameters.AddWithValue("@Initials", toUpdate.Initials);
                command.Parameters.AddWithValue("@SchoolID", toUpdate.TheSchool.SchoolID);
                command.Parameters.AddWithValue("@TeacherID", toUpdate.ID);
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
