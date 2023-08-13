using DataAccessLayer.DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataLayerInterface
{
    public interface IDataLayer
    {
        Task<string> IsValidCredential(User user);
        Task<bool> RegisterUser(User cred);
        Task<User> GetUserDetail(string token, Guid userId);
    }
}
