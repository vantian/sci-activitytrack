using System.Threading.Tasks;
using Bkpm.ActivityTracker.Models.TokenAuth;
using Bkpm.ActivityTracker.Web.Controllers;
using Shouldly;
using Xunit;

namespace Bkpm.ActivityTracker.Web.Tests.Controllers
{
    public class HomeController_Tests: ActivityTrackerWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}