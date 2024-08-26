using AppTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AppTask.Services
{
    public class UserService : IUserService
    {
        private HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }

        public async Task<UserModel> GetAcessToken(string email)
        {
            return await _http.GetFromJsonAsync<UserModel>($"users?email{email}");
        }

        public async Task<UserModel> ValidateAcessToken(UserModel userModel)
        {
            var msg = await _http.PostAsJsonAsync<UserModel>($"users", userModel);
            if (msg.IsSuccessStatusCode)
            {
                return await msg.Content.ReadFromJsonAsync<UserModel>();
            }
            return null;
        }
    }
}
