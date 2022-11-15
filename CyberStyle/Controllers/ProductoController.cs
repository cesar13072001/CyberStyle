using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using CyberStyle.Models;


namespace CyberStyle.Controllers
{
    public class ProductoController : Controller
    {
        ciberstyleEntities db = new ciberstyleEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getImage(int id)
        {

            Producto producto = db.Producto.Find(id);
            if (producto.imagen != null)
            {
                byte[] byteImage = producto.imagen;
                MemoryStream memoryStream = new MemoryStream(byteImage);
                Image image = Image.FromStream(memoryStream);
                memoryStream = new MemoryStream();
                image.Save(memoryStream, ImageFormat.Jpeg);
                memoryStream.Position = 0;
                return File(memoryStream, "image/jpg");
            }
            return null;
        }

        public int IdRol()
        {
            int idrol = -1;
            if (Session["user"] != null)
            {
                foreach (var item in Session["user"] as List<login_usuario_Result>)
                {
                    idrol = Int32.Parse(item.idrol.ToString());

                }
                return idrol;
            }
            return -1;
        }

        public ActionResult Listar()
        {
            if (IdRol() == 1)
            {
                var lista = from p in db.Producto select p;

                return View(lista.ToList());
            }
            return RedirectToAction("Index","Home");
        }

        public ActionResult Create()
        {
            if (IdRol() == 1)
            {
                ViewBag.categorias = new SelectList(db.Categoria.ToList(), "idcategoria", "nomcategoria");
                return View(new Producto());
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Create(Producto obj)
        {
            if (IdRol() == 1)
            {

                HttpPostedFileBase fileBase = Request.Files[0];
                WebImage image = new WebImage(fileBase.InputStream);
                obj.imagen = image.GetBytes();
                obj.masvend = 0;
                obj.estado = "Activo";
                obj.precio = (decimal)obj.precio;
                db.Producto.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Listar");

        }
            return RedirectToAction("Index","Home");
    }


        public ActionResult Edit(int id)
        {
            if (IdRol() == 1)
            {

            Producto producto = db.Producto.Find(id);
            ViewBag.categorias = new SelectList(db.Categoria.ToList(), "idcategoria", "nomcategoria",producto.idcategoria);
            return View(producto);
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public ActionResult Edit(Producto obj)
        {
           if (IdRol() == 1)
           {
            try
            {
                var nuevo = db.Producto.FirstOrDefault(x => x.idproducto == obj.idproducto);
                HttpPostedFileBase fileBase = Request.Files[0];
                if (fileBase.ContentLength != 0)
                {
                    WebImage image = new WebImage(fileBase.InputStream);
                    obj.imagen = image.GetBytes();
                }
                else obj.imagen = db.Producto.Find(obj.idproducto).imagen;
                nuevo.idproducto = obj.idproducto;
                nuevo.nombre = obj.nombre;
                nuevo.descripcion = obj.descripcion;
                nuevo.precio = obj.precio;
                nuevo.idcategoria = obj.idcategoria;
                nuevo.imagen = obj.imagen;
                nuevo.stock = obj.stock;
                nuevo.estado = obj.estado;
                nuevo.masvend = obj.masvend;

                db.SaveChanges();
                return RedirectToAction("Listar");
            }
            catch(Exception e)
            {
                return View(obj);
            }
            }
            return RedirectToAction("Index", "Home");


        }

    }
}