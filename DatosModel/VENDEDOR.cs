//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatosModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class VENDEDOR
    {
        public decimal CODIGO { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO { get; set; }
        public string NUMERO_IDENTIFICACION { get; set; }
        public Nullable<decimal> CODIGO_CIUDAD { get; set; }
    
        public virtual CIUDAD CIUDAD { get; set; }
    }
}
