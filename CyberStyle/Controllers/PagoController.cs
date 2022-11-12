using CyberStyle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CyberStyle.Controllers
{
    public class PagoController : Controller
    {

        private Pago pa = new Pago();

        [HttpGet]

        public ActionResult Index()
        {

            ViewBag.alerta = "info";
            return View(pa.listar());
        }
    }
}