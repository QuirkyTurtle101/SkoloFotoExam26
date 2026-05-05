namespace SkoloFotoExam26.Models
{
    public class Photographer : User
    {
        public int PhotographerID { get; private set; }

        public string WebPage { get; private set; }

        public string CVRNumber { get; private set; }

        public string TheAddress { get; private set; }

        public int ExperienceInYears { get; private set; }

        public int MaxTravelRadiusInKm { get; private set; }

        public string Instagram { get; private set; }

        public string Facebook { get; private set; }


        public Photographer(string firstName, string lastName, string phoneNumber, string email, string webPage, string cvrNumber,
            string theAddress, int experienceInYears, int maxTravelRadiusInKm, string instagram, string facebook) 
            : base(firstName, lastName, phoneNumber, email)
        {
            WebPage = webPage;
            CVRNumber = cvrNumber;
            TheAddress = theAddress;
            ExperienceInYears = experienceInYears;
            MaxTravelRadiusInKm = maxTravelRadiusInKm;
            Instagram = instagram;
            Facebook = facebook;
                
        }

        public override string ToString()
        {
            return $"PhotographerID: {PhotographerID}, {base.ToString()}, WebPage: {WebPage}, CVRNumber: {CVRNumber}, " +
                $"TheAddress: {TheAddress}, ExperienceInYears: {ExperienceInYears}, MaxTravelRadiusInKm: {MaxTravelRadiusInKm}, " +
                $"Instagram: {Instagram}, Facebook: {Facebook}";
        }


    }
}
