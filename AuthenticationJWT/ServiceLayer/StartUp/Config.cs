using DataAccessLayer.DataLayer;
using DataAccessLayer.DataLayerInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.StartUp
{
    public class Config
    {
        private readonly IDataLayer _dataLayerConfig;
        public Config()
        {
            _dataLayerConfig = new DataAccessDomain();            
        }
        public IDataLayer DataLayerService { 
            get { return _dataLayerConfig; }
        }
    }
}
