using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CyberStyle.Models;
using System.Web.Helpers;
using System.Data.Entity;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace CyberStyle.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        ciberstyleEntities1 db = new ciberstyleEntities1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductoListar()
        {
            var lista = from p in db.usp_ProductoListar() select p;

            return View(lista.ToList());
        }

        public ActionResult ProductoCreate()
        {
         
            ViewBag.Categoria = new SelectList(db.Categoria.ToList(), "idcategoria", "nomcategoria");
            return View(new Producto());


        }

        [HttpPost]
        public ActionResult ProductoCreate(Producto obj)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase fileBase = Request.Files[0];
                WebImage image = new WebImage(fileBase.InputStream);
                obj.foto = image.GetBytes();
                db.Producto.Add(obj);
                db.SaveChanges();
                return RedirectToAction("ProductoCreate");
            }

            return View("ProductoCreate", obj);

        }

    }
}