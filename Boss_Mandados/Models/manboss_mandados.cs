//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Boss_Mandados.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class manboss_mandados
    {
        public int id { get; set; }
        public int estado { get; set; }
        public int cliente { get; set; }
        public Nullable<int> repartidor { get; set; }
        public double total { get; set; }
        public Nullable<System.DateTime> tiempo_trayecto { get; set; }
        public Nullable<System.DateTime> tiempo_total { get; set; }
        public int cuenta_pendiente { get; set; }
        public System.DateTime fecha { get; set; }
        public int tipo_pago { get; set; }
    }
}
