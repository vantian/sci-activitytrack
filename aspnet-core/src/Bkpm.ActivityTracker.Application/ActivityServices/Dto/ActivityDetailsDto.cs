using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Bkpm.ActivityTracker.ActivityEntity;
using Bkpm.ActivityTracker.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bkpm.ActivityTracker.ActivityServices.Dto
{
    [AutoMapFrom(typeof(ActivityDetails))]
    public class ActivityDetailsDto : EntityDto<int>
    {
        [Required]
        public int? ActivityGroupId { get; set; }

        [Required]
        [StringLength(255)]
        public string Nama { get; set; }

        [Required]
        [StringLength(500)]
        public string Deskripsi { get; set; }

        [Required]
        public int? Progress { get; set; }

        [Required]
        public DateTime? TanggalKegiatan { get; set; }

        public DateTime? CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }

        public DateTime ActivityAudit => !this.LastModificationTime.HasValue ? this.CreationTime.Value : this.LastModificationTime.Value;

        public ActivityGroupDto ActivityGroup { get; set; }
        public string CreatorName { get; set; }
        public string LastEditorName { get; set; }
    }
}
