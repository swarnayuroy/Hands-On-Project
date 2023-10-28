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
using System.Threading.Tasks;
using System.Web;

namespace API_Service.RepositoryLayer.Repository
{
    public class DataRepository : IRepository
    {
        public async Task<User> CheckCredential(User user)
        {
            User userDetail = null;
            try
            {
                userDetail = await Task.Run(()=>MockData.userList.Where(usr => usr.Email == user.Email && usr.Password == user.Password).FirstOrDefault<User>());
            }
            catch (Exception)
            {
                throw;
            }
            return userDetail;
        }
        public async Task<IList<User>> GetAllUsers()
        {
            List<User> userList = null;
            try
            {
                userList = await Task.Run(()=>MockData.userList.ToList<User>());
            }
            catch (Exception)
            {
                throw;
            }
            return userList;
        }
        public async Task<TokenResponse> GetTokenForValidation(User user)
        {
            TokenResponse response = new TokenResponse { Token = string.Empty };
            try
            {
                User validUser = await Task.Run(()=>new DataRepository().CheckCredential(user));
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
        public async Task<User> GetUserById(string userId)
        {
            User userDetail = new User();
            try
            {
                userDetail = await Task.Run(() => MockData.userList.Where(usr => usr.Id == Guid.Parse(userId)).FirstOrDefault<User>());
            }
            catch (Exception)
            {
                throw;
            }
            return userDetail;
        }
        public async Task<List<string>> GetEmailList()
        {
            List<string> emailList = new List<string>();
            try
            {
                emailList = await Task.Run(()=>MockData.userList.Select(usr => usr.Email).ToList<string>());
            }
            catch (Exception)
            {
                throw;
            }
            return emailList;
        }
        public async Task<bool> RegisterUser(User user)
        {
            bool status = false;
            try
            {
                user.Id = Guid.NewGuid();           
                user.RegisteredTime = DateTime.Now;
                await Task.Run(()=>MockData.userList.Add(user));
                status = true;
            }
            catch (Exception)
            {
                throw;
            }
            return status;
        }
        public async Task<bool> EditUserDetails(User userDetails)
        {
            bool status = false;
            try
            {
                var userList = await Task.Run(() => MockData.userList);
                foreach (User user in userList)
                {
                    if (user.Id == userDetails.Id)
                    {
                        if (string.IsNullOrEmpty(userDetails.Password) && string.IsNullOrEmpty(userDetails.Email))
                        {
                            //Saving user entered details
                            user.Gender = userDetails.Gender;
                            user.DateOfBirth = userDetails.DateOfBirth;
                            user.ContactNo = userDetails.ContactNo;
                            user.State = userDetails.State;
                            user.City = userDetails.City;
                            user.Zip = userDetails.Zip;
                        }
                        else
                        {
                            //Saving a new password
                            user.Password = userDetails.Password;
                        }                        
                        break;
                    }
                }
                MockData.userList = userList;
                status = true;
            }
            catch (Exception)
            {
                throw;
            }
            return status;
        }
        public async Task<bool> DeleteUser(string id)
        {
            bool status = false;
            try
            {
                User user = await GetUserById(id);
                MockData.userList.Remove(user);
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