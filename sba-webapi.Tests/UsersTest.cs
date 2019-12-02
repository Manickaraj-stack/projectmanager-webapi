using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using sba_webapi;
using sba_webapi.Controllers;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ProjectManager.Tests
{
    [TestClass]
    public class UsersTest
    {
        [TestMethod]
        public void AddUser()
        {
            var mockSet = new Mock<DbSet<User>>();

            var mockContext = new Mock<ProjectManagerEntities>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var service = new UsersController(mockContext.Object);
            service.PostUser(new User { ID = 1, FirstName = "Matt", LastName = "debolt" });
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void GetUser()
        {
            var data = new List<User>
            {
                new User { ID = 1, FirstName = "Matt", LastName = "Debolt" },
                new User { ID = 2, FirstName = "John", LastName = "flynn" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();

            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<ProjectManagerEntities>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var service = new UsersController(mockContext.Object);
            var users = service.GetUsers().ToList();


            Assert.AreEqual(2, users.Count());
            Assert.AreEqual("Matt", users[0].FirstName);
            Assert.AreEqual("John", users[1].FirstName);
        }
    }
}