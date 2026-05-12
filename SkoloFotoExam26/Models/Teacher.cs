namespace SkoloFotoExam26.Models
{
    public class Teacher : User
    {

        public string Initials { get; private set; }

        public School TheSchool { get; private set; }

        public Teacher()
        {
            
        }
        public Teacher(int teacherID, string initials, string firstName, string lastName, string phoneNumber, string email, 
            School theSchool) 
            : base(teacherID, firstName, lastName, phoneNumber, email)
        {
            Initials = initials;
            TheSchool = theSchool;
        }
        public Teacher(string initials, string firstName, string lastName, string phoneNumber, string email,
            School theSchool)
            : base(firstName, lastName, phoneNumber, email)
        {
            Initials = initials;
            TheSchool = theSchool;
        }

        public override string ToString()
        {
            return $"TeacherID: {ID}, Initials: {Initials}, {base.ToString()}";
        }
    }
}
