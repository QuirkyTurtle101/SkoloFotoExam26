using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using SkoloFotoExam26.Services;

namespace Unittesting;

[TestClass]
public class TeacherRepoTest
{

    //[TestMethod]
    //public async Task TeacherCountTest()
    //{

    //    LoginRepoAsync loginRepo = new LoginRepoAsync();
    //    LoginInfo loginInfo = new LoginInfo("Anton@gmail.com", "123", UserType.Teacher);
    //    await loginRepo.AddAsync(loginInfo);

    //    List<Teacher> teacherList = new List<Teacher>();
    //    TeacherRepoAsync teacherRepo = new TeacherRepoAsync();
    //    SchoolRepoAsync schoolRepo = new SchoolRepoAsync();

    //    School school = await schoolRepo.GetAsync(2);
    //    //School school = new School("SchoolName", "StreetName", "City", 4000, SchoolType.Public);
    //    //await schoolRepo.AddAsync(school);
    //    Teacher teacher = new Teacher("A. E.", "Anton", "Eriksen", "99889988", "Anton@gmail.com", school);
    //    int before = await teacherRepo.CountAsync();
    //    await teacherRepo.AddAsync(teacher);
    //    int after = await teacherRepo.CountAsync();
    //    Assert.AreEqual(before + 1, after);
    //}

    [TestMethod]
    public async Task TeacherAddedTest()
    {
        LoginRepoAsync loginRepo = new LoginRepoAsync();
        LoginInfo loginInfo = new LoginInfo("A@gmail.com", "123", UserType.Teacher);
        await loginRepo.AddAsync(loginInfo);

        SchoolRepoAsync schoolRepo = new SchoolRepoAsync();
        TeacherRepoAsync teacherRepo = new TeacherRepoAsync();
        //School school = new School("SchoolName", "StreetName", "City", 4000, SchoolType.Public);
        School school = await schoolRepo.GetAsync(2);
        Teacher teacher = new Teacher("A. E.", "Anton", "Eriksen", "99889988", "A@gmail.com", school);
        int beforeCount = await teacherRepo.CountAsync();
        await teacherRepo.AddAsync(teacher);
        int afterCount = await teacherRepo.CountAsync();
        Assert.AreEqual(beforeCount + 1, afterCount);
    }
    //[TestMethod]
    //public async Task TeacherRemovedTest()
    //{
    //    LoginRepoAsync loginRepo = new LoginRepoAsync();
    //    LoginInfo loginInfo = new LoginInfo("Dan@gmail.com", "123", UserType.Teacher);
    //    await loginRepo.AddAsync(loginInfo);

    //    SchoolRepoAsync schoolRepo = new SchoolRepoAsync();
    //    TeacherRepoAsync teacherRepo = new TeacherRepoAsync();
    //    School school = await schoolRepo.GetAsync(2);
    //    Teacher teacher = new Teacher("D. E.", "Dan", "Eriksen", "77889988", "Dan@gmail.com", school);
    //    await teacherRepo.AddAsync(teacher);
    //    int before = await teacherRepo.CountAsync();
    //    await teacherRepo.DeleteAsync(teacher.ID);
    //    int after = await teacherRepo.CountAsync();
    //    Assert.AreEqual(before - 1, after);
    //}
    //[TestMethod]
    //public async Task TeacherGetTest()
    //{
    //    TeacherRepoAsync teacherRepo = new TeacherRepoAsync();
    //    School school = new School("2SchoolName", "2StreetName", "2City", 4000, SchoolType.Public);
    //    Teacher teacher = new Teacher("E. E.", "Erik", "Eriksen", "66889988", "Erik@gmail.com", school);
    //    await teacherRepo.AddAsync(teacher);
    //    Teacher getTeacher = await teacherRepo.GetAsync(teacher.ID);
    //    Assert.AreEqual(getTeacher, teacher);
    //    Teacher getNull = await teacherRepo.GetAsync(11111);
    //    Assert.AreEqual(null, getNull);
    //}
    //[TestMethod]
    //public async Task TeacherGetAllTest()
    //{


    //}
    //[TestMethod]
    //public async Task TeacherUpdate()
    //{
    //    TeacherRepoAsync teacherRepo = new TeacherRepoAsync();
    //    School school = new School("2SchoolName", "2StreetName", "2City", 4000, SchoolType.Public);
    //    Teacher teacher = new Teacher("E. E.", "Erik", "Eriksen", "66889988", "Erik@gmail.com", school);
    //    await teacherRepo.AddAsync(teacher);
    //    School schoolUpdated = new School("32SchoolName", "32StreetName", "32City", 4000, SchoolType.Public);
    //    Teacher teacherUpdated = new Teacher("A. A.", "A", "A", "11889988", "A@gmail.com", schoolUpdated);
    //    await teacherRepo.UpdateAsync(teacherUpdated);
    //    Teacher theUpdatedTeacher = await teacherRepo.GetAsync(teacher.ID);

    //    Assert.AreEqual(teacherUpdated.ID, theUpdatedTeacher.ID);
    //    Assert.AreEqual(teacherUpdated.Initials, theUpdatedTeacher.Initials);
    //    Assert.AreEqual(teacherUpdated.FirstName, theUpdatedTeacher.FirstName);
    //    Assert.AreEqual(teacherUpdated.LastName, theUpdatedTeacher.LastName);
    //    Assert.AreEqual(teacherUpdated.PhoneNumber, theUpdatedTeacher.PhoneNumber);
    //    Assert.AreEqual(teacherUpdated.Email, theUpdatedTeacher.Email);
    //    Assert.AreEqual(teacherUpdated.TheSchool, theUpdatedTeacher.TheSchool);
    //}


}
