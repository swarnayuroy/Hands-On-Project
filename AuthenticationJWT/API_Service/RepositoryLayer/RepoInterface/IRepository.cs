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
        IEnumerable<User> GetAllUsers();
        User CheckCredential(User user);
        TokenResponse GetTokenForValidation(User user);
        bool RegisterUser(User user);
    }
}
