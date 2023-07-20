using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserDetailsDTO : UserDTO
    {
        public string Gender { get; set; }
        private DateTime DateOfBirth { get; set; }
        private string ContactNo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public DateTime RegisteredTime { get; set; }
    }
}
