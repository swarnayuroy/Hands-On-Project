using API_Service.App_Start;
using API_Service.Models;
using API_Service.Models.Data;
using API_Service.RepositoryLayer.RepoInterface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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

        public TokenResponse GetTokenForValidation(User user)
        {
            TokenResponse response = new TokenResponse { Token = string.Empty };
            try
            {
                User validUser = new DataRepository().CheckCredential(user);
                if (validUser != null)
                {
                    response.Token = JwtManager.GenerateToken(validUser);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
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