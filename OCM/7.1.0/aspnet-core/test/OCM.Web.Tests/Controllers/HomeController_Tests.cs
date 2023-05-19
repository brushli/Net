using System.Threading.Tasks;
using OCM.Models.TokenAuth;
using OCM.Web.Controllers;
using Shouldly;
using Xunit;

namespace OCM.Web.Tests.Controllers
{
    public class HomeController_Tests: OCMWebTestBase
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