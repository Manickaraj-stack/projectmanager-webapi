using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using sba_webapi;

namespace sba_webapi.Controllers
{
    public class TasksController : ApiController
    {
        private ProjectManagerEntities db = new ProjectManagerEntities();

        // GET: api/Tasks
        public IEnumerable<Tasks> GetTasks()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var tasks = from t in db.Tasks
                           join p in db.ParentTasks on t.ParentId equals p.ParentId
                           join s in db.Projects on t.ProjectId equals s.ProjectId
                           join r in db.Users on s.ID equals r.ID
                           select new Tasks
                           {
                               TaskId = t.TaskId,
                               ProjectId = s.ProjectId,
                               ParentId = p.ParentId,
                               UserId = r.ID,
                               StartDate = s.StartDate,
                               EndDate = s.EndDate,
                               IsParentTask = t.IsParentTask,
                               TaskPriority = t.TaskPriority,
                               ProjectName = s.ProjectName,
                               ParentTaskName = p.ParentTask1,
                               UserName = r.FirstName,
                               TaskName = t.TaskName
                           };

            return tasks;
        }

        // GET: api/Tasks/5
        [ResponseType(typeof(Task))]
        public IHttpActionResult GetTask(int id)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // PUT: api/Tasks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTask(int id, Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != task.TaskId)
            {
                return BadRequest();
            }

            db.Entry(task).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tasks
        [ResponseType(typeof(Task))]
        public IHttpActionResult PostTask(Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tasks.Add(task);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = task.TaskId }, task);
        }

        // DELETE: api/Tasks/5
        [ResponseType(typeof(Task))]
        public IHttpActionResult DeleteTask(int id)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            db.Tasks.Remove(task);
            db.SaveChanges();

            return Ok(task);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskExists(int id)
        {
            return db.Tasks.Count(e => e.TaskId == id) > 0;
        }
    }
}
