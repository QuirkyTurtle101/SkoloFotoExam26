using System.ComponentModel.DataAnnotations;

namespace SkoloFotoExam26.Models
{
    public class PhotographingEvent
    {

        public int PhotographingEventID { get; private set; }

        
        public DateTime Start { get; private set; }
        
        public DateTime End { get; private set; }

        public SchoolSecretary SchoolSecretary { get; private set; }

        public Photographer Photographer { get; private set; }

        public PhotographingEvent()
        {
            
        }
        public PhotographingEvent(DateTime start, DateTime end, SchoolSecretary schoolSecretary, Photographer photographer)
        {
            //PhotographingEventID = _counter++;
            Start = start;
            End = end;
            SchoolSecretary = schoolSecretary;
            Photographer = photographer;
        }
        public PhotographingEvent(int photographingEventID, DateTime start, DateTime end, SchoolSecretary schoolSecretary, Photographer photographer)
        {
            PhotographingEventID = photographingEventID;
            Start = start;
            End = end;
            SchoolSecretary = schoolSecretary;
            Photographer = photographer;
        }
        public override string ToString()
        {
            return $"SchoolSecretaryID: {PhotographingEventID}, Start: {Start}, End: {End}, SchoolSecretaryID: " +
                $"{SchoolSecretary.SchoolSecretaryID}, PhotographerID: {Photographer.PhotographerID}";
        }
    }
}
