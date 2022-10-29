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

        public ActionResult Index()
        {
            var lista = from d in db.p_productoMasVendido02() select d;

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
    }
}