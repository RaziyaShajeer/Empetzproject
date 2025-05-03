using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Empetz_API.API.Public.RequestObject;
using Empetz_APITests.Fixtures;

namespace Empetz_APITests.Controller
{
    public class PublicContrllerTest
    {
        protected readonly HttpClient _httpClient;

        public PublicContrllerTest()
        {
			//ApiWebApplicationFactory _factory = new ApiWebApplicationFactory();
			//_httpClient = _factory.CreateClient();
			_httpClient = new HttpClient();
			_httpClient.BaseAddress = new Uri("https://localhost:7216/");
		}
        [Fact]
        public async Task POST_Register_user_without_phone_Results_BadRequest()
        {
            //Arrange
            UserSignUp user = new UserSignUp();
            user.FirstName = "Test";
            user.Phone = "";
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            //Act
            var response = await _httpClient.PostAsync("api/v1/user", httpContent);
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }
    }
}


