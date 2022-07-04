using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Bkpm.ActivityTracker.ActivityEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bkpm.ActivityTracker.ActivityServices.Dto
{
    [AutoMapFrom(typeof(ActivityGroup))]
    public class ActivityGroupDto : EntityDto<int>
    {
        [Required]
        [StringLength(255)]
        public string Nama { get; set; }
        public DateTime? CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }

        public DateTime ActivityAudit => !this.LastModificationTime.HasValue ? this.CreationTime.Value : this.LastModificationTime.Value;
    }
}
