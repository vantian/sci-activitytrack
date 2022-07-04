using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bkpm.ActivityTracker.Authorization.Users;

namespace Bkpm.ActivityTracker.ActivityEntity
{
    public class ActivityLog : Entity, IHasCreationTime
    {
        public int ActivityDetailId { get; set; }
        public long UserId { get; set; }
        public DateTime CreationTime { get; set; }
        public string Deskripsi { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(ActivityDetailId))]
        public virtual ActivityDetails ActivityDetail { get; set; }

    }
}
