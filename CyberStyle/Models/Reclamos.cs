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
    
    public partial class Reclamos
    {
        public string idreclamo { get; set; }
        public Nullable<int> idpago { get; set; }
        public string telefono { get; set; }
        public string descripcion { get; set; }
        public string estado { get; set; }
        public string fecha { get; set; }
    
        public virtual Pago Pago { get; set; }
    }
}
