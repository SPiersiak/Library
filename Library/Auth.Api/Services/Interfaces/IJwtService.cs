using Auth.Api.Commands;
using Auth.Api.Models;

namespace Auth.Api.Services.Interfaces;

public interface IJwtService
{
    AuthenticationToken? GenerateAuthToken(SignInCommand user);
}
