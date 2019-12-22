using Dapper;
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
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection cn = DAL.getCn())
                {

                    cn.Open();
                    User newUser = cn.Query<User>("Select * From Users Where username='" + user.Username + "' and pass='" + user.Pass + "'").FirstOrDefault();
                    if (newUser != null)
                    {
                        FormsAuthentication.SetAuthCookie(user.Username, true);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToRoute(new { controller = "Auth", action = "Login" });
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid email or password");
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}