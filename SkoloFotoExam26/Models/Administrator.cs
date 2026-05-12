namespace SkoloFotoExam26.Models
{
    public class Administrator : User
    {

        public Administrator(string firstName, string lastName, string phoneNumber, string email) : base(firstName, lastName, phoneNumber, email)
        {

        }

        public override string ToString()
        {
            return $"{base.ToString()}";
        }
    }
}
