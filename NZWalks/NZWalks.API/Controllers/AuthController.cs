using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            TokenHandler = tokenHandler;
        }

        public ITokenHandler TokenHandler { get; }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
        {
            //Valadate the incoming request (make this later)


            // Check if user is authenticated
            //Check username and password
            var user = await userRepository.AuthenticateAsync(
                loginRequest.Username, loginRequest.Password);

            if (user != null)
            {
                // Generate a JWT Token
                var token = await TokenHandler.CreateTokenAsync(user);
                return Ok(token);
                   
            }
            return BadRequest("Username or Password is incorrect.");

        }
    }
}
