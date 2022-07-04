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
using Bkpm.ActivityTracker.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Bkpm.ActivityTracker.ActivityServices
{
    public class ActivityDetailsAppService : AsyncCrudAppService<ActivityDetails, ActivityDetailsDto, int, PagedActivityDetailsResultRequestDto, CreateOrEditActivityDetailsDto, ActivityDetailsDto>, IActivityDetailsAppService
    {
        IRepository<ActivityLog, int> _logrepository;
        IRepository<User, long> _userRepository;
        IRepository<ActivityFiles, int> _activityFileRepository;
        public ActivityDetailsAppService(
            IRepository<ActivityDetails, int> repository
            , IRepository<ActivityLog, int> logrepository
            , IRepository<User, long> userRepository
            , IRepository<ActivityFiles, int> activityFileRepository
        )
            : base(repository)
        {
            _logrepository = logrepository;
            _userRepository = userRepository;
            _activityFileRepository = activityFileRepository;
        }

        //public async Task<ActivityGroupDto> GetForDashboard(int id)
        //{
        //    if (id = 0)
        //    { 
                
        //    }

        //    var entity = await Repository.GetAll().AsNoTracking()
        //        .WhereIf(id > 0, e => e.Id == id)
        //        .FirstOrDefaultAsync();

        //    if (entity == null)
        //    {
        //        return default;
        //    }

        //    var entityDto = MapToEntityDto(entity);

        //    ObjectMapper.Map(entity.ActivityDetails, entityDto.ActivityDetails);
        //    return entityDto;
        //}

        public async Task<ActivityDetailsDto> CreateOrUpdateAsync(CreateOrEditActivityDetailsDto input)
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

        public override async Task<ActivityDetailsDto> CreateAsync(CreateOrEditActivityDetailsDto input)
        {
            ActivityDetails entity = ObjectMapper.Map<ActivityDetails>(input);

            await base.Repository.InsertAndGetIdAsync(entity);
            await LogUser(entity);

            return MapToEntityDto(entity);
        }

        public async Task<ActivityDetailsDto> GetForView(int id)
        {
            var entity = await Repository.GetAllIncluding(e => e.ActivityGroup).AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
            {
                return default;
            }

            var entityDto = MapToEntityDto(entity);

            ObjectMapper.Map(entity.ActivityGroup, entityDto.ActivityGroup);

            var creator = await _userRepository.GetAsync(entity.CreatorUserId.Value);
            entityDto.CreatorName = creator.FullName;

            if (entity.LastModifierUserId.HasValue)
            { 
                var editor = await _userRepository.GetAsync(entity.LastModifierUserId.Value);
                entityDto.LastEditorName = editor.FullName;
            }
            return entityDto;
        }

        public async Task<PagedResultDto<ActivityFileDto>> GetLampiranByActivityId(PagedActivityFileResultRequestDto input)
        {
            var output = new PagedResultDto<ActivityFileDto>();
            var entity = await _activityFileRepository.GetAllIncluding(e => e.Creator).AsNoTracking().Where(e => e.ActivityDetailId == input.ActivityDetailId).ToListAsync();

            if (entity.Count() == 0)
            {
                return output;
            }

            var entityDto = new List<ActivityFileDto>();
            ObjectMapper.Map(entity, entityDto);

            foreach (var a in entityDto)
            {
                foreach (var b in entity.Where(e => e.Id == a.Id))
                {
                    a.CreatorName = b.Creator.FullName;
                }
            }

            output.Items = entityDto;
            output.TotalCount = entityDto.Count();

            return output;
        }

        public async Task<ActivityFileDto> CreateLampiranByActivityId(CreateActivityFileDto input)
        {
            var output = new ActivityFileDto();
            ActivityFiles entity = ObjectMapper.Map<ActivityFiles>(input);

            await _activityFileRepository.InsertAndGetIdAsync(entity);
            await LogUser(new ActivityDetails() { Id = input.ActivityDetailId.Value, Deskripsi = $"Upload Lampiran: {input.Nama}" });

            ObjectMapper.Map(entity, output);

            return output;
        }

        public new async Task<ActivityDetailsDto> UpdateAsync(CreateOrEditActivityDetailsDto input)
        {
            var entity = await Repository.GetAsync(input.Id);

            ObjectMapper.Map(input, entity);

            await Repository.UpdateAsync(entity);
            await LogUser(entity);

            return MapToEntityDto(entity);
        }

        protected override IQueryable<ActivityDetails> CreateFilteredQuery(PagedActivityDetailsResultRequestDto input)
        {
            return Repository.GetAll().Include(e => e.ActivityGroup)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => 
                    x.Nama.ToLower().Contains(input.Keyword.ToLower()) 
                    || x.Deskripsi.ToLower().Contains(input.Keyword.ToLower()) 
                    || x.ActivityGroup.Nama.ToLower().Contains(input.Keyword.ToLower())
                )
                .WhereIf(input.ActivityGroupId.HasValue, x => x.ActivityGroupId == input.ActivityGroupId.Value)
                ;
        }

        [AbpAuthorize(PermissionNames.Pages_ActivityGroup)]
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();

            await Repository.DeleteAsync(input.Id);
            await LogUser(new ActivityDetails() { Id = input.Id, Deskripsi = "Hapus Data" });
        }

        private async Task LogUser(ActivityDetails entity)
        {
            var s = AbpSession.UserId.Value;
            string jsonString = JsonSerializer.Serialize(new { Nama = entity.Nama, Deskripsi = entity.Deskripsi, Progress = entity.Progress, TanggalKegiatan = entity.TanggalKegiatan.ToString("dd-MM-yyyy") });
            await _logrepository.InsertAsync(new ActivityLog()
            {
                ActivityDetailId = entity.Id,
                UserId = AbpSession.UserId.Value,
                Deskripsi = jsonString
            });
        }
    }
}
