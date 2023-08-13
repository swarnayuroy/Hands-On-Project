using AutoMapper;
using DataAccessLayer.DataLayer.Entity;
using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.StartUp
{
    public interface IMap
    {
        User GetUserEntity(UserDTO userDTO);
        UserDetailsDTO GetUserDetail(User user);
    }
    public class MapEntity : IMap
    {
        public User GetUserEntity(UserDTO userDTO)
        {
            Mapper.CreateMap<UserDTO, User>();
            User user = Mapper.Map<UserDTO, User>(userDTO);
            return user;
        }
        public UserDetailsDTO GetUserDetail(User user)
        {
            Mapper.CreateMap<User, UserDetailsDTO>();
            UserDetailsDTO userDetail = Mapper.Map<User, UserDetailsDTO>(user);
            return userDetail;
        }
    }
}
