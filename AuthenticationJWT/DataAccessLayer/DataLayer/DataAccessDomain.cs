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
        public bool RegisteredUser(User user)
        {
            try
            {
                user.Id = Guid.NewGuid();
                MockData.userList.Add(user);
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        public bool IsValidCredential(User user)
        {
            var anyUser = 0;
            try
            {
                anyUser = MockData.userList.Where(usr => usr.Email == user.Email && usr.Password == user.Password).Count();
                if (anyUser == 1)
                {
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
