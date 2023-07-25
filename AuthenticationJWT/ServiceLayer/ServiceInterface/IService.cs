using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceInterface
{
    public interface IService
    {
        Task<bool> ConfirmValidCredential(UserDTO user);
        Task<bool> RegisterNewUser(UserDTO user);
    }
}
