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

        public Task DeleteAsync(int toDelete)
        {
            throw new NotImplementedException();
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
                    while (reader.Read())
                    {
                        //int bookingID = reader.GetInt32("BookingID");
                        DateTime start = reader.GetDateTime("Start");
                        DateTime end = reader.GetDateTime("End");
                        int photographingEventID = reader.GetInt32("PhotographingEventID");
                        int teacherID = reader.GetInt32("TeacherID");
                        int schoolClassID = reader.GetInt32("SchoolClassID");
                        PhotographingEvent photographingEvent = await _photographingEventRepo.GetAsync(photographingEventID);
                        Teacher teacher = await _teacherRepo.GetAsync(teacherID);
                        SchoolClass schoolClass = await _schoolClassRepo.GetAsync(schoolClassID);
                        Booking booking = new Booking(start, end, photographingEvent, teacher, schoolClass);
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

                if (reader.Read())
                {
                    //int bookingID = reader.GetInt32("BookingID");
                    DateTime start = reader.GetDateTime("Start");
                    DateTime end = reader.GetDateTime("End");
                    int photographingEventID = reader.GetInt32("PhotographingEventID");
                    int teacherID = reader.GetInt32("TeacherID");
                    int schoolClassID = reader.GetInt32("SchoolClassID");
                    PhotographingEvent photographingEvent = await _photographingEventRepo.GetAsync(photographingEventID);
                    Teacher teacher = await _teacherRepo.GetAsync(teacherID);
                    SchoolClass schoolClass = await _schoolClassRepo.GetAsync(schoolClassID);
                    booking = new Booking(start, end, photographingEvent, teacher, schoolClass);

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

        public Task UpdateAsync(Booking toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
