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
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            Category category = new Category();
            category.Id = 0;
            category.Name = "Brak";
            var categories = db.Categories.ToList();
            categories.Insert(0, category);
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");

            var productList = db.Products.Where(s => s.Avaliable == true ).ToList();

            return View(productList);
        }
        public ActionResult Search(string searchString)
        {
            var products = db.Products;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = (System.Data.Entity.DbSet<Product>)products.Where(p => p.Name.Contains(searchString));
            }

            return View(products.ToList());
        }

        [HttpPost]
        public ActionResult Index(Category cat)
        {
            var productList = new Object();
            if (cat.Id == 0) 
                productList = db.Products.Where(s => s.Avaliable == true).ToList();
            else 
                productList = db.Products.Where(s => s.Category.Id == cat.Id).Where(s => s.Avaliable == true).ToList();

            Category category = new Category();
            category.Id = 0;
            category.Name = "Brak";

            var categories = db.Categories.ToList();
            categories.Insert(0, category);
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            return View(productList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult DownloadFile(int id)
        {
            var attachedFile = db.AttachedFiles.Find(id);

            if (attachedFile != null)
            {
                var fileBytes = System.IO.File.ReadAllBytes(attachedFile.FilePath); 
                var fileName = attachedFile.FileName;

                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }

            return HttpNotFound();
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

        [HttpPost]
        public ActionResult Details(int? id, int? pusty)
        {
            List<int> products = new List<int>();
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
            HttpCookie cookie;

            if (!string.IsNullOrEmpty(productid))
            {
                cookie = Request.Cookies["a"];
                if (cookie == null)
                {
                    cookie = new HttpCookie("a");
                    cookie.Value = productid;
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    string xd = Request.Cookies["a"].Value;
                    var idlist = xd.Split(' ');
                    for (int i = 0; i <= idlist.Length - 1; i++)
                    {
                        int id1 = Convert.ToInt32(idlist[i]);
                        products.Add(id1);
                    }
                    products.Add(Convert.ToInt32(productid));
                    string resoult = "";
                    foreach (var id2 in products)
                    {
                        resoult = resoult + id2 + " ";
                    }
                    cookie.Value = resoult;
                }

                Response.Cookies.Add(cookie);
                
            }

            return View(product);
        }
    }
}