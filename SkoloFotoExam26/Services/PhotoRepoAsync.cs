using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using System;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SkoloFotoExam26.Services
{
    public class PhotoRepoAsync : IRepoAsync<Photo, int> 
    {
        #region Query strings
        private string _add = "INSERT INTO Photo VALUES(@FileName, @FilePath, @Price, @Date, @Height, @Width, @PhotoType)";
        private string _delete = "DELETE FROM Photo WHERE PhotoID = @PhotoID";
        private string _getAll = "SELECT * FROM Photo";
        private string _get = "SELECT * FROM Photo WHERE PhotoID = @PhotoID";

        #endregion


        private IRepoAsync<Booking, int> _bookings;

        public PhotoRepoAsync(IRepoAsync<Booking, int> bookings)
        {
            _bookings = bookings;
        }

        public async Task AddAsync(Photo input)
        {
            SqlConnection connection = new SqlConnection(Secret.connectionString);
            try
            {
                SqlCommand command = new SqlCommand(_add, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@FileName", input.FileName);
                command.Parameters.AddWithValue("@FilePath", input.FilePath);
                command.Parameters.AddWithValue("@Price", input.Price);
                command.Parameters.AddWithValue("@Date", input.TheDate);
                command.Parameters.AddWithValue("@Height", input.Height);
                command.Parameters.AddWithValue("@Width", input.Width);
                command.Parameters.AddWithValue("@PhotoType", input.PhotoType);
                await command.ExecuteNonQueryAsync();
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
                command.Parameters.AddWithValue("@PhotoID", toDelete);
                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL exception: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception message: {ex.Message}");
            }
        }

        public async Task<List<Photo>> GetAllAsync()
        {
            List<Photo> photos = new List<Photo>();
            using (SqlConnection connection = new SqlConnection(Secret.connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getAll, connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int valueType = reader.GetInt32("PhotoType");
                        PhotoType photoType = (PhotoType)valueType;
                        string fileName = reader.GetString("FileName");
                        string filePath = reader.GetString("FilePath");
                        DateTime date = reader.GetDateTime("Date");
                        int height = reader.GetInt32("Height");
                        int width = reader.GetInt32("Width");

                        //Booking booking = await _bookings.GetAsync();
                        //Photo photo = new Photo(fileName, filePath, date, height, width, photoType);

                        //photos.Add(photo);
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
            return photos;
        }

        

        public async Task<Photo> GetAsync(int toGet)
        {
            Photo photo = null;
            SqlConnection connection = new SqlConnection(Secret.connectionString);
            try
            {
                SqlCommand command = new SqlCommand(_get, connection);
                command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@PhotoID", toGet);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    string fileName = reader.GetString("FileName");
                    string filePath = reader.GetString("FilePath");
                    double price = reader.GetDouble("Price");
                    DateTime date = reader.GetDateTime("Date");
                    int height = reader.GetInt32("Height");
                    int width = reader.GetInt32("Width");
                    int valueType = reader.GetInt32("PhotoType");
                    PhotoType photoType = (PhotoType)valueType;

                    //photo = new Photo(fileName, filePath, price, date, height, width, photoType, )
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
            return photo;
        }

        public Task UpdateAsync(Photo toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
