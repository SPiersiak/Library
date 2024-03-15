using Auth.Api.Commands;

namespace Auth.Api.Services.Interfaces;

public interface IUserService
{
    bool SignIn(SignInCommand model);
}
