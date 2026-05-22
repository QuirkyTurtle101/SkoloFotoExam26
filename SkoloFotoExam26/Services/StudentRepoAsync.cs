using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using System.Data;

namespace SkoloFotoExam26.Services
{
    public class StudentRepoAsync : IRepoAsync<Student, int>
    {
        #region Querys

        private string _addStudent = "INSERT INTO Student VALUES (@FirstName, @MiddleName, @LastName, @ParentID, @SchoolClassID)";
        private string _getStudent = "SELECT * FROM Student WHERE StudentID = @StudentID";
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
        private string _updateStudent = "UPDATE Student set " +
            "FirstName=@FirstName, " +
            "MiddleName=@MiddleName, " +
            "LastName=@LastName, " +
            "ParenID=@ParentID, " +
            "School=@School, " +
            "SchoolClass=@SchoolClassID" +
            "WHERE StudentID = @StudentID";
        #endregion

        private IRepoAsync<Parent, int> _parentRepo;

        private IRepoAsync<School, int> _schoolRepo;
        private IRepoAsync<SchoolClass, int> _schoolClassRepo;

        public StudentRepoAsync(IRepoAsync<Parent, int> parentRepo, IRepoAsync<SchoolClass, int> schoolClassRepo)
        {
            _parentRepo = parentRepo;
            _schoolClassRepo = schoolClassRepo;
        }

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

        public async Task<Student> GetAsync(int toGet)
        {
            Student student = null;
            using SqlConnection connection = new SqlConnection(Secret.connectionString);
            try
            {
                using SqlCommand command = new SqlCommand(_getStudent, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@StudentID", toGet);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    int studentID = reader.GetInt32("StudentID");
                    string firstName = reader.GetString("FirstName");
                    string middleName = reader.GetString("MiddleName");
                    string lastName = reader.GetString("LastName");
                    int parentID = reader.GetInt32("ParentID");
                    //int schoolID = reader.GetInt32("SchoolID");
                    int schoolClassID = reader.GetInt32("SchoolClassID");
                    Parent parent = await _parentRepo.GetAsync(parentID);
                    //School school = await _schoolRepo.GetAsync(schoolID);
                    SchoolClass schoolClass = await _schoolClassRepo.GetAsync(schoolClassID);

                    student = new Student(firstName, middleName, lastName, parent, schoolClass);
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
            return student;
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

                        string cName = reader.GetString("ClassName"); 
                        string pName = reader.GetString("FirstName");

                        SchoolClass dummyClass = new SchoolClass { ClassName = cName };
                        Parent dummyParent = new Parent { FirstName = pName };

                        students.Add(new Student(studentID, firstName, middleName, lastName, dummyParent, dummyClass));
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
        public async Task UpdateAsync(Student toUpdate)
        {
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            using (SqlCommand command = new SqlCommand(_updateStudent, connection))
            {
                try
                {
                    await command.Connection.OpenAsync();

                    command.Parameters.AddWithValue("@FirstName", toUpdate.FirstName);
                    command.Parameters.AddWithValue("@MiddleName", toUpdate.MiddleName);
                    command.Parameters.AddWithValue("@LastName", toUpdate.LastName);
                    command.Parameters.AddWithValue("@ParentID", toUpdate.Parent.ID);
                    command.Parameters.AddWithValue("@SchoolClassID", toUpdate.SchoolClass.SchoolClassID);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0)
                    {
                        Console.WriteLine("Ingen rækker blev opdateret! Tjek om ID'et findes.");
                    }
                }
                catch (SqlException sqlex)
                {
                    Console.WriteLine("Sql fejl: " + sqlex.Message);
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

        public Task DeleteAsync(int toDelete)
        {
            throw new NotImplementedException();
        }
    }
}
