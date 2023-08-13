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
                Id=Guid.Parse("fba94e25-b7b7-424c-b81f-9d3a13af4fb1"),
                Name="John Doe",
                Email="doe.john@gmail.com",
                Password = "TestJohn@123"
            },
            new User
            {
                Id=Guid.Parse("204ab634-ade1-4b08-9cda-6f304a9ca30d"),
                Name="Jane Doe",
                Email="doe.jane@gmail.com",
                Password = "TestJane@321"
            },
            new User
            {
                Id=Guid.Parse("040ce2b9-9adc-42f7-a4d4-0bd4bdb58a7b"),
                Name="Jack Dawson",
                Email="dawson.jack88@gmail.com",
                Password = "TestJack@1986"
            }
        };
    }
}