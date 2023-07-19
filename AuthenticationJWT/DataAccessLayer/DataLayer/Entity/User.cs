using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DataLayer.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        private DateTime DateOfBirth { get; set; }
        private string ContactNo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public DateTime RegisteredTime { get; set; }
    }
}
