namespace CardPileAPI.Services.Security.Authentication
{

    public interface ICustomAuthenticationManager
    {


        Task<string> AuthenticateAsync(long clientID, string username, string password);

    }

}
