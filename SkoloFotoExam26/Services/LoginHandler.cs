using SkoloFotoExam26.Models;
using SkoloFotoExam26.Helpers;
using Microsoft.Data.SqlClient;

namespace SkoloFotoExam26.Services
{
    /// <summary>
    /// Class used to handle login attempts.
    /// </summary>
    public class LoginHandler
    {
        private LoginAttempt _toBeHandled;

        public LoginHandler(LoginAttempt toBeHandled)
        {
            _toBeHandled = toBeHandled;
        }

        public async Task<object> HandleLogin()
        {
            Task<(bool, string, UserType?)> passwordcheck = CheckPassword();
            (bool result, string msg, UserType? type) = await passwordcheck;
            if (result == true && type != null)
            {
                //TODO finish this
                //needs all User-derived models in place first
                switch (type)
                {

                }
            }
            return msg;
        }

        private async Task<(bool, string, UserType?)> CheckPassword()
        {
            using (SqlConnection connection = new SqlConnection("placeholder, replace with connection string"))
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM LoginInfo WHERE Email = @Email;", connection);
                    Task openConnection = command.Connection.OpenAsync();
                    command.Parameters.AddWithValue("@Email", _toBeHandled.Email);
                    await openConnection;
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if(await reader.ReadAsync())
                    {
                        byte[] hashToCheck = HashHelper.HashPassword(_toBeHandled.Password);
                        byte[] hashFromDB = (byte[])reader["PasswordHash"];
                        if (hashToCheck == hashFromDB)
                        {
                            return (true, "Login succesful.", (UserType)reader["UserType"]);
                        }
                        return (false, "Wrong password.", null);
                    }
                    else
                    {
                        return (false, "Wrong email.", null);
                    }
                }
                catch(Exception e)
                {
                    return (false, e.Message, null);
                }
            }
        }
    }
}
