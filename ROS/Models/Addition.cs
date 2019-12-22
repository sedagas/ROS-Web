using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ROS.Models
{
    public enum State
    {
        Closed,
        Open
    }

    public enum Status
    {
        Payed,
        NotPayed
    }

    public class Addition
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public State  State { get; set; }
        public string CustomerName { get; set; }
        public string TableName { get; set; }
        public string Status { get; set; }
        List<AdditionDetails> Details;
    }
}