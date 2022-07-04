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
    [AutoMapTo(typeof(ActivityDetails))]
    public class CreateOrEditActivityDetailsDto :  EntityDto<int>
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
    }
}
