using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.ServiceInterface
{
    public interface IService
    {
        bool ConfirmValidCredential(UserDTO user, out string usrName);
        bool RegisterNewUser(UserDTO user);
    }
}
