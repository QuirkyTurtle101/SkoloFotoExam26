namespace SkoloFotoExam26.Models
{
    public class Teacher : User
    {
        public int TeacherID { get; private set; }



        public Teacher(int teacherID, string firstName, string lastName, string phoneNumber, string email) 
            : base(firstName, lastName, phoneNumber, email)
        {
            TeacherID = teacherID;
        }

        public override string ToString()
        {
            return $"TeacherID: {TeacherID}, {base.ToString()}";
        }
    }
}
