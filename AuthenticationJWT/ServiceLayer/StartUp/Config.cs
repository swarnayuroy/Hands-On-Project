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
        private readonly IMap _mapper;
        public Config()
        {
            _dataLayerConfig = new DataAccessDomain();
            _mapper = new MapEntity();
        }
        public IDataLayer DataLayerService { 
            get { return _dataLayerConfig; }
        }
        public IMap MapperService { 
            get { return _mapper; } 
        }
    }
}
