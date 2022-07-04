using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;
using Abp.Application.Services.Dto;
using Bkpm.ActivityTracker.ActivityServices;
using Bkpm.ActivityTracker.ActivityServices.Dto;

namespace Bkpm.ActivityTracker.Tests.ActivityGroup
{
    public class ActivityDetailsAppService_Tests : ActivityTrackerTestBase
    {
        private readonly IActivityDetailsAppService _appService;

        public ActivityDetailsAppService_Tests()
        {
            _appService = Resolve<IActivityDetailsAppService>();
        }

        [Fact]
        public async Task CreateActivityGroup_Test()
        {
            LoginAsDefaultTenantAdmin();

            // Act
            await _appService.CreateAsync(
                new CreateOrEditActivityDetailsDto
                {
                    Nama= "Test",
                    Deskripsi = "Test",
                    Progress = 100,
                    ActivityGroupId = 1
                });

            await UsingDbContextAsync(async context =>
            {
                var entity = await context.ActivityDetails.FirstOrDefaultAsync(u => u.Nama == "Test");
                entity.ShouldNotBeNull();
            });
        }

        [Fact]
        public async Task GetForList_Test()
        {
            // Act
            await _appService.CreateAsync(
                new CreateOrEditActivityDetailsDto
                {
                    Nama = "Test",
                    Deskripsi = "Test",
                    Progress = 100,
                    ActivityGroupId = 1
                });

            // Act
            var output = await _appService.GetAllAsync(new PagedActivityDetailsResultRequestDto { MaxResultCount = 20, SkipCount = 0, Keyword = "test" });

            // Assert
            output.Items.Count.ShouldBeGreaterThan(0);
        }
    }
}
