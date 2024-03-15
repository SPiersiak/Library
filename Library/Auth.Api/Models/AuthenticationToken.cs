namespace Auth.Api.Models;
public class AuthenticationToken
{
    public string Token { get; set; }
    public int ExpiresIn { get; set; }
}
