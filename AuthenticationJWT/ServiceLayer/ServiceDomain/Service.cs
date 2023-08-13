using AutoMapper;
using DataAccessLayer.DataLayer.Entity;
using DataAccessLayer.DataLayerInterface;
using ServiceLayer.DTO;
using ServiceLayer.ServiceInterface;
using ServiceLayer.StartUp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceDomain
{
    public class Service : IService
    {
        #region Declaration and Initialization
        private readonly IDataLayer _dataLayer;
        private readonly IMap _mapper;
        public Service()
        {
            _dataLayer = new Config().DataLayerService;
            _mapper = new Config().MapperService;
        }
        #endregion

        #region Services
        public async Task<string> ConfirmValidCredential(UserDTO userDTO)
        {
            string token = null;
            try
            {
                User user = _mapper.GetUserEntity(userDTO);
                token = await Task.Run(() => _dataLayer.IsValidCredential(user));
            }
            catch (Exception)
            {
                throw;
            }
            return token;
        }

        public async Task<UserDetailsDTO> GetUserDetail(string token, Guid userId)
        {
            UserDetailsDTO userDetail = null;
            try
            {
                User user = await Task.Run(() => _dataLayer.GetUserDetail(token, userId));
                if (user != null)
                {
                    userDetail = _mapper.GetUserDetail(user);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return userDetail;
        }

        public async Task<bool> RegisterNewUser(UserDTO userDTO)
        {
            bool status = false;
            try
            {
                User user = _mapper.GetUserEntity(userDTO);
                status = await Task.Run(()=> _dataLayer.RegisterUser(user));
                if (status)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return status;
        }
        #endregion
    }
}
