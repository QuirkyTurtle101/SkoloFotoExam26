using SkoloFotoExam26.Models;
using SkoloFotoExam26.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unittesting
{
    [TestClass]
    public sealed class ParentRepoAsyncTest
    {
        [TestMethod]
        public async Task AddParent()
        {
            //Arrange 
            ParentRepoAsync parentRepo = new ParentRepoAsync();
            Parent parent = new Parent(1, "Kasper", "Laursen", "kasp@gmail.com", "20102010", "Solbakkevej 2", 4000, "Roskilde");
            int countBeforeAdd = await parentRepo.CountAsync();

            //Act
            await parentRepo.AddAsync(parent);
            int countAfterAdd = await parentRepo.CountAsync();

            //Assert
            Assert.AreEqual(countBeforeAdd + 1, countAfterAdd);
        }
    }
}
