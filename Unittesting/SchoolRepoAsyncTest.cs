using SkoloFotoExam26.Models;
using SkoloFotoExam26.Services;

namespace Unittesting
{
    [TestClass]
    public sealed class SchoolRepoAsyncTest
    {
        [TestMethod]
        public async Task AddSchool()
        {
            //Arrange 
            SchoolRepoAsync schoolRepo = new SchoolRepoAsync();
            School school = new School("Absalons skole", "Absalonsgade 2", "Roskilde", 4000, SchoolType.Public);
            int countBeforeAdd = await schoolRepo.CountAsync();
            
            //Act
            await schoolRepo.AddAsync(school);
            int countAfterAdd = await schoolRepo.CountAsync();

            //Assert
            Assert.AreEqual(countBeforeAdd + 1, countAfterAdd);
        }
    }
}
