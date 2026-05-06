// See https://aka.ms/new-console-template for more information
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using SkoloFotoExam26.Services;

SchoolSecretary schoolSecretary1 = new SchoolSecretary("F. L.", "FirstName", "LastName", "PhoneNumber", "Email");
Repo<int, SchoolSecretary> SchoolSecretaryRepo = new Repo<int, SchoolSecretary>();
SchoolSecretaryRepo.AddKeyValue(schoolSecretary1.SchoolSecretaryID, schoolSecretary1);
foreach (SchoolSecretary s in SchoolSecretaryRepo.GetAll())
{
    Console.WriteLine(s);
}
