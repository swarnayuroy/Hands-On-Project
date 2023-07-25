using API_Service.Models;
using API_Service.RepositoryLayer.RepoInterface;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API_Service.Controllers
{
    public class UserServiceController : ApiController
    {
        #region Declaration and Initialization
        private IRepository _repo;
        private readonly ILog _logger;
        
        public UserServiceController()
        {
            _logger = LogManager.GetLogger(typeof(UserServiceController));
        }
        public UserServiceController(IRepository repoService)
        {
            _repo = repoService;
        }
        #endregion

        #region User Service APIs
        [HttpGet]
        [Route("api/getusers")]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var userList = new List<User>();
            try
            {
                userList = await Task.Run(()=> _repo.GetAllUsers().ToList<User>());                
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }
            return userList;
        }

        [HttpPost]
        [Route("api/registeruser")]
        public async Task<HttpResponseMessage> RegisterUser(User user)
        {
            HttpResponseMessage response = null;
            try
            {
                bool status = await Task.Run(() => _repo.RegisterUser(user));
                if (status)
                {
                    response = new HttpResponseMessage(HttpStatusCode.Created);
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }
            response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            return response;
        }

        [HttpPost]
        [Route("api/checkuser")]
        public async Task<User> CheckCredential(User user)
        {
            User usrDetails = null;
            try
            {
                usrDetails = await Task.Run(()=> _repo.CheckCredential(user));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
                throw;
            }
            return usrDetails;
        }
        #endregion
    }
}
