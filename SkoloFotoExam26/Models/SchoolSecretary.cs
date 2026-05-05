namespace SkoloFotoExam26.Models
{
    public class SchoolSecretary : User
    {

        //private static int _counter = 1;
        public int SchoolSecretaryID { get; private set; }

        public string Initials { get; private set; }
        public SchoolSecretary(string firstName, string lastName, string initials, string phoneNumber, string email) :
            base(firstName, lastName, phoneNumber, email)
        {
            //SchoolSecretaryID = _counter++;
            Initials = initials;

        }



        public override string ToString()
        {
            return $"ID: {SchoolSecretaryID}, Initials: {Initials}, {base.ToString()}";
        }
    }
}
