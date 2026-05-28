using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using SkoloFotoExam26.Services;

namespace Unittesting;

[TestClass]
public class TeacherRepoTest
{



    [TestMethod]
    public async Task TeacherAddedTest()
    {
        LoginRepoAsync loginRepo = new LoginRepoAsync();
        LoginInfo loginInfo = new LoginInfo("A@gmail.com", "123", UserType.Teacher);
        await loginRepo.AddAsync(loginInfo);

        SchoolRepoAsync schoolRepo = new SchoolRepoAsync();
        TeacherRepoAsync teacherRepo = new TeacherRepoAsync();
        School school = await schoolRepo.GetAsync(2);
        Teacher teacher = new Teacher("A. E.", "Anton", "Eriksen", "99889988", "A@gmail.com", school);
        int beforeCount = await teacherRepo.CountAsync();
        await teacherRepo.AddAsync(teacher);
        int afterCount = await teacherRepo.CountAsync();
        Assert.AreEqual(beforeCount + 1, afterCount);
    }



}
