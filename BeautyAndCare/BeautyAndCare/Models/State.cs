//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BeautyAndCare.Models
{
   using System;
   using System.Collections.Generic;
   
   public partial class State
   {
       public int state_id { get; set; }
       public string countryCode { get; set; }
       public int name_id { get; set; }
       public string stateCode { get; set; }
       public Nullable<decimal> latitude { get; set; }
       public Nullable<decimal> longitude { get; set; }
       public string polygon { get; set; }
       public System.Guid rowguid { get; set; }
   }
}
