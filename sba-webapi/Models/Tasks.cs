using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projectmanager.Models
{
    public class Tasks
    {
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
        public int ParentId { get; set; }
        public Nullable<bool> IsParentTask { get; set; }
        public string TaskName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int UserId { get; set; }
        public int TaskPriority { get; set; }
        public string ProjectName { get; set; }
        public string ParentTaskName { get; set; }
        public string UserName { get; set; }

        public virtual ParentTask ParentTask { get; set; }
        public virtual Project Project { get; set; }
        public virtual User User { get; set; }
    }
}
