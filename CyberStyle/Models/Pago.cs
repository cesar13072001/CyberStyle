//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CyberStyle.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class Pago
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pago()
        {
            this.DetallePago = new HashSet<DetallePago>();
            this.Reclamos = new HashSet<Reclamos>();
        }
    
        public int idpago { get; set; }
        public Nullable<int> idusuario { get; set; }
        public Nullable<System.DateTime> fechaPago { get; set; }
        public string nomTarjeta { get; set; }
        public string numTarjeta { get; set; }
        public string annio { get; set; }
        public string mes { get; set; }
        public string cvv { get; set; }
        public Nullable<decimal> total { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetallePago> DetallePago { get; set; }
        public virtual Usuario Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reclamos> Reclamos { get; set; }


        public List<Pago> listar()
        {

            var pagos = new List<Pago>();
            string cadena = "SELECT * FROM PAGO";

            try
            {
                using (var contenedor = new ciberstyleEntities())
                {
                    pagos = contenedor.Database.SqlQuery<Pago>(cadena).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return pagos;
        }
        
        
    }
}
