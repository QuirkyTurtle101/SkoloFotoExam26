namespace SkoloFotoExam26.Models
{
    public class PhotographingEvent
    {
        //private static int _counter = 1;

        public int PhotographingEventID { get; private set; }
        public DateTime Start { get; private set; }

        public DateTime End { get; private set; }

        public SchoolSecretary SchoolSecretary { get; private set; }
        public PhotographingEvent(DateTime start, DateTime end, SchoolSecretary schoolSecretary)
        {
            //PhotographingEventID = _counter++;
            Start = start;
            End = end;
            SchoolSecretary = schoolSecretary;

        }

        public override string ToString()
        {
            return $"SchoolSecretaryID: {PhotographingEventID}, Start: {Start}, End: {End}, SchoolSecretaryID: {SchoolSecretary.SchoolSecretaryID}";
        }
    }
}
