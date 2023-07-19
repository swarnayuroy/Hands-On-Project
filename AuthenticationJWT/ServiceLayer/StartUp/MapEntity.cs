using AutoMapper;
using DataAccessLayer.DataLayer.Entity;
using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.StartUp
{
    public class MapEntity
    {
        public User GetUserEntity(UserDTO userDTO)
        {
            Mapper.CreateMap<UserDTO, User>();
            User user = Mapper.Map<UserDTO, User>(userDTO);
            return user;
        }
    }
}
