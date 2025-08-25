using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderService.API.Controllers;
using OrderService.Business.Interfaces;
using OrderService.Domain.Model;
using OrderService.Domain.Model.Auth;
using System.Threading.Tasks;
using Xunit;

namespace OrderService.Tests
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _controller = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public async Task Login_ReturnsOk_WhenCredentialsAreCorrect()
        {

            var loginRequest = new LoginRequest { Username = "user", Password = "pass" };
            var loginResponse = new LoginResponse { Username = "user", Role = "Admin", Token = "xyz" };

            _authServiceMock.Setup(s => s.Authenticate(loginRequest))
                .ReturnsAsync(loginResponse);


            var result = await _controller.Login(loginRequest);


            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedResponse = Assert.IsType<LoginResponse>(okResult.Value);
            Assert.Equal("user", returnedResponse.Username);
        }

        [Fact]
        public async Task Register_ReturnsBadRequest_WhenUserAlreadyExists()
        {
           
            var registerRequest = new RegisterRequest
            {
                Username = "existinguser",
                Password = "pass",
                Role = "User"
            };

            _authServiceMock.Setup(s => s.Register(registerRequest))
                .ThrowsAsync(new ArgumentException("Username already exists"));

 
            var result = await _controller.Register(registerRequest);

           
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Username already exists", badRequest.Value.ToString());
        }
    }
}
