using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;
using Abp.Application.Services.Dto;
using Bkpm.ActivityTracker.ActivityServices;
using Bkpm.ActivityTracker.ActivityServices.Dto;

namespace Bkpm.ActivityTracker.Tests.ActivityGroup
{
    public class ActivityGroupAppService_Tests : ActivityTrackerTestBase
    {
        private readonly IActivityGroupAppService _appService;

        public ActivityGroupAppService_Tests()
        {
            _appService = Resolve<IActivityGroupAppService>();
        }

        [Fact]
        public async Task CreateActivityGroup_Test()
        {
            // Act
            await _appService.CreateAsync(
                new CreateOrEditActivityGroupDto
                {
                    Nama= "Test"
                });

            await UsingDbContextAsync(async context =>
            {
                var entity = await context.ActivityGroup.FirstOrDefaultAsync(u => u.Nama == "Test");
                entity.ShouldNotBeNull();
            });
        }

        [Fact]
        public async Task GetForList_Test()
        {
            // Act
            await _appService.CreateAsync(
                new CreateOrEditActivityGroupDto
                {
                    Nama = "Test"
                });

            // Act
            var output = await _appService.GetAllAsync(new PagedActivityGroupResultRequestDto { MaxResultCount = 20, SkipCount = 0, Keyword = "test" });

            // Assert
            output.Items.Count.ShouldBeGreaterThan(0);
        }
    }
}
