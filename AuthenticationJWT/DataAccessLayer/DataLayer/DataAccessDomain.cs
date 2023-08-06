using DataAccessLayer.DataLayer.Entity;
using DataAccessLayer.DataLayerInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    }
}
