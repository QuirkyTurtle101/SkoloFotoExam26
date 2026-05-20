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

        private string _addBooking = "INSERT INTO Booking VALUES(@Start, @End, @SchoolClassID, @PhotographingEventID, @TeacherID)";

        private string _getBooking = "SELECT * FROM Booking WHERE BookingID = @BookingID";

        private string _delete = "DELETE FROM Booking WHERE BookingID = @BookingID";

        private string _update = "UPDATE Booking SET [Start] = @Start, [End] = @End, SchoolClassID = @SchoolClassID, PhotographingEventID = @PhotographingEventID, TeacherID = @TeacherID WHERE BookingID = @BookingID";

        private IRepoAsync<PhotographingEvent, int> _photographingEventRepo;

        private IRepoAsync<Teacher, int> _teacherRepo;

        private IRepoAsync<SchoolClass, int> _schoolClassRepo;

        public BookingRepoAsync(IRepoAsync<PhotographingEvent, int> photographingEventRepo, IRepoAsync<Teacher, int> teacherRepo,
             IRepoAsync<SchoolClass, int> schoolClassRepo)
        {
            _photographingEventRepo = photographingEventRepo;
            _teacherRepo = teacherRepo;
            _schoolClassRepo = schoolClassRepo;
        }

        public async Task AddAsync(Booking input)
        {
            using SqlConnection connection = new SqlConnection(Secret.connectionString);
            try
            {
                SqlCommand command = new SqlCommand(_addBooking, connection);
                await command.Connection.OpenAsync();
                
                command.Parameters.AddWithValue("@Start", input.Start);
                command.Parameters.AddWithValue("@End", input.End);
                command.Parameters.AddWithValue("@PhotographingEventID", input.ThePhotographingEvent.PhotographingEventID);
                command.Parameters.AddWithValue("@TeacherID", input.TheTeacher.ID);
                command.Parameters.AddWithValue("@SchoolClassID", input.TheSchoolClass.SchoolClassID);
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
                command.Parameters.AddWithValue("@BookingID", toDelete);
                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException sqlExp)
            {
                Console.WriteLine($"sql exception message: {sqlExp.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"exception message: {ex.Message}");
            }
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            List<Booking> bookings = new List<Booking>();
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getAll, connection);
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int bookingID = reader.GetInt32("BookingID");
                        DateTime start = reader.GetDateTime("Start");
                        DateTime end = reader.GetDateTime("End");
                        int photographingEventID = reader.GetInt32("PhotographingEventID");
                        int teacherID = reader.GetInt32("TeacherID");
                        int schoolClassID = reader.GetInt32("SchoolClassID");
                        PhotographingEvent photographingEvent = await _photographingEventRepo.GetAsync(photographingEventID);
                        Teacher teacher = await _teacherRepo.GetAsync(teacherID);
                        SchoolClass schoolClass = await _schoolClassRepo.GetAsync(schoolClassID);
                        Booking booking = new Booking(start, end, photographingEvent, schoolClass, teacher, bookingID);
                        bookings.Add(booking);
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

        public async Task<Booking> GetAsync(int toGet)
        {
            Booking booking = null;
            using SqlConnection connection = new SqlConnection(Secret.connectionString);
            try
            {
                using SqlCommand command = new SqlCommand(_getBooking, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@BookingID", toGet);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    int bookingID = reader.GetInt32("BookingID");
                    DateTime start = reader.GetDateTime("Start");
                    DateTime end = reader.GetDateTime("End");
                    int photographingEventID = reader.GetInt32("PhotographingEventID");
                    int teacherID = reader.GetInt32("TeacherID");
                    int schoolClassID = reader.GetInt32("SchoolClassID");
                    PhotographingEvent photographingEvent = await _photographingEventRepo.GetAsync(photographingEventID);
                    Teacher teacher = await _teacherRepo.GetAsync(teacherID);
                    SchoolClass schoolClass = await _schoolClassRepo.GetAsync(schoolClassID);
                    booking = new Booking(start, end, photographingEvent, schoolClass, teacher, bookingID);

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
            return booking;
        }

        public async Task UpdateAsync(Booking toUpdate)
        {
            using SqlConnection connection = new SqlConnection(Secret.connectionString);
            try
            {
                SqlCommand command = new SqlCommand(_update, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@Start", toUpdate.Start);
                command.Parameters.AddWithValue("@End", toUpdate.End);
                command.Parameters.AddWithValue("@SchoolClassID", toUpdate.TheSchoolClass.SchoolClassID);
                command.Parameters.AddWithValue("@PhotographingEventID", toUpdate.ThePhotographingEvent.PhotographingEventID);
                command.Parameters.AddWithValue("@TeacherID", toUpdate.TheTeacher.ID);
                command.Parameters.AddWithValue("@BookingID", toUpdate.BookingID);
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
    }
}
