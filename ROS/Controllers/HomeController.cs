using Dapper;
using DapperExtensions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ROS.Infrastructure;
using ROS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ROS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            string id = User.Identity.GetUserId();

            ViewData["id"] = id;
            bool result = User.Identity.IsAuthenticated;
            ViewData["name"] = User.Identity.Name;
            return View();
        }
        
        public ActionResult List(int ?id)
        {
            Product product;
            using (SqlConnection cn = DAL.getCn())
            {
                cn.Open();
                string ikod = User.Identity.GetUserId();
                product = cn.Get<Product>(id);
                cn.Close();
            }
            return View(product);
        }
        
    
    }
}