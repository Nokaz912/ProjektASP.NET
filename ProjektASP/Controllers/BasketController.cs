using Microsoft.AspNet.Identity;

using Newtonsoft.Json;
using ProjektASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjektASP.Controllers
{
    public class BasketController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Basket
        public ActionResult BasketView()
        {
            List<int> products = new List<int>();
            List<Product> products2 = new List<Product>();
            if (Request.Cookies["a"] != null)
            {
                //ViewBag.Message = Request.Cookies["a"].Value;


                string xd = Request.Cookies["a"].Value;
                var idlist = xd.Split(' ');
                for (int i = 0; i <= idlist.Length - 1; i++)
                {
                    int id1 = Convert.ToInt32(idlist[i]);
                    products.Add(id1);
                }
                foreach (var id2 in products)
                {
                    Product prod = db.Products.Find(id2);
                    Product prod3 = new Product { Id = prod.Id, Name = prod.Name, CategoryId = prod.CategoryId, Price = prod.Price, Images = prod.Images, AttachedFiles = prod.AttachedFiles };
                    products2.Add(prod3);
                }
            }
            else
            {
                ViewBag.Message = "Ciasteczko [a] nie istnieje";
                return View(products2); 
            }


            return View(products2);
        }

        [HttpPost]
        public ActionResult BasketView(int? id)
        {
            HttpCookie cookie = Request.Cookies["a"];
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);
            List<Product> products2 = new List<Product>();
            return View(products2);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        /*[HttpPost]
        public ActionResult Details(int? id, int? pusty)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var productid = Convert.ToString(product.Id);

            return View(product);
        }*/

        public ActionResult Deleted(int? id)
        {
            List<int> products = new List<int>();
            List<Product> products2 = new List<Product>();
            if (Request.Cookies["a"] != null)
            { 

                string xd = Request.Cookies["a"].Value;
                var idlist = xd.Split(' ');
                for (int i = 0; i <= idlist.Length - 1; i++)
                {
                    int id1 = Convert.ToInt32(idlist[i]);
                    products.Add(id1);
                }
                foreach (var id2 in products)
                {
                    Product prod = db.Products.Find(id2);
                    Product prod3 = new Product { Id = prod.Id, Name = prod.Name, CategoryId = prod.CategoryId, Price = prod.Price, Images = prod.Images, AttachedFiles = prod.AttachedFiles };
                    products2.Add(prod3);
                }
            }
            else
            {
                ViewBag.Message = "Ciasteczko [a] nie istnieje";
                return View(products2); ;
            }
            var productToRemove = products2.FirstOrDefault(p => p.Id == id.Value);
            if (productToRemove != null)
            {
                products2.Remove(productToRemove);
            }
            return RedirectToAction("BasketView");
        }
        public ActionResult ClearCart()
        {
            HttpCookie cookie = new HttpCookie("a");
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);

            List<Product> products2 = new List<Product>();
            return View("BasketView", products2);
        }
        [HttpPost]
        public ActionResult Order(string Payment1, string Shipping1)
        {
            List<int> products = new List<int>();
            List<Product> products2 = new List<Product>();
            if (Request.Cookies["a"] != null)
            {
                //ViewBag.Message = Request.Cookies["a"].Value;


                string xd = Request.Cookies["a"].Value;
                var idlist = xd.Split(' ');
                for (int i = 0; i <= idlist.Length - 1; i++)
                {
                    int id1 = Convert.ToInt32(idlist[i]);
                    products.Add(id1);
                }
                foreach (var id2 in products)
                {
                    Product prod = db.Products.Find(id2);
                    Product prod3 = new Product { Id = prod.Id, Name = prod.Name, CategoryId = prod.CategoryId, Price = prod.Price, Images = prod.Images, AttachedFiles = prod.AttachedFiles };
                    products2.Add(prod3);
                }
            }

            if (products.Count < 1)
            {
                return RedirectToAction("Index", "Home");
            }

            Order newOrder = new Order
            {
                UserId = User.Identity.GetUserId(), // Przyjmuję, że używasz ASP.NET Identity
                OrderDate = DateTime.Now,
                TotalAmount = (decimal)products2.Sum(p => p.Price),
                State = "W trakcie realizacji", 
                Payment = Payment1,
                Shipping = Shipping1

            };
            db.Orders.Add(newOrder);
            db.SaveChanges();
            foreach (var product in products2)
            {
                OrdersProducts ordersProducts = new OrdersProducts{ProductId= product.Id, OrderId = newOrder.OrderId };
                db.OrdersProducts.Add(ordersProducts);
                db.SaveChanges();
            }
            
            
            return View();
        }
    }
}