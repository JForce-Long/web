using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CUAHANG.Models.Entities;
using CUAHANG.Models.DAO;
using CUAHANG.Models.DTO;
using PagedList;

namespace CUAHANG.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        Model1 center = new Model1();
        Model1 db = new Model1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Loginn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAction(string username, string password)
        {
            var check = center.LOGINs.Where(x => x.Username == username && x.Password == password);

            if (check.Count() > 0)
            {
                Session["user"] = check.First().Username;
                return RedirectToAction("Products", "Store");
            }
            else
            {
                return RedirectToAction("Loginn", "Store");
            }
        }

        public ActionResult Products( int page = 1, int pageSize = 10, string keysearch = "", string searchCategory = "", string searchPrice = "huy", int priceFrom = 0, int priceTo = 0)
        {
             if (Session["user"] == null)
             {
                 return RedirectToAction("Login", "Store");
             }

            var dao = new ProductDAO();
            var model = dao.list(page, pageSize);
            return View(model);

            ViewBag.page = page;
            ViewBag.searchStr = keysearch;
            ViewBag.searchCategory = searchCategory;
            
            if(priceFrom > priceTo)
            {
                priceTo = priceFrom;
            }    
            ViewBag.PriceFrom = priceFrom;
            ViewBag.PriceTo = priceTo;

            if(priceTo>0 || priceFrom>0)
            {
                var listProduct = center.PRODUCTs.OrderBy(sp => sp.id).Where(sanPham => sanPham.GiaTien>=priceFrom&&sanPham.GiaTien<=priceTo).Where(sanPham => sanPham.TenSP.Contains(keysearch) && sanPham.CATEGORY.TenTL.Contains(searchCategory)).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return View(listProduct);
            }

            if (searchPrice == "huy")
            {
                var listProduct = center.PRODUCTs.OrderBy(sanPham => sanPham.id).Where(sanPham => sanPham.TenSP.Contains(keysearch) && sanPham.CATEGORY.TenTL.Contains(searchCategory)).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return View(listProduct);

            }
            else if (searchPrice == "tang")
            {
                var listProduct = center.PRODUCTs.OrderBy(sanPham => sanPham.GiaTien).Where(sanPham => sanPham.TenSP.Contains(keysearch) && sanPham.CATEGORY.TenTL.Contains(searchCategory)).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return View(listProduct);
            }
            else 
            {
                var listProduct = center.PRODUCTs.OrderByDescending(sanPham => sanPham.GiaTien).Where(sanPham => sanPham.TenSP.Contains(keysearch) && sanPham.CATEGORY.TenTL.Contains(searchCategory)).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return View(listProduct);
            }


        }

        public ActionResult Create()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Store");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create( int idTL, string Image, string TenSP, string MoTa, decimal GiaTien, int SoLuong, HttpPostedFileBase uploadImage)
        {
            if (uploadImage != null)
            {
                ProductDAO product = new ProductDAO();
                product.Insert( idTL, Image, TenSP, MoTa, GiaTien, SoLuong, uploadImage);
            }

            return RedirectToAction("Products", "Store");
        }

        public ActionResult Edit(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Store");
            }

            PRODUCT ds = db.PRODUCTs.Find(id);
            return View(ds);
        }

        [HttpPost]
        public ActionResult Edit( int id, int idTL, string TenTL, string Image, string TenSP, string MoTa, decimal GiaTien, int SoLuong, HttpPostedFileBase uploadImage)
        {

            
                ProductDAO product = new ProductDAO();
                product.Updatepro( id, idTL, TenTL, Image, TenSP, MoTa, GiaTien, SoLuong, uploadImage);
           

            return RedirectToAction("Products", "Store");
        }

        public ActionResult Details(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Store");
            }

            var ds = new ProductDAO().ViewDetail(id);
            return View(ds);
        }

        public ActionResult Delete()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Store");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            ProductDAO product = new ProductDAO();
            product.Delete(id);

            return RedirectToAction("Products", "Store");
        }
    }
}