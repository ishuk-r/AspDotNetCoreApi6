using AspDotNetCoreApi6.Enums;
using AspDotNetCoreApi6.Models;
using AspDotNetCoreApi6.Models.ViewModels;

namespace AspDotNetCoreApi6.Services.Interface
{
    public interface IUser
    {
        Task<Status> RegisterUser(UserModel user);
        Task<Status> Login(Login loginModel);
    }
}
