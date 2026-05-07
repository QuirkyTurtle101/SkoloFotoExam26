namespace SkoloFotoExam26.Models
{
    public class SchoolClass
    {


        public int SchoolClassID { get; private set; }
        public string ClassName { get; private set; }

        public School TheSchool { get; private set; }

        public SchoolClass(string className, School theSchool)
        {
            ClassName = className;
            TheSchool = theSchool;
        }


        public override string ToString()
        {
            return $"ClassName: {ClassName}";
        }
    }
}
