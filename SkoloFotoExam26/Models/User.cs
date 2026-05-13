namespace SkoloFotoExam26.Models
{
    public abstract class User
    {
        public int ID { get;  set; }
        public string FirstName { get;  set; }

        public string LastName { get;  set; }

        public string Email { get;  set; }
        public string PhoneNumber { get;  set; }

        public User()
        {
            
        }
        public User(string firstName, string lastName, string phoneNumber, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;

        }
        public User(int id, string firstName, string lastName, string phoneNumber, string email)
        {
            ID = id;
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
