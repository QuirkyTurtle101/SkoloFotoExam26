namespace SkoloFotoExam26.Models
{
    public class Parent : User
    {
        public int ParentID { get; private set; }
        public string Street { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }

        public Parent(string firstName, string lastName, string email, string phoneNumber, string street, int zipCode, string city, int parentID) :
            base(firstName, lastName, phoneNumber, email)
        {
            Street = street;
            ZipCode = zipCode;
            City = city;
            ParentID = parentID;
        }
        public Parent(string firstName, string lastName, string email, string phoneNumber, string street, int zipCode, string city) :
           base(firstName, lastName, phoneNumber, email)
        {
            Street = street;
            ZipCode = zipCode;
            City = city;
        }

        public Parent()
        {

        }

        public override string ToString()
        {
            return $"ID: {ParentID}\nFirst name: {FirstName}\nLast name: {LastName}\nEmail: {Email}\nPhone number: {PhoneNumber}\nStreet name: {Street}\nZip code: {ZipCode}\nCity: {City}\n{base.ToString()}";
        }
    }
}
