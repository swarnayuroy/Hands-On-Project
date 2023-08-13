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
    [JwtAuthentication]
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
        [HttpGet]
        [Route("api/getusers")]        
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

        [HttpGet]
        [Route("api/user/{id}")]
        public async Task<HttpResponseMessage> GetUserById(string id)
        {
            try
            {
                User userDetail = await Task.Run(()=>_repo.GetUserById(id));
                if (userDetail != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, userDetail);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Some error occurred, please try again later!");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "User not found!");
        }
        #endregion
    }
}
