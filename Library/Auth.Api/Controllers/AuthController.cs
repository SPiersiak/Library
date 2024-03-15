using Auth.Api.Commands;
using Auth.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers;
[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IJwtService _jwtService;
    private readonly IUserService _userService;
    public AuthController(IJwtService jwtService, IUserService userService)
    {
        _jwtService = jwtService;
        _userService = userService;
    }
    [HttpPost]
    [AllowAnonymous]
    public IActionResult Login([FromBody] SignInCommand user)
    {
        var result = _userService.SignIn(user);
        if (result)
        {
            var loginResult = _jwtService.GenerateAuthToken(user);
            return Ok(loginResult);
        }

        return Unauthorized(false);
    }
}
