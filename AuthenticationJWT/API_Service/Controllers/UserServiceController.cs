using API_Service.App_Start;
using API_Service.Models;
using API_Service.RepositoryLayer.RepoInterface;
using API_Service.RepositoryLayer.Repository;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            _repo = new ServiceConfig().RepoService;
        }
        #endregion

        #region User Service APIs
        [HttpGet]
        [Route("api/getusers")]
        public IEnumerable<User> GetAllUsers()
        {
            List<User> userList = null;
            try
            {
                userList = _repo.GetAllUsers().ToList<User>();                
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message.ToString());
            }
            return userList;
        }
        #endregion
    }
}
