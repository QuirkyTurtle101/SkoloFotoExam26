namespace SkoloFotoExam26.Models
{
    public class LoginAttempt
    {
        private string _emailEntered;
        private string _passwordEntered;

        public string Email { get { return _emailEntered; } set { _emailEntered = value; } }
        public string Password { get { return _passwordEntered; } set { _passwordEntered = value; } }

        public LoginAttempt(string email, string password)
        {
            _emailEntered = email;
            _passwordEntered = password;
        }
    }
}
