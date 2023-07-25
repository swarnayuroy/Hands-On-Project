using API_Service.Models;
using API_Service.Models.Data;
using API_Service.RepositoryLayer.RepoInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Service.RepositoryLayer.Repository
{
    public class DataRepository : IRepository
    {
        public User CheckCredential(User user)
        {
            User userDetail = null;
            try
            {
                userDetail = MockData.userList.Where(usr => usr.Email == user.Email && usr.Password == user.Password).FirstOrDefault<User>();
            }
            catch (Exception)
            {
                throw;
            }
            return userDetail;
        }

        public IEnumerable<User> GetAllUsers()
        {
            List<User> userList = null;
            try
            {
                userList = MockData.userList.ToList<User>();
            }
            catch (Exception)
            {
                throw;
            }
            return userList;
        }

        public bool RegisterUser(User user)
        {
            bool status = false;
            try
            {
                user.Id = Guid.NewGuid();
                user.RegisteredTime = DateTime.Now;
                MockData.userList.Add(user);
                status = true;
            }
            catch (Exception)
            {
                throw;
            }
            return status;
        }
    }
}