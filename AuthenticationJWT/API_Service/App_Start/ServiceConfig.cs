using API_Service.RepositoryLayer.RepoInterface;
using API_Service.RepositoryLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Service.App_Start
{
    public class ServiceConfig
    {
        private IRepository _repo;
        public ServiceConfig()
        {
            _repo = new DataRepository();
        }
        public IRepository RepoService
        {
            get { return _repo; }
        }
    }
}