using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Bkpm.ActivityTracker.ActivityEntity;
using Bkpm.ActivityTracker.ActivityServices.Dto;
using Bkpm.ActivityTracker.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Bkpm.ActivityTracker.ActivityServices
{
    [AbpAuthorize(PermissionNames.Pages_ActivityGroup)]
    public class ActivityGroupAppService : AsyncCrudAppService<ActivityGroup, ActivityGroupDto, int, PagedActivityGroupResultRequestDto, CreateOrEditActivityGroupDto, ActivityGroupDto>, IActivityGroupAppService
    {

        public ActivityGroupAppService(
            IRepository<ActivityGroup, int> repository
        )
            : base(repository)
        {
        }

        public async Task<ActivityGroupDashboardDto> GetForDashboard(int id)
        {
            var entity = await Repository.GetAllIncluding(e => e.ActivityDetails).AsNoTracking()
                .WhereIf(id > 0, e => e.Id == id)
                .WhereIf(id == 0, e => e.ActivityDetails.Count() > 0)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                return default;
            }

            var entityDto = new ActivityGroupDashboardDto() { Id = entity.Id };

            ObjectMapper.Map(entity.ActivityDetails, entityDto.ActivityDetails);
            return entityDto;
        }

        public async Task<ActivityGroupDto> CreateOrUpdateAsync(CreateOrEditActivityGroupDto input)
        {
            if (input.Id <= 0)
            {
                //create
                return await this.CreateAsync(input);
            }
            else
            {
                return await this.UpdateAsync(input);
            }
        }

        public override async Task<ActivityGroupDto> CreateAsync(CreateOrEditActivityGroupDto input)
        {
            CheckCreatePermission();

            ActivityGroup entity = ObjectMapper.Map<ActivityGroup>(input);

            await base.Repository.InsertAndGetIdAsync(entity);

            return MapToEntityDto(entity);
        }

        public new async Task<ActivityGroupDto> UpdateAsync(CreateOrEditActivityGroupDto input)
        {
            CheckUpdatePermission();

            var entity = await Repository.GetAsync(input.Id);

            ObjectMapper.Map(input, entity);

            await Repository.UpdateAsync(entity);

            return MapToEntityDto(entity);
        }

        protected override IQueryable<ActivityGroup> CreateFilteredQuery(PagedActivityGroupResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Nama.ToLower().Contains(input.Keyword.ToLower()))
                .OrderByDescending(e => e.CreationTime).ThenByDescending(e => e.LastModificationTime);

        }

        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();

            await Repository.DeleteAsync(input.Id);
        }
    }
}
