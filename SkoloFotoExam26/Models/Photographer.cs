namespace SkoloFotoExam26.Models
{
    public class Photographer : User
    {
        public int PhotographerID { get; private set; }

        public string Website { get; private set; }

        public string CVRNumber { get; private set; }

        public string StreetName { get; private set; }

        public string City { get; private set; }

        public string PostCode { get; private set; }
        public int ExperienceInYears { get; private set; }

        public int MaxTravelRadiusInKm { get; private set; }

        public string Instagram { get; private set; }

        public string Facebook { get; private set; }


        public Photographer(string firstName, string lastName, string phoneNumber, string email, string website, string cvrNumber,
            string city, string postCode, string streetName, int experienceInYears, int maxTravelRadiusInKm, string instagram,
            string facebook) 
            : base(firstName, lastName, phoneNumber, email)
        {
            Website = website;
            CVRNumber = cvrNumber;
            City = city;
            PostCode = postCode;
            StreetName = streetName;
            ExperienceInYears = experienceInYears;
            MaxTravelRadiusInKm = maxTravelRadiusInKm;
            Instagram = instagram;
            Facebook = facebook;
                
        }

        public override string ToString()
        {
            return $"PhotographerID: {PhotographerID}, {base.ToString()}, WebPage: {Website}, CVRNumber: {CVRNumber}, " +
                $"City: {City}, PostCode: {PostCode}, TheAddress: {StreetName}, ExperienceInYears: {ExperienceInYears}, " +
                $"MaxTravelRadiusInKm: {MaxTravelRadiusInKm}, Instagram: {Instagram}, Facebook: {Facebook}";
        }


    }
}
