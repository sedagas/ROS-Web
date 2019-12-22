using Dapper;
using ROS.Infrastructure;
using ROS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace ROS.Controllers
{
    [Authorize]
    public class AdditionController : Controller
    {

        // GET: Addition
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Addition()
        {
            List<Addition> list;
            using(SqlConnection cn = DAL.getCn())
            {
                cn.Open();
                list = cn.Query<Addition>("Select * from Addition Where State = '0' and Uid='" + getId() + "'").ToList();
                cn.Close();
            }
            return View(list);
        }

        public int getId()
        {
            int id;
            using (SqlConnection cn = DAL.getCn())
            {
                cn.Open();

                User uId = cn.Query<User>("Select * From Users Where Username = '" + User.Identity.Name + "'").FirstOrDefault();
                id = uId.Id;
                cn.Close();
            }
            return id;
        }
    }
}