namespace SkoloFotoExam26.Models
{
    public class SchoolSecretary : User
    {

        //private static int _counter = 1;

        public string Initials { get;  set; }

        public School TheSchool { get;  set; }

        public SchoolSecretary()
        {
            
        }

        public SchoolSecretary(string firstName, string lastName, string initials, string phoneNumber, string email, 
            School theSchool) :
            base(firstName, lastName, phoneNumber, email)
        {

            Initials = initials;
            TheSchool = theSchool;
            //SchoolSecretaryID = schoolSecretaryID;
        }

        public SchoolSecretary(int id, string firstName, string lastName, string initials, string phoneNumber, string email,
            School theSchool) : 
            base(id, firstName,lastName, phoneNumber, email)
        {
            Initials = initials;
            TheSchool = theSchool;
        }



        public override string ToString()
        {
            return $"ID: {ID}, Initials: {Initials}, {base.ToString()}";
        }
    }
}
