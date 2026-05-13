using SkoloFotoExam26.Models;
using SkoloFotoExam26.Helpers;
using Microsoft.Data.SqlClient;
using SkoloFotoExam26.Services;
using SkoloFotoExam26.Interfaces;

namespace SkoloFotoExam26.Services
{
    /// <summary>
    /// Class used to handle login attempts.
    /// </summary>
    public class LoginHandler
    {
        private LoginAttempt _toBeHandled;
        private IRepoAsync<LoginInfo, string> _loginRepo;

        public LoginHandler(LoginAttempt toBeHandled, IRepoAsync<LoginInfo, string> loginRepo)
        {
            _toBeHandled = toBeHandled;
            _loginRepo = loginRepo;
        }

        /// <summary>
        /// Async public method to handle login attempts.
        /// </summary>
        /// <returns>A task which returns either a UserType corresponding to the user who logged in, or an error message string.</returns>
        public async Task<object> HandleLoginAttempt()
        {
            try
            {
                Task<(bool, string, UserType?)> passwordcheck = CheckPassword();
                //space for extra stuff we can do before pwd check done here
                (bool result, string msg, UserType? type) = await passwordcheck;
                if (result == true && type != null)
                {
                    return type;
                }
                return msg;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Method to pull user data from the SQL database. Needs to be its own public method separate from HandleLoginAttempt so we can inject our repo.
        /// </summary>
        /// <param name="repo">The ILoginableRepo to pull our user data from.</param>
        /// <returns>A task which returns the User object with the data for the user that's logged in.</returns>
        public async Task<User> GetUser(ILoginableRepo repo)
        {
            User result = await repo.GetForLogin(_toBeHandled.Email);

            return result;
        }
        

        private async Task<(bool, string, UserType?)> CheckPassword()
        {
            LoginInfo dbResult = null;
            byte[] hashToCheck = null;
            try
            {
                Task<LoginInfo> dbTask = _loginRepo.GetAsync(_toBeHandled.Email);
                hashToCheck = HashHelper.HashPassword(_toBeHandled.Password);
                dbResult = await dbTask;
            }
            catch(Exception e)
            {
                if(e.Message == "WrongEmail")
                {
                    return (false, "Wrong email.", null);
                }
                else
                {
                    return (false, $"Exception: {e.Message}", null);
                }
            }
            
            if (HashHelper.CheckHashes(hashToCheck, dbResult.PasswordHash))
            {
                return (true, "Login succesful.", dbResult.TheUserType);
            }
            return (false, "Wrong password.", null);

            //TODO rewrite using LoginRepo and LoginInfo model
            //using (SqlConnection connection = new SqlConnection("placeholder, replace with connection string"))
            //{
            //    try
            //    {
            //        SqlCommand command = new SqlCommand("SELECT * FROM LoginInfo WHERE Email = @Email;", connection);
            //        Task openConnection = command.Connection.OpenAsync();
            //        command.Parameters.AddWithValue("@Email", _toBeHandled.Email);
            //        await openConnection;
            //        SqlDataReader reader = await command.ExecuteReaderAsync();
            //        if(await reader.ReadAsync())
            //        {
            //            byte[] hashToCheck = HashHelper.HashPassword(_toBeHandled.Password);
            //            byte[] hashFromDB = (byte[])reader["PasswordHash"];
            //            if (hashToCheck == hashFromDB)
            //            {
            //                return (true, "Login succesful.", (UserType)reader["UserType"]);
            //            }
            //            return (false, "Wrong password.", null);
            //        }
            //        else
            //        {
            //            return (false, "Wrong email.", null);
            //        }
            //    }
            //    catch(Exception e)
            //    {
            //        return (false, e.Message, null);
            //    }
            //}
        }
    }
}
