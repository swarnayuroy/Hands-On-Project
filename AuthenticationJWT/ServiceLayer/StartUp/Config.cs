using DataAccessLayer.DataLayer;
using DataAccessLayer.DataLayerInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.StartUp
{
    public class Config
    {
        private readonly IDataLayer dataLayerConfig;
        public Config()
        {
            dataLayerConfig = new DataAccessDomain();
        }
        public IDataLayer DataLayerConfig { get { return dataLayerConfig; } }
    }
}
