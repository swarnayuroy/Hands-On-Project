using DataAccessLayer.DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DataLayer.Data
{
    //This class will be removed once API is set in this project
    public class MockData
    {
        public static List<User> userList = new List<User>()
        {
            new User
            {
                Id = Guid.NewGuid(),
                Name="John Doe",
                Email="doe.john@gmail.com",
                Password = "TestJohn@123"
            },
            new User
            {
                Id = Guid.NewGuid(),
                Name="Jane Doe",
                Email="doe.jane@gmail.com",
                Password = "TestJane@321"
            },
            new User
            {
                Id = Guid.NewGuid(),
                Name="Jack Dawson",
                Email="dawson.jack88@gmail.com",
                Password = "TestJack@1986"
            }
        };
    }
}
