using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeautyAndCare.Models
{
    public class tblUserDto
    {
       
        public int IdUser { get; set; }
        public string NameUser { get; set; }
        public string PassUser { get; set; }
        public Nullable<int> IdTypeUser { get; set; }
        public Nullable<double> TotalPointUser { get; set; }
        public Nullable<int> PhoneUser { get; set; }
        public string EmailUser { get; set; }
        public string FirtNameUser { get; set; }
        public string LastNameUser { get; set; }
        public string AddressUser { get; set; }
        public Nullable<bool> SubscribeUser { get; set; }
    }
}