namespace SkoloFotoExam26.Models
{
    public class Booking
    {
        public int BookingID { get; private set; }

        public DateTime Start { get; private set; }

        public DateTime End { get; private set; }

        public PhotographingEvent ThePhotographingEvent { get; private set; }

        public Photographer ThePhotographer { get; private set; }

        public SchoolClass TheSchoolClass;

        public Booking(DateTime start, DateTime end, PhotographingEvent thePhotographingEvent, Photographer thePhotographer, 
            SchoolClass theSchoolClass)
        {
            Start = start;
            End = end;
            ThePhotographingEvent = thePhotographingEvent;
            ThePhotographer = thePhotographer;
            TheSchoolClass = theSchoolClass;
        }

        public override string ToString()
        {
            return $"Start: {Start}, End: {End}";
        }
    }
}
