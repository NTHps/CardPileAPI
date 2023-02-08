namespace CardPileAPI.Services.Security.Authentication
{

    public interface ICustomAuthenticationManager
    {


        Task<string> AuthenticateAsync(string username, string password);

        IDictionary<string, string> Tokens { get; }

    }

}
