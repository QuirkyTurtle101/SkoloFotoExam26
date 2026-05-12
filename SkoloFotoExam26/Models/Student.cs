using SkoloFotoExam26.Pages.SchoolSecretaries;

namespace SkoloFotoExam26.Models
{
    public class Student
    {

        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Parent Parent { get; set; }
        public School School { get; set; }
        public SchoolClass SchoolClass { get; set; }

        public Student()
        {
            
        }
        
        public Student(string firstName, string middleName, string lastName, Parent parent,School schoolName, SchoolClass className)//Denne konstructør bruges når man opretter en elev
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Parent = parent;
            School = schoolName;
            SchoolClass = className;
        }
        public Student(int studentID, string firstName, string middleName, string lastName, Parent parent, School school, SchoolClass schoolClass)//Denne konstructør bruges når f.eks. at finde en elev
        {
            StudentID = studentID;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Parent = parent;
            School = school;
            SchoolClass = schoolClass;
        }

        public override string ToString()
        {
            return $"StudentID: {StudentID}, First name: {FirstName}, MiddleName: {MiddleName}, LastName: {LastName}, Parent: {Parent}, School: {School}, SchoolClass: {SchoolClass}";
        }
    }
}
