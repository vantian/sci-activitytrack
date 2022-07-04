using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bkpm.ActivityTracker.ActivityEntity
{
    public class ActivityGroup : Entity, IAudited, ISoftDelete
    {
        public string Nama { get; set; }
        public bool IsDeleted { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }


        //Reverse FK
        public virtual ICollection<ActivityDetails> ActivityDetails { get; set; }

        public ActivityGroup()
        {
            this.ActivityDetails = new List<ActivityDetails>();
        }

    }
}
