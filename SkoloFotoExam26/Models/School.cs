using System.Security.Principal;

namespace SkoloFotoExam26.Models
{
    public class School
    {
        public int SchoolID { get; }
        public string Name { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public SchoolType SchoolType { get; set; }

        public School()
        {
            
        }

        public School(string name, string streetName, string city, string zipCode, SchoolType schoolType)
        {
            Name = name;
            StreetName = streetName;
            City = city;
            SchoolType = schoolType;
            ZipCode = zipCode;
        }

        public override string ToString()
        {
            return $"SchoolID: {SchoolID}, Name: {Name}, Street: {StreetName}, Zip-Code: {ZipCode}, school type: {SchoolType}";
        }
    }
}
