namespace SkoloFotoExam26.Models
{
    public class Parent : User
    {
        public int ParentID { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }

        public Parent(int parentID, string firstName, string lastName, string email, string phoneNumber) :
            base(firstName, lastName, phoneNumber, email)
        {
            ParentID = parentID;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public override string ToString()
        {
            return $"ID: {ParentID}\nFirst name: {FirstName}\nLast name: {LastName}\nEmail: {Email}\nPhone number: {PhoneNumber}\n{base.ToString()}";
        }
    }
}
