namespace SkoloFotoExam26.Models
{
    public class PhotographingEvent
    {
        public DateTime Start { get; private set; }

        public DateTime End { get; private set; }

        public SchoolSecretary SchoolSecretary { get; private set; }
        public PhotographingEvent(DateTime start, DateTime end, SchoolSecretary schoolSecretary)
        {
            Start = start;
            End = end;
            SchoolSecretary = schoolSecretary;

        }

        public override string ToString()
        {
            return $"Start: {Start}, End: {End}, SchoolSecretaryID: {SchoolSecretary.SchoolSecretaryID}";
        }
    }
}
