namespace SkoloFotoExam26.Models
{
    public class Parent : User
    {
        public int ParentID { get; private set; }
        public string Street { get; private set; }
        public int ZipCode { get; private set; }
        public string City { get; private set; }

        public Parent(int parentID, string firstName, string lastName, string email, string phoneNumber, string street, int zipCode, string city) :
            base(firstName, lastName, phoneNumber, email)
        {
            Street = street;
            ZipCode = zipCode;
            City = city;
        }

        public override string ToString()
        {
            return $"ID: {ParentID}\nFirst name: {FirstName}\nLast name: {LastName}\nEmail: {Email}\nPhone number: {PhoneNumber}\nStreet name: {Street}\nZip code: {ZipCode}\nCity: {City}\n{base.ToString()}";
        }
    }
}
