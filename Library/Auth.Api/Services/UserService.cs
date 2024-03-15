using Auth.Api.Commands;
using Auth.Api.Models;
using Auth.Api.Services.Interfaces;

namespace Auth.Api.Services;

public class UserService : IUserService
{
    public bool SignIn(SignInCommand model)
    {
        if(model.Name.Equals(User.Username) &&  model.Password.Equals(User.Password)) { return true; } return false;
    }

    private static User User = new User() { Username = "admin", Password = "admin" };
}
