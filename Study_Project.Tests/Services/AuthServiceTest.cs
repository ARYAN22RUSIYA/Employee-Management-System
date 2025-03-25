using Xunit;
using Moq;
using System.Threading.Tasks;
using Study_Project.Models;
using Study_Project.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace Study_Project.Tests
{
    public class AuthServiceTest
    {
        private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
        private readonly Mock<RoleManager<IdentityRole>> _roleManagerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly AuthService _authService;

        public AuthServiceTest()
        {
            // Mocks for UserManager and RoleManager
            var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            _userManagerMock = new Mock<UserManager<IdentityUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            var roleStoreMock = new Mock<IRoleStore<IdentityRole>>();
            _roleManagerMock = new Mock<RoleManager<IdentityRole>>(roleStoreMock.Object, null, null, null, null);

            _configurationMock = new Mock<IConfiguration>();

            _authService = new AuthService(_userManagerMock.Object, _roleManagerMock.Object, _configurationMock.Object);
        }

        [Fact]
        public async Task LoginAsync_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var loginModel = new Login { Username = "testuser", Password = "Password123" };
            var user = new IdentityUser { UserName = loginModel.Username };

            _userManagerMock.Setup(um => um.FindByNameAsync(loginModel.Username))
                .ReturnsAsync(user);

            _userManagerMock.Setup(um => um.CheckPasswordAsync(user, loginModel.Password))
                .ReturnsAsync(true);

            _userManagerMock.Setup(um => um.GetRolesAsync(user))
                .ReturnsAsync(new List<string> { "User" });

            // Mock JWT settings from IConfiguration
            _configurationMock.Setup(c => c["Jwt:Issuer"]).Returns("TestIssuer");
            _configurationMock.Setup(c => c["Jwt:Audience"]).Returns("TestAudience");
            _configurationMock.Setup(c => c["Jwt:ExpiryMinutes"]).Returns("60");
            _configurationMock.Setup(c => c["Jwt:Key"]).Returns("VerySecretKeyForTestingPurpose123!");

            // Act
            var result = await _authService.LoginAsync(loginModel);

            // Assert
            Assert.NotNull(result);
            Assert.Contains("token", result.ToString());
        }
    }
}
