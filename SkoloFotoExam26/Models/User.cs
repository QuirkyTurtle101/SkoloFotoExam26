namespace SkoloFotoExam26.Models
{
    public abstract class User
    {

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        //public string Password { get; set; }


        public User(string firstName, string lastName, string phoneNumber, string email /*string password*/)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            //Password = password;

        }

        public override string ToString()
        {
            return $"FirstName: {FirstName}, LastName: {LastName}, PhoneNumber: {PhoneNumber}, Email: {Email}" /*Password: {Password}*/;
        }
    }
}
