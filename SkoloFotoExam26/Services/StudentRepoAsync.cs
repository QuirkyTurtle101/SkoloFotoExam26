using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using System.Data;

namespace SkoloFotoExam26.Services
{
    public class StudentRepoAsync : IRepoAsync<Student, int>
    {
        #region Querys

        private string _addStudent = "INSERT INTO Student VALUES (@StudentID, @FirstName, @MiddleName, @LastName, @ParentID, @SchoolClassID)";
        private string _getAllStudent = @"SELECT " +
            "s.StudentID," +
            "s.FirstName, " +
            "s.MiddleName, " +
            "s.LastName, " +
            "s.ParentID, " +
            "s.SchoolClassID, " +
            "c.ClassName, " +
            "sch.Name AS SchoolName " +
            "FROM Student s " +
            "JOIN SchoolClass c ON s.SchoolClassID = c.SchoolClassID " +
            "JOIN School sch ON c.SchoolID = sch.SchoolID";

        #endregion

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

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
                    command.Parameters.AddWithValue("@ParentID", input.Parent.ID);
                    command.Parameters.AddWithValue("@SchoolID", input.School.SchoolID);
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

        public Task<Student> GetAsync(int toGet)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Student>> GetAllAsync()
        {
            List<Student> students = new List<Student>();
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getAllStudent, connection);
                    await command.Connection.OpenAsync();

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        int studentID = reader.GetInt32("StudentID");
                        string firstName = reader.GetString("FirstName");
                        string middleName = reader.IsDBNull(reader.GetOrdinal("MiddleName")) ? "" : reader.GetString("MiddleName");
                        string lastName = reader.GetString("LastName");

                        string sName = reader.GetString("SchoolName");
                        string cName = reader.GetString("ClassName"); 
                        string pName = reader.GetString("FirstName");

                        School dummySchool = new School { Name = sName };
                        SchoolClass dummyClass = new SchoolClass { ClassName = cName };
                        Parent dummyParent = new Parent { FirstName = pName };

                        students.Add(new Student(studentID, firstName, middleName, lastName, dummyParent, dummySchool, dummyClass));
                    }
                }
                catch (SqlException sqlExp)
                {
                    throw;
                }
                catch (Exception exp)
                {
                    throw;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
            return students;
        }

        public Task DeleteAsync(int toDelete)
        {
            throw new NotImplementedException();
        }


        public Task UpdateAsync(Student toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
