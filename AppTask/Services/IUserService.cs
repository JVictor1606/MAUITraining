using AppTask.Models;

namespace AppTask.Services
{
    public interface IUserService
    {
        Task<UserModel> GetAcessToken(string email);
        Task<UserModel> ValidateAcessToken(UserModel userModel);
    }
}