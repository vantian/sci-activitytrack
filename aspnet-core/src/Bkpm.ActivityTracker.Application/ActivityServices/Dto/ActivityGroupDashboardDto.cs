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
    public class ActivityGroupDashboardDto : EntityDto<int>
    {
        public virtual ICollection<ActivityDetailsDto> ActivityDetails { get; set; }

        public ActivityGroupDashboardDto()
        {
            ActivityDetails = new List<ActivityDetailsDto>();
        }
    }
}
