namespace SkoloFotoExam26.Models
{
    public class Administrator : User
    {
        public int AdministratorID { get; }

        public Administrator(string firstName, string lastName, string phoneNumber, string email) : base(firstName, lastName, phoneNumber, email)
        {

        }

        public override string ToString()
        {
            return $"AdministratorID: {AdministratorID}" + base.ToString();
        }
    }
}
