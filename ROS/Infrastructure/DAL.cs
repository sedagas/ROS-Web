using Dapper;
using ROS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ROS.Infrastructure
{
    public class DAL
    {
        public static SqlConnection getCn()
        {
            var cn = new SqlConnection();
            cn.ConnectionString = "data source=DESKTOP-PLD1ULM\\SQLEXPRESS; database=ROS; integrated security = SSPI;";
            return cn;
        }
        
    }
}