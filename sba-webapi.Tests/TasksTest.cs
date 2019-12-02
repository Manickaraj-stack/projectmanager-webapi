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
    public class TasksTest
    {
        [TestMethod]
        public void AddTask()
        {
            var mockSet = new Mock<DbSet<Task>>();

            var mockContext = new Mock<ProjectManagerEntities>();
            mockContext.Setup(m => m.Tasks).Returns(mockSet.Object);

            var service = new TasksController(mockContext.Object);
            service.PostTask(
                new Task
                {
                    TaskName = "complete e-learnings",
                    IsParentTask = false,
                    ParentId = 1,
                    ProjectId = 2,
                    UserId = 2,
                    StartDate = "11/20/2019",
                    EndDate = "12/25/2019",
                    TaskPriority = 5
                });
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        //[TestMethod]
        //public void GetTasks()
        //{
        //    var dataTask = new List<Task>
        //    {
        //        new Task
        //        {
        //            TaskName = "complete e-learnings",
        //            IsParentTask = false,
        //            ParentId = 1,
        //            ProjectId = 3,
        //            UserId = 2,
        //            StartDate = "11/20/2019",
        //            EndDate = "12/25/2019",
        //            TaskPriority = 5
        //        },
        //        new Task
        //        {
        //            TaskName = "complete assignments",
        //            IsParentTask = false,
        //            ParentId = 1,
        //            ProjectId = 1,
        //            UserId = 1,
        //            StartDate = "11/20/2019",
        //            EndDate = "12/25/2019",
        //            TaskPriority = 3
        //        },
        //        new Task
        //        {
        //            TaskName = "complete class room sessions",
        //            IsParentTask = false,
        //            ParentId = 1,
        //            ProjectId = 2,
        //            UserId = 2,
        //            StartDate = "11/20/2019",
        //            EndDate = "12/25/2019",
        //            TaskPriority = 10
        //        }
        //    }.AsQueryable();

        //    var dataUser = new List<User>
        //    {
        //        new User { ID = 1, FirstName = "Matt"  },
        //        new User { ID = 2, FirstName = "john"  }
        //    }.AsQueryable();

        //    var dataProject = new List<Project>
        //    {
        //        new Project { ProjectId = 1, ProjectName = "AAA", ProjectPriority = 10, StartDate = "11/15/2019", EndDate = "12/15/2019", IsSetdate = false, ID = 1 },
        //        new Project { ProjectId = 2, ProjectName = "BBB", ProjectPriority = 10, StartDate = "11/15/2019", EndDate = "12/15/2019", IsSetdate = false, ID = 2 },
        //        new Project { ProjectId = 3, ProjectName = "ZZZ", ProjectPriority = 10, StartDate = "11/15/2019", EndDate = "12/15/2019", IsSetdate = false, ID = 1 },
        //    }.AsQueryable();

        //    var dataParentTask = new List<ParentTask>
        //    {
        //        new ParentTask{ ParentId = 11, ParentTask1 = "Complete FSD"},
        //        new ParentTask{ ParentId = 12, ParentTask1 = "Complete AIML"},
        //    }.AsQueryable();

        //    var mockSetTask = new Mock<DbSet<Task>>();
        //    var mockSetParentTask = new Mock<DbSet<ParentTask>>();
        //    var mockSetProject = new Mock<DbSet<Project>>();
        //    var mockSetUser = new Mock<DbSet<User>>();

        //    mockSetTask.As<IQueryable<Task>>().Setup(m => m.Provider).Returns(dataTask.Provider);
        //    mockSetTask.As<IQueryable<Task>>().Setup(m => m.Expression).Returns(dataTask.Expression);
        //    mockSetTask.As<IQueryable<Task>>().Setup(m => m.ElementType).Returns(dataTask.ElementType);
        //    mockSetTask.As<IQueryable<Task>>().Setup(m => m.GetEnumerator()).Returns(dataTask.GetEnumerator());

        //    mockSetParentTask.As<IQueryable<ParentTask>>().Setup(m => m.Expression).Returns(dataParentTask.Expression);
        //    mockSetProject.As<IQueryable<Project>>().Setup(m => m.Expression).Returns(dataProject.Expression);
        //    mockSetUser.As<IQueryable<User>>().Setup(m => m.Expression).Returns(dataUser.Expression);

        //    var mockContext = new Mock<ProjectManagerEntities>();
        //    mockContext.Setup(m => m.Tasks).Returns(mockSetTask.Object);
        //    mockContext.Setup(m => m.ParentTasks).Returns(mockSetParentTask.Object);
        //    mockContext.Setup(m => m.Projects).Returns(mockSetProject.Object);
        //    mockContext.Setup(m => m.Users).Returns(mockSetUser.Object);

        //    var service = new TasksController(mockContext.Object);
        //    var tasks = service.GetTasks();

        //    Assert.AreEqual(3, tasks.Count());
        //    Assert.AreEqual("complete e-learnings", tasks.ToList()[0].TaskName);
        //    Assert.AreEqual("complete assignments", tasks.ToList()[1].TaskName);
        //    Assert.AreEqual("complete class room sessions", tasks.ToList()[2].TaskName);
        //}
    }
}
