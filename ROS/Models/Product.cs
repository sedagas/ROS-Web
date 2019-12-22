using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ROS.Models
{
    public enum Category
    {
        Salad,
        Meat,
        Burger,
        Starter,
        Desert,
        SoftDrink,
        AlcoholicDrink
    }

    public class AccessData
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public enum Amount
    {
        Portion,
        Kilo
    }
    public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Category Category { get; set; }
            public string Price { get; set; }
            public string Amount { get; set; }
            public int Uid { get; set; }

        public List<Product> Prd = new List<Product>();
    }
}