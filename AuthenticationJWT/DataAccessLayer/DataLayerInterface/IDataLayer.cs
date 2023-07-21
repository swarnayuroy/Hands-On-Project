using DataAccessLayer.DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DataLayerInterface
{
    public interface IDataLayer
    {
        bool IsValidCredential(User user, out string usrName);
        bool RegisterUser(User user);
    }
}
