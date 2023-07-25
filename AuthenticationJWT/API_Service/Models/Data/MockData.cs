using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Service.Models.Data
{
    public class MockData
    {
        public static List<User> userList = new List<User>()
        {
            new User
            {
                Name="John Doe",
                Email="doe.john@gmail.com",
                Password = "TestJohn@123"
            },
            new User
            {
                Name="Jane Doe",
                Email="doe.jane@gmail.com",
                Password = "TestJane@321"
            },
            new User
            {
                Name="Jack Dawson",
                Email="dawson.jack88@gmail.com",
                Password = "TestJack@1986"
            }
        };
    }
}