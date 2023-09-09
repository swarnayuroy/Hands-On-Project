using DataAccessLayer.DataLayer.Entity;
using DataAccessLayer.DataLayerInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataLayer
{
    public class DataAccessDomain : IDataLayer
    {
        #region Declaration and Initialization
        private readonly Uri _baseAddress;
        private readonly HttpClient _client = new HttpClient();
        public DataAccessDomain()
        {
            _baseAddress = new Uri("https://localhost:44379/api/");
            _client.BaseAddress = _baseAddress;
        }
        #endregion

        public async Task<bool> RegisterUser(User user)
        {
            bool responseStatus = false;
            try
            {
                StringContent usrData = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await Task.Run(() =>_client.PostAsync(_client.BaseAddress + "registeruser", usrData).Result);
                if (response.IsSuccessStatusCode)
                {
                    responseStatus = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return responseStatus;
        }
        public async Task<string> IsValidCredential(User cred)
        {
            TokenResponse tokenResponse = new TokenResponse() 
            { 
                Token = "" 
            };
            try
            {
                StringContent usrCred = new StringContent(JsonConvert.SerializeObject(cred), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await Task.Run(() => _client.PostAsync(_client.BaseAddress + "generatetoken", usrCred).Result);
                if (response.IsSuccessStatusCode)
                {
                    tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(response.Content.ReadAsStringAsync().Result);
                }                
            }
            catch (Exception)
            {
                throw;
            }
            return tokenResponse.Token;
        }
        public async Task<User> GetUserDetail(string token, Guid userId, bool isOnlyCredential)
        {
            User userDetail = null;
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await Task.Run(() => _client.GetAsync($"{_client.BaseAddress}/user/{userId}").Result);
                if (response.IsSuccessStatusCode)
                {
                    userDetail = JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result);
                    if (isOnlyCredential)
                    {
                        User userCredDetail = new User()
                        {
                            Id = userDetail.Id,
                            Name = userDetail.Name,
                            Email = userDetail.Email,
                            Password = userDetail.Password
                        };
                        userDetail = userCredDetail;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return userDetail;
        }
        public async Task<bool> IsEmailExist(string email)
        {
            bool responseStatus = false;
            try
            {
                HttpResponseMessage response = await Task.Run(() => _client.GetAsync(_client.BaseAddress + "getemails").Result);
                if (response.IsSuccessStatusCode)
                {
                    IList<string> emailList = JsonConvert.DeserializeObject<IList<string>>(response.Content.ReadAsStringAsync().Result);
                    if (emailList.Contains(email))
                    {
                        responseStatus = true;
                    }
                    emailList.Clear();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return responseStatus;
        }
        public async Task<bool> EditUserDetails(string token, User user, bool savePassword)
        {
            bool responseStatus = false;
            try
            {
                if (!savePassword)
                {
                    user.Password = String.Empty;
                    user.Email = String.Empty;
                }
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                StringContent usrDetails = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await Task.Run(() => _client.PutAsync(_client.BaseAddress + "edituser", usrDetails).Result);
                if (response.IsSuccessStatusCode)
                {
                    responseStatus = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return responseStatus;
        }
    }
}
