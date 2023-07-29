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
        #region Declaration and Initialization
        private readonly string _signingKey;
        public DataRepository()
        {
            _signingKey = "4Nmw1zotLoYdFJpJPR2p21hyahPB2qQIDpY8lKpp+6I=";
        }
        #endregion
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
            TokenResponse response = null;
            try
            {
                User validUser = new DataRepository().CheckCredential(user);
                if (validUser != null)
                {
                    //generating a token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signingKey));
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, validUser.Id.ToString()),
                            new Claim(ClaimTypes.Name, validUser.Name),
                            new Claim(ClaimTypes.Email, validUser.Email)
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(5),
                        SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
                    };

                    response.Token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
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