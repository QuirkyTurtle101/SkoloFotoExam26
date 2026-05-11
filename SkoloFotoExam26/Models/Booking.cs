namespace SkoloFotoExam26.Models
{
    public class Booking
    {
        public int BookingID { get; private set; }

        public DateTime Start { get; private set; }

        public DateTime End { get; private set; }

        public PhotographingEvent ThePhotographingEvent { get; private set; }

        public Teacher TheTeacher { get; private set; }

        public SchoolClass TheSchoolClass { get; private set; }

        public Booking()
        {
            
        }
        public Booking(DateTime start, DateTime end, PhotographingEvent thePhotographingEvent, Teacher theTeacher,
            SchoolClass theSchoolClass)
        {
            Start = start;
            End = end;
            ThePhotographingEvent = thePhotographingEvent;
            TheTeacher = theTeacher;
            TheSchoolClass = theSchoolClass;
        }

        public Booking(DateTime start, DateTime end, PhotographingEvent thePhotographingEvent, SchoolClass theSchoolClass, Teacher theTeacher, int id)
        {
            Start = start;
            End = end;
            ThePhotographingEvent = thePhotographingEvent;
            TheSchoolClass = theSchoolClass;
            TheTeacher = theTeacher;
            BookingID = id;
        }

        public override string ToString()
        {
            return $"BookingID: {BookingID}, Start: {Start}, End: {End}";
        }
    }
}
