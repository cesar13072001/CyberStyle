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
    
    public partial class DetallePago
    {
        public int idpago { get; set; }
        public int idproducto { get; set; }
        public Nullable<int> cantidad { get; set; }
        public Nullable<decimal> subtotal { get; set; }
    
        public virtual Pago Pago { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
