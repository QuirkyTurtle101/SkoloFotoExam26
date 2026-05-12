namespace SkoloFotoExam26.Models
{
    public abstract class User
    {
        public int ID { get; private set; }
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }

        protected User()
        {
            
        }

        public User(string firstName, string lastName, string phoneNumber, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;

        }

        public override string ToString()
        {
            return $"FirstName: {FirstName}, LastName: {LastName}, PhoneNumber: {PhoneNumber}, Email: {Email}";
        }
    }
}
