using Dapper;
using DapperExtensions;
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
    public class TableController : Controller
    {
        // GET: Table
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tables()
        {
            List<Tables> tblList = new List<Tables>();
            using (SqlConnection cn = DAL.getCn())
            {
                cn.Open();
                tblList = cn.Query<Tables>("Select * from Tables Where Uid='" + getId() + "'").ToList();
                cn.Close();
            }
            return View(tblList);
        }

        public ActionResult AddTable()
        {
            var typeList = Enum.GetValues(typeof(Group))
               .Cast<Category>()
               .Select(t => new AccessData
               {
                   Id = ((int)t),
                   Name = t.ToString()
               });
            ViewBag.ListData = typeList;
            return View();
        }
        
        [HttpPost]
        public ActionResult AddTable(Tables tbl)
        {
            List<Tables> tblList;
            using (SqlConnection cn = DAL.getCn())
            {
                cn.Open();
                tbl.Uid = getId();
                cn.Insert(tbl);
                tblList = cn.Query<Tables>("Select * From Tables Where Uid='" + getId() + "'").ToList();
                cn.Close();
            }
            return RedirectToAction("Tables", "Table", tblList);

        }

        public ActionResult EditTable(int? id)
        {
            Tables table;
            try { 
            using (SqlConnection cn = DAL.getCn())
            {
                cn.Open();
                table = cn.Get<Tables>(id);
                cn.Close();
            }
            return View(table);
            }catch(Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Table", "EditTable"));
            }
        }

        [HttpPost]
        public ActionResult EditTable(Tables tbl)
        {
            Tables table;
            List<Tables> tblList;
            using (SqlConnection cn = DAL.getCn())
            {
                cn.Open();
                table = cn.Get<Tables>(tbl.Id);
                table.Name = tbl.Name;
                table.Groups = tbl.Groups;
                table.Count = tbl.Count;
                cn.Update<Tables>(table);
                tblList = cn.Query<Tables>("Select * From Tables Where Uid='" + getId() + "'").ToList();
                cn.Close();
            }
            return RedirectToAction("Tables", "Table", tblList);
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

        public ActionResult Delete(int? id)
        {
            Tables table;
            using (SqlConnection cn = DAL.getCn())
            {
                table = cn.Get<Tables>(id);
            }
            return View(table);
        }


        public ActionResult DeleteTables(int? id)
        {
            Tables table;
            List<Tables> tblList;
            try
            {
                using (SqlConnection cn = DAL.getCn())
                {
                    cn.Open();
                    table = cn.Get<Tables>(id);
                    cn.Delete(table);
                    tblList = cn.Query<Tables>("Select * from Tables").ToList();
                    cn.Close();
                    return RedirectToAction("Tables", "Table", tblList);
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Product", "DeleteProduct"));
            }
        }
    }
}