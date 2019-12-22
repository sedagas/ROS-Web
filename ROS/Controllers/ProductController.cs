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
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products()
        {
            List<Product> prdList = new List<Product>();
            using (SqlConnection cn = DAL.getCn())
            {
                cn.Open();
                prdList = cn.Query<Product>("Select * From Product Where Uid='"+ getId() + "'").ToList();
                cn.Close();
            }
            return View(prdList);
        }

        public ActionResult AddProduct()
        {
            var typeList = Enum.GetValues(typeof(Category))
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
        public ActionResult AddProduct(Product prd)
        {
            List<Product> prdList;
            using (SqlConnection cn = DAL.getCn())
            {
                cn.Open();
                //User uId = cn.Query<User>("Select * From Users Where Username = '" + User.Identity.Name + "'").FirstOrDefault();
                prd.Uid = getId();
                int id = cn.Insert(prd);
                prdList = cn.Query<Product>("Select * From Product Where Uid='"+getId()+"'").ToList();
                cn.Close();

                return RedirectToAction("Products", "Product", prdList);
            }
        }

        public ActionResult Edit(int? id)
        {
            using (SqlConnection cn = DAL.getCn())
            {
                cn.Open();
                Product product = cn.Get<Product>(id);
                cn.Close();
                return View(product);

            }
        }

        [HttpPost]
        public ActionResult Edit(Product prd)
        {
            List<Product> prdList;
            try
            {
                using (SqlConnection cn = DAL.getCn())
                {
                    cn.Open();
                    Product product = cn.Get<Product>(prd.Id);
                    product.Name = prd.Name;
                    product.Category = prd.Category;
                    product.Price = prd.Price;
                    product.Amount = prd.Amount;
                    cn.Update(product);
                    prdList = cn.Query<Product>("Select * From Product").ToList();
                    cn.Close();
                }
              return RedirectToAction("Products", "Product", prdList);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Product", "Edit"));
            }
        }


        public ActionResult Delete(int? id)
        {
            Product product;
            using (SqlConnection cn = DAL.getCn())
            {
                product = cn.Get<Product>(id);
            }
            return View(product);
        }

        public ActionResult DeleteProduct(int? id)
        {
            Product product;
            List<Product> prdList;
            try
            { 
                using (SqlConnection cn = DAL.getCn())
                {
                    cn.Open();
                    product = cn.Get<Product>(id);
                    cn.Delete(product);
                    prdList = cn.Query<Product>("Select * from Product").ToList();
                    cn.Close();
                    return RedirectToAction("Products", "Product", prdList);
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Product", "DeleteProduct"));
            }
        }

        public int getId()
        {
            int id;
            using(SqlConnection cn = DAL.getCn())
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