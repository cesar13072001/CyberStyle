using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CyberStyle.Models;


namespace CyberStyle.Controllers
{
    public class HomeController : Controller
    {
        ciberstyleEntities db = new ciberstyleEntities();

        public int getIndex(int id)
        {
            List<CarritoItem> compras = (List<CarritoItem>)Session["carrito"];
            for (int i = 0; i < compras.Count(); i++)
            {
                if (compras[i].Producto.idproducto == id)
                    return i;
            }
            return -1;
        }

        [HttpPost]
        public ActionResult AgregarCarrito(int id, int cantidad)
        {
            if (Session["carrito"] == null)
            {
                List<CarritoItem> compras = new List<CarritoItem>();
                compras.Add(new CarritoItem(db.Producto.Find(id), cantidad));
                Session["carrito"] = compras;
                ViewBag.mensajeA = "Producto Añadido";
            }
            else
            {
                List<CarritoItem> compras = (List<CarritoItem>)Session["carrito"];
                int indexExistente = getIndex(id);
                if (indexExistente == -1)
                    compras.Add(new CarritoItem(db.Producto.Find(id), cantidad));
                else
                    compras[indexExistente].Cantidad += cantidad;
                Session["carrito"] = compras;
                ViewBag.mensajeA = "Producto Añadido";
            }
            return Json(new { response = true }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult AgregarCarrito()
        {
            return RedirectToAction("Index");
        }


        public ActionResult Index()
        {
            var lista = from d in db.p_productoMasVendido02() select d;
            ViewBag.lista02 = (from d in db.p_productoGeneral() select d).ToList();

            return View(lista.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Eliminar(int id)
        {
            List<CarritoItem> compras = (List<CarritoItem>)Session["carrito"];
            compras.RemoveAt(getIndex(id));
            return RedirectToAction("/");
        }
    }
}