using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sba_webapi.Models
{
    public class Projects
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public Nullable<bool> IsSetdate { get; set; }
        public int ID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int ProjectPriority { get; set; }

        public virtual User User { get; set; }
    }
}
