using System.Security.Principal;

namespace SkoloFotoExam26.Models
{
    public class School
    {
        public int SchoolID { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public SchoolType SchoolType { get; set; }

        public School()
        {
            
        }

        public School(int schoolID, string name, string street, string city, int zipCode, SchoolType schoolType)
        {
            Name = name;
            Street = street;
            City = city;
            SchoolType = schoolType;
            ZipCode = zipCode;
            SchoolID = schoolID;
        }

        public override string ToString()
        {
            return $"SchoolID: {SchoolID}, Name: {Name}, Street: {Street}, Zip-Code: {ZipCode}, school type: {SchoolType}";
        }
    }
}
