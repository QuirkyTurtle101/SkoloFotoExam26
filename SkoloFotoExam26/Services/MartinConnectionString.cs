namespace SkoloFotoExam26.Services
{
    public abstract class MartinConnectionString
    {
        protected String mConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = SkoleFotoDB; Integrated Security = True; Connect Timeout = 30; Encrypt=False;Trust Server Certificate=False;Application Intent = ReadWrite; Multi Subnet Failover=False";
    }
}
