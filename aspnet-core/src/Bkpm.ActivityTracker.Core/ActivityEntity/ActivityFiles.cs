using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Bkpm.ActivityTracker.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bkpm.ActivityTracker.ActivityEntity
{
    public class ActivityFiles : Entity, IAudited, ISoftDelete
    {
        public int ActivityDetailId { get; set; }
        public string Nama { get; set; }
        public string Url { get; set; }
        public bool IsDeleted { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }


        [ForeignKey(nameof(ActivityDetailId))]
        public virtual ActivityDetails ActivityDetail { get; set; }

        [ForeignKey(nameof(CreatorUserId))]
        public virtual User Creator { get; set; }
    }
}
