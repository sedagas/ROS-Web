using Dapper;
using ROS.Infrastructure;
using ROS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ROS.Controllers
{
    [Authorize]
    public class DebtorController : Controller
    {
        // GET: Debtor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Debtors()
        {
            List<Addition> list;
            using(SqlConnection cn = DAL.getCn())
            {
                cn.Open();
                list = cn.Query<Addition>("Select * from Addition Where Status = '1' AND Uid='" + getId() + "'").ToList();

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