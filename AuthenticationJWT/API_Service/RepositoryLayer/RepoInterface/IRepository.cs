using API_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Service.RepositoryLayer.RepoInterface
{
    public interface IRepository
    {
        Task<IList<User>> GetAllUsers();
        Task<User> CheckCredential(User user);
        Task<User> GetUserById(string userId);
        Task<TokenResponse> GetTokenForValidation(User user);
        Task<bool> RegisterUser(User user);
        Task<bool> EditUserDetails(User userDetails);
        Task<bool> DeleteUser(string id);
        Task<List<string>> GetEmailList();
    }
}
