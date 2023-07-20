using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.ServiceInterface
{
    public interface IService
    {
        bool ConfirmValidCredential(UserDTO user);
        bool RegisterNewUser(UserDTO user);
    }
}
