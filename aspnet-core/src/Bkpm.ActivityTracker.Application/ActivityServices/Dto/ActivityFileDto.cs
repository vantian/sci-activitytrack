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
    [AutoMapFrom(typeof(ActivityFiles))]
    public class ActivityFileDto : EntityDto<int>
    {
        [Required]
        [StringLength(255)]
        public string Nama { get; set; }
        public string Url { get; set; }
        public DateTime? CreationTime { get; set; }
        public string CreatorName { get; set; }
        [Required]
        public int? ActivityDetailId { get; set; }
    }
}
