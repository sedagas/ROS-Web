using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ROS.Models
{
    public class AdditionDetails
    {
        public int Id { get; set; }
        public int ProductRef { get; set; }
        public int AdditionRef { get; set; }
        public double Amount { get; set; }
        public double UnitPrice { get; set; }
        public double Total { get; set; }
    }
}