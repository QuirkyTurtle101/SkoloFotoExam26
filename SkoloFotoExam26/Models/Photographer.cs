namespace SkoloFotoExam26.Models
{
    public class Photographer : User
    {
        public string Website { get;  set; }

        public string CVRNumber { get;  set; }

        public string Street { get;  set; }

        public string City { get;  set; }

        public int ZipCode { get;  set; }
        public int ExperienceInYears { get;  set; }

        public int MaxTravelRadiusInKm { get;  set; }

        public string Instagram { get;  set; }

        public string Facebook { get;  set; }

        public Photographer()
        {
            
        }
        public Photographer(string firstName, string lastName, string phoneNumber, string email, string website, string cvrNumber,
            string city, int zipCode, string street, int experienceInYears, int maxTravelRadiusInKm, string instagram,
            string facebook) : 
            base(firstName, lastName, phoneNumber, email)
        {
            Website = website;
            CVRNumber = cvrNumber;
            City = city;
            ZipCode = zipCode;
            Street = street;
            ExperienceInYears = experienceInYears;
            MaxTravelRadiusInKm = maxTravelRadiusInKm;
            Instagram = instagram;
            Facebook = facebook;
        }

        public Photographer(int id, string firstName, string lastName, string phoneNumber, string email, string website, string cvrNumber,
            string city, int zipCode, string street, int experienceInYears, int maxTravelRadiusInKm, string instagram,
            string facebook) : 
            base(id, firstName, lastName, phoneNumber, email)
        {
            Website = website;
            CVRNumber = cvrNumber;
            City = city;
            ZipCode = zipCode;
            Street = street;
            ExperienceInYears = experienceInYears;
            MaxTravelRadiusInKm = maxTravelRadiusInKm;
            Instagram = instagram;
            Facebook = facebook;
        }

        public override string ToString()
        {
            return $"PhotographerID: {ID}, {base.ToString()}, WebPage: {Website}, CVRNumber: {CVRNumber}, " +
                $"City: {City}, PostCode: {ZipCode}, TheAddress: {Street}, ExperienceInYears: {ExperienceInYears}, " +
                $"MaxTravelRadiusInKm: {MaxTravelRadiusInKm}, Instagram: {Instagram}, Facebook: {Facebook}";
        }


    }
}
