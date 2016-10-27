using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyAndCare.Models
{
    public class ListAll
    {
        public List<tblMenu> ListMenuAll { get; set; }
        public List<tblProduct> ListProductAll { get; set; }
        public tblProduct tblProductAll { get; set; }
    }
}