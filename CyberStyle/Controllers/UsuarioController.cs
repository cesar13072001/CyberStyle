using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CyberStyle.Models;

namespace CyberStyle.Controllers
{
    public class UsuarioController : Controller
    {
        ciberstyleEntities db = new ciberstyleEntities();

        public ActionResult Login()
        {
            return View();

        }


        [HttpPost]
        public ActionResult Login(Usuario usu)
        {
            List<login_usuario_Result> user = new List<login_usuario_Result>();
            List<login_usuario_Result> vacio = new List<login_usuario_Result>();

            user.AddRange(db.login_usuario(usu.correo, usu.contrasenia));
            Session["user"] = user;
            if (Session["user"] != null && user.Count() > 0)
            {
                return RedirectToAction("Index", "Home");
            }
            Session["usuario"] = vacio;
            ViewBag.mensaje = "Correo o contraseña incorrecta";

            return View();
        }


        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(Usuario usu)
        {
            string mensaje = string.Empty;
            try
            {         
                usu.idrol = 2;
                usu.fechaRegistro = DateTime.Now;
                db.Usuario.Add(usu);
                db.SaveChanges();
                return RedirectToAction("Login");     
            }
            catch (Exception ex)
            {
                mensaje = "Revise todos los campos";
            }
            ViewBag.mensaje = mensaje;
            return View(usu);
        }

    }
}