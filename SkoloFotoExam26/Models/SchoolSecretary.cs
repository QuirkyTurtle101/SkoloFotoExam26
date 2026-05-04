namespace SkoloFotoExam26.Models
{
    public class SchoolSecretary : User
    {

        private static int _counter = 1;
        public int SchoolSecretaryID { get; private set; }

        public string Initials { get; private set; }
        public SchoolSecretary(string firstName, string lastName, string initials, string phoneNumber, string email, string password) :
            base(firstName, lastName, phoneNumber, email, password)
        {
            SchoolSecretaryID = _counter++;
            Initials = initials;
            //char tempFirstName = firstName[0];
            //char tempLastName = lastName[0];
            //Initials = $"{tempFirstName}. {tempLastName}.";
        }



        public override string ToString()
        {
            return $"ID: {SchoolSecretaryID}, Initials: {Initials}, {base.ToString()}";
        }
    }
}
