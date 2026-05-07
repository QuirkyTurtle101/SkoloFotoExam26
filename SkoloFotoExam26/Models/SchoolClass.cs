namespace SkoloFotoExam26.Models
{
    public class SchoolClass
    {
        public int SchoolClassID { get; set; }
        public string ClassName { get; set; }
        public School School { get; set; }

        public SchoolClass()
        {
            
        }

        public SchoolClass(int schoolClassID, string className, School school)//Denne bruges hvis man f.eks. skal finde en skoleklasse
        {
            SchoolClassID = schoolClassID;
            ClassName = className;
            School = school;
        }

        public SchoolClass(string className, School school) //Denne konstructør bruges når man opretter en skoleklasse
        {
            ClassName = className;
            School = school;
        }

        public override string ToString()
        {
            return $"SchoolClassID: {SchoolClassID}, Class name: {ClassName}, School: {School}";
        }

    }
}
