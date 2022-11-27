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


        ciberstyleEntities db = new ciberstyleEntities();
        // GET: Pago
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ValPago()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            else if(Session["carrito"] == null)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                return RedirectToAction("Pago");
            }
        }

        public ActionResult pago()
        {
            return View();
        }

        public int Idusuario()
        {
            int idrol = -1;
            if (Session["user"] != null)
            {
                foreach (var item in Session["user"] as List<login_usuario_Result>)
                {
                    idrol = Int32.Parse(item.idusuario.ToString());

                }
                return idrol;
            }
            return -1;
        }
        public ActionResult FinalizarCompra(double total = 0, string numTarjeta = "", string nomTarjeta = "", string annio = "", string mes = "", string cvv = "")
        {


            List<CarritoItem> compras = (List<CarritoItem>)Session["carrito"];
            List<login_usuario_Result> session = (List<login_usuario_Result>)Session["user"];

            int idusuario = -1;
            idusuario = Idusuario();
            if (compras != null && compras.Count() > 0)
            {

                /* int idProducto=0;
                 int cantidadPro = 0;
                 decimal subtotal = 0;
                */


                decimal totalT = 0;
                foreach (var item in Session["carrito"] as List<CarritoItem>)
                {
                    totalT += (decimal)(item.Cantidad * item.Producto.precio);
                }
                /*foreach (CarritoItem x in compras)
                {
                    idProducto = x.Producto.idProducto;
                    cantidadPro = x.Cantidad;
                    subtotal = x.Producto.costo * x.Cantidad;
                }*/




                Pago nuevoPago = new Pago();
                nuevoPago.idusuario = idusuario;
                nuevoPago.fechaPago = DateTime.Now;
                nuevoPago.total = (decimal?)totalT;
                nuevoPago.numTarjeta = numTarjeta;
                nuevoPago.nomTarjeta = nomTarjeta;
                nuevoPago.annio = annio;
                nuevoPago.mes = mes;
                nuevoPago.cvv = cvv;
                nuevoPago.DetallePago = (from Producto in compras
                                         select new DetallePago
                                         {
                                             idproducto = Producto.Producto.idproducto,
                                             cantidad = Producto.Cantidad,
                                             subtotal = Producto.Producto.precio * Producto.Cantidad
                                         }).ToList();

                db.Pago.Add(nuevoPago);
                db.SaveChanges();
                Session["carrito"] = new List<CarritoItem>();
                // EnviarCorreo();
            }
            ViewBag.valor = "Compras";
            var pagoS = from d in db.mostrarUltimoPago(idusuario)
                        select d;
            foreach (var item in pagoS)
            {
                ViewBag.codigopago = item.idpago;
                ViewBag.fechaCompra = item.fechaPago;
            }

            return View();

        }


        //public void EnviarCorreo()
        //{

        //    string destinatario = "";
        //    destinatario = CorreoUsuario();
        //    string EmailOrigen = "clinicasilvifarma@gmail.com";
        //    string EmailDestino = destinatario;
        //    string Contraseña = "apqipyogjfqcmewt";
        //    /*string path = @"C:\turuta\burger.png";
        //    string path2 = @"C:\turuta\a.jpg";*/

        //    int idusu = Idusuario();
        //    var listaDatos = from r in db.usp_listarCompras_ultimo(idusu)
        //                     select r;
        //    int idPago = 0;
        //    string FechaPago = "";
        //    string tarjeta = "";
        //    double total = 0.00;

        //    List<usp_listarCompras_ultimo_Result> listacompras = new List<usp_listarCompras_ultimo_Result>();
        //    listacompras.AddRange(listaDatos);

        //    foreach (var item2 in listacompras)
        //    {
        //        idPago = Int32.Parse(item2.idPago.ToString());
        //        FechaPago = item2.fechaPago.ToString();
        //        tarjeta = item2.numTarjeta.ToString();
        //    }

        //    var listaDatos2 = from r in db.usp_cargarcompras_ultimo(idPago)
        //                      select r;

        //    string tabla = "";

        //    List<usp_cargarcompras_ultimo_Result> listacompras2 = new List<usp_cargarcompras_ultimo_Result>();
        //    listacompras2.AddRange(listaDatos2);
        //    //Image img = null;
        //    foreach (var item2 in listacompras2)
        //    {
        //        /*using (MemoryStream memstr = new MemoryStream(item2.foto))
        //        {
        //            img = Image.FromStream(memstr);                  
        //        }*/
        //        total = (double)item2.total;
        //        tabla +=
        //        "<tr style='height: 50px; vertical-align: middle;'>" +
        //        "<td style='border: 1px solid black;'><span style='display:flex;justify-content: center;'>" + item2.idProducto + "</span></td>" +
        //        "<td style='border: 1px solid black;'><span style='display:flex;justify-content: center;'>" + item2.nombre + "</span></td>" +
        //        "<td style='border: 1px solid black;'><span style='display:flex;justify-content: center;'>" + item2.Cantidad + "</span></td>" +
        //        "<td style='border: 1px solid black;'><span style='display:flex;justify-content: center;'>" + item2.Subtotal + "</span></td>" +
        //        "</tr>";


        //    }
        //    MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Compra Nª " + idPago + " realizado correctamente ",
        //        "<h1>ID de transacción: " + idPago + "</h1>" +
        //        "<h3>Fecha de pago: " + FechaPago + "</h3>" +
        //        "<h3>Metodo de pago: " + tarjeta + "</h3>" +
        //        "<table style='border-collapse: collapse; border: 1px solid black; width: 1000px;'>" +
        //        "<tr style='background-color: rgb(0, 0, 87); color: aliceblue;'>" +
        //        "<th style='border: 1px solid black;'>&nbsp; IDPRODUCTO &nbsp;</th>" +
        //        "<th style='border: 1px solid black;'>&nbsp; NOMBRE &nbsp;</th>" +
        //        "<th style='border: 1px solid black;'>&nbsp; CANTIDAD &nbsp;</th>" +
        //        "<th style='border: 1px solid black;'>&nbsp; SUBTOTAL &nbsp;</th>" +
        //        tabla +
        //        "</table>" +
        //        "<h1> Total pagado: S/" + total + "</h1>" +
        //        "<h3> Gracias por comprar </h3>"
        //        );




        //    /*oMailMessage.Attachments.Add(new Attachment(path));
        //    oMailMessage.Attachments.Add(new Attachment(path2));*/

        //    oMailMessage.IsBodyHtml = true;
        //    SmtpClient oSmtpCliente = new SmtpClient("smtp.gmail.com");
        //    oSmtpCliente.EnableSsl = true;
        //    oSmtpCliente.UseDefaultCredentials = false;
        //    oSmtpCliente.Port = 587;
        //    oSmtpCliente.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);
        //    oSmtpCliente.Send(oMailMessage);
        //    oSmtpCliente.Dispose();

        //}

    }
}