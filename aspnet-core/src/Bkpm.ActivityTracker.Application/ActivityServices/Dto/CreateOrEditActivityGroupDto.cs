using Abp.Application.Services.Dto;
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
    [AutoMapTo(typeof(ActivityGroup))]
    public class CreateOrEditActivityGroupDto : EntityDto<int>
    {
        [Required]
        [StringLength(255)]
        public string Nama { get; set; }
    }
}
