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
   
   public partial class tblMenu
   {
       public int IdMenu { get; set; }
       public string NameMenu { get; set; }
       public string NameMenu_en { get; set; }
       public Nullable<int> IdParentMenu { get; set; }
       public Nullable<int> StatusMenu { get; set; }
       public Nullable<int> OrderMenu { get; set; }
       public Nullable<int> IsParent { get; set; }
       public string LinkMenu { get; set; }
       public Nullable<int> IsCategoryMenu { get; set; }
       public Nullable<int> ShowHomeMenu { get; set; }
       public string Description_en { get; set; }
       public string Description_vi { get; set; }
   }
}
