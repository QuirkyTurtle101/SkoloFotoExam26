namespace SkoloFotoExam26.Models
{
    public class Parent : User
    {

        public string Street { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }

        public Parent(string firstName, string lastName, string email, string phoneNumber, string street, int zipCode, string city, int parentID) :
            base(parentID, firstName, lastName, phoneNumber, email)
        {
            Street = street;
            ZipCode = zipCode;
            City = city;
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
            return $"ID: {ID}\nFirst name: {FirstName}\nLast name: {LastName}\nEmail: {Email}\nPhone number: {PhoneNumber}\nStreet name: {Street}\nZip code: {ZipCode}\nCity: {City}\n{base.ToString()}";
        }
    }
}
