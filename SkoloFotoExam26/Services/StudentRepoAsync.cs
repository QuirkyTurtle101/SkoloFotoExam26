using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;

namespace SkoloFotoExam26.Services
{
    public class StudentRepoAsync : IRepoAsync<Student, int>
    {
        #region Querys

        private string _addStudent = "INSERT INTO Student VALUES (@FirstName, @MiddleName, @LastName, @ParentID, @SchoolClassID)";

        #endregion

        public async Task AddAsync(Student input)
        {
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_addStudent, connection);
                    await connection.OpenAsync();

                    command.Parameters.AddWithValue("@FirstName", input.FirstName);
                    command.Parameters.AddWithValue("@MiddleName", input.MiddleName);
                    command.Parameters.AddWithValue("@LastName", input.LastName);
                    command.Parameters.AddWithValue("@ParentID", input.Parent.ParentID);
                    command.Parameters.AddWithValue("@SchoolClassID", input.SchoolClass.SchoolClassID);

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

        public Task<List<Student>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetAsync(int toGet)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Student toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
