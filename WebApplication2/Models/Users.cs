using System;
using System.Collections.Generic;

namespace WebApplication2
{
    public partial class Users
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Surname { get; set; }
    }
}
