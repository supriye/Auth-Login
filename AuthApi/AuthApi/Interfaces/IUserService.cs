using AuthApi.Models;

namespace AuthApi.Interfaces
{
    public interface IUserService
    {
        User? ValidateUser(string userName, string password);
        void SaveUser(User user);
    }
}
