using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ROS.Models
{
    public enum Group
    {
        Graden,
        Terace,
        Salon
    }
  
    public class Tables
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public Group Groups { get; set; }
        public int Count { get; set; }
        public int Uid { get; set; }

        public List<Tables> tbl = new List<Tables>();
    }
}