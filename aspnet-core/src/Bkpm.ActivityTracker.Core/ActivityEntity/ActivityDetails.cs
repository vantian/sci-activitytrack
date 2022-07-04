using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bkpm.ActivityTracker.ActivityEntity
{
    public class ActivityDetails : Entity, IAudited, ISoftDelete
    {
        public int ActivityGroupId { get; set; }
        public string Nama { get; set; }
        public string Deskripsi { get; set; }
        public int Progress { get; set; }
        public bool IsDeleted { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime TanggalKegiatan { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }

        [ForeignKey(nameof(ActivityGroupId))]
        public virtual ActivityGroup ActivityGroup { get; set; }

        //Reverse FK
        public virtual ICollection<ActivityFiles> ActivityFiles { get; set; }
        public virtual ICollection<ActivityLog> ActivityLog { get; set; }
    }
}
