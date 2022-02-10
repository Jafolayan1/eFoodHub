using eFoodHub.Entities;

namespace eFoodHub.Services.Interfaces
{
    public interface IAuthenticationService
    {
        bool CreateUser(User User, string Password);

        Task<bool> Signout();

        User AuthenticateUser(string UserName, string Password);

        User GetUser(string UserName);
    }
}