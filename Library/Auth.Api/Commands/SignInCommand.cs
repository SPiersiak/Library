namespace Auth.Api.Commands;

public class SignInCommand
{
    public required string Name { get; set; }

    public required string Password { get; set; }
}
