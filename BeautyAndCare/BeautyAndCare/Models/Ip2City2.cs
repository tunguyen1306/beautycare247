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
   
   public partial class Ip2City2
   {
       public string network { get; set; }
       public string network_start_ip { get; set; }
       public string network_last_ip { get; set; }
       public Nullable<long> network_start_integer { get; set; }
       public Nullable<long> network_last_integer { get; set; }
       public string geoname_id { get; set; }
       public string registered_country_geoname_id { get; set; }
       public string represented_country_geoname_id { get; set; }
       public string is_anonymous_proxy { get; set; }
       public string is_satellite_provider { get; set; }
       public string postal_code { get; set; }
       public string latitude { get; set; }
       public string longitude { get; set; }
       public string city { get; set; }
       public Nullable<int> city_id { get; set; }
       public System.Guid rowguid { get; set; }
   }
}
