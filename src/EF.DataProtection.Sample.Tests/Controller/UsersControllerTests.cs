using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EF.DataProtection.Sample.Controllers;
using EF.DataProtection.Sample.Tests.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Xunit;

namespace EF.DataProtection.Sample.Tests.Controller
{
    public class UsersControllerTests: IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public UsersControllerTests(WebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Should_CreateUser_WithoutError()
        {
            var user = new UserCreateModel
            {
                Email = "email@email.com",
                PhoneNumber = "400000000",
                SensitiveData = "Some sensitive data"
            };

            var response = await _httpClient
                .PostAsync("api/users",
                    new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            var userId = await response.Content
                .ReadAsStringAsync();

            var usersResponse = await _httpClient
                .GetAsync($"api/users/{userId}");
            
            usersResponse.EnsureSuccessStatusCode();
            
            var userFromDatabase = await usersResponse.Content.ReadAsAsync<UserDto>();

            Assert.NotNull(userFromDatabase);
            Assert.Equal(user.Email, userFromDatabase.Email);
            Assert.Equal(user.SensitiveData, userFromDatabase.SensitiveData);
            Assert.Equal(user.PhoneNumber, userFromDatabase.PhoneNumber);
        }
        
        [Fact]
        public async Task Should_Encrypt_SensitiveFields()
        {
            var user = new UserCreateModel
            {
                Email = "email@email.com",
                PhoneNumber = "400000000",
                SensitiveData = "Some sensitive data"
            };

            var response = await _httpClient
                .PostAsync("api/users",
                    new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            var userId = await response.Content
                .ReadAsStringAsync();

            var usersResponse = await _httpClient
                .GetAsync($"api/users/{userId}/protected");
            
            usersResponse.EnsureSuccessStatusCode();

            var userFromDatabase = await usersResponse.Content.ReadAsAsync<UserDto>();

            Assert.NotNull(userFromDatabase);
            Assert.NotEqual(user.Email, userFromDatabase.Email);
            Assert.NotEqual(user.SensitiveData, userFromDatabase.SensitiveData);
            Assert.NotEqual(user.PhoneNumber, userFromDatabase.PhoneNumber);
        }
    }

    public class UserDto
    {
        public long Id { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
        
        public string SensitiveData { get; set; }
        
        public string PhoneNumberHash { get; protected set; }
    }
}