using API_Service.Models;
using API_Service.Models.Filter;
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
        public UserServiceController(IRepository repoService)
        {
            _logger = LogManager.GetLogger(typeof(UserServiceController));
            _repo = repoService;
        }
        #endregion

        #region User Service APIs
        [HttpPost]
        [Route("api/registeruser")]
        [AllowAnonymous]
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
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");
            }
            response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            return response;
        }

        /*
         need to dicard the below endpoint once the token 
         validation is implemented successfully
        */
        [HttpPost]
        [Route("api/checkuser")]
        public async Task<HttpResponseMessage> CheckCredential(User user)
        {            
            try
            {
                User usrDetails = await Task.Run(()=> _repo.CheckCredential(user));
                if (usrDetails != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, usrDetails);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid Credential");
        }
        
        [HttpPost]
        [Route("api/generatetoken")]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> GetTokenForValidation(User user)
        {            
            try
            {
                TokenResponse tokenResponse = await Task.Run(() => _repo.GetTokenForValidation(user));
                if (!string.IsNullOrEmpty(tokenResponse.Token))
                {                    
                    return Request.CreateResponse(HttpStatusCode.OK, tokenResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Some error occurred!");
            }
            return Request.CreateResponse(HttpStatusCode.Unauthorized, "not an user! couldn't generate token.");
        }

        [HttpGet]
        [Route("api/getusers")]
        [JwtAuthentication]
        public async Task<HttpResponseMessage> GetAllUsers()
        {
            try
            {
                List<User> userList = await Task.Run(() => _repo.GetAllUsers().ToList<User>());
                if (userList.Count != 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, userList);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Some error occurred, please try again later!");
            }
            return Request.CreateResponse(HttpStatusCode.NoContent, "No content for display!");
        }

        #endregion
    }
}
