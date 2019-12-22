using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ROS.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RestaurantName { get; set; }
        public string Username { get; set; }
        public string Pass { get; set; }
    }
}