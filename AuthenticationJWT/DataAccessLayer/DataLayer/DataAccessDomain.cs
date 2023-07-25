using DataAccessLayer.DataLayer.Data;
using DataAccessLayer.DataLayer.Entity;
using DataAccessLayer.DataLayerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.DataLayer
{
    public class DataAccessDomain : IDataLayer
    {
        public bool RegisterUser(User user)
        {
            try
            {
                user.Id = Guid.NewGuid(); //need to remove this line of code after integrating to API service
                MockData.userList.Add(user);
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        public bool IsValidCredential(User user, out string usrName)
        {
            usrName = $"";
            User userDetail = null;
            try
            {
                userDetail = MockData.userList.Where(usr => usr.Email == user.Email && usr.Password == user.Password).FirstOrDefault<User>();
                if (userDetail != null)
                {
                    usrName = userDetail.Name;
                    return true;
                }                
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }
    }
}
