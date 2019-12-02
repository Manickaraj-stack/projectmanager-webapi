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
    public class ProjectsTest
    {
        [TestMethod]
        public void AddProject()
        {
            var mockSet = new Mock<DbSet<Project>>();

            var mockContext = new Mock<ProjectManagerEntities>();
            mockContext.Setup(m => m.Projects).Returns(mockSet.Object);

            var service = new ProjectsController(mockContext.Object);
            service.PostProject(new Project { ProjectName = "Complete FSE", ProjectPriority = 10, StartDate = "11/25/2019", EndDate = "12/22/2019", ID = 1, IsSetdate = false });
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void GetProject()
        {
            var data = new List<Project>
            {
                new Project { ProjectName = "AAA", ProjectPriority = 10, StartDate = "11/15/2019", EndDate = "12/15/2019", IsSetdate = false, ID = 1 },
                new Project { ProjectName = "BBB", ProjectPriority = 10, StartDate = "11/15/2019", EndDate = "12/15/2019", IsSetdate = false, ID = 2 },
                new Project { ProjectName = "ZZZ", ProjectPriority = 10, StartDate = "11/15/2019", EndDate = "12/15/2019", IsSetdate = false, ID = 1 },
            }.AsQueryable();

            var dataUser = new List<User>
            {
                new User { ID = 1, FirstName = "Matt"  },
                new User { ID = 2, FirstName = "john"  }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Project>>();
            var mockSetUser = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<Project>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Project>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Project>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Project>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockSetUser.As<IQueryable<User>>().Setup(m => m.Expression).Returns(dataUser.Expression);

            var mockContext = new Mock<ProjectManagerEntities>();
            mockContext.Setup(m => m.Projects).Returns(mockSet.Object);
            mockContext.Setup(m => m.Users).Returns(mockSetUser.Object);

            var service = new ProjectsController(mockContext.Object);
            var projects = service.GetProjects();

            Assert.AreEqual(3, projects.Count());
            Assert.AreEqual("AAA", projects.ToList()[0].ProjectName);
            Assert.AreEqual("BBB", projects.ToList()[1].ProjectName);
            Assert.AreEqual("ZZZ", projects.ToList()[2].ProjectName);
        }
    }
}
