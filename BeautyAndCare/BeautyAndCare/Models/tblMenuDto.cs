using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyAndCare.Models
{
    public class tblMenuDto
    {
        public tblMenu tblMenu { get; set; }
        public string NameMenuParent { get; set; }
        public List<tblMenu> ListMenu { get; set; }

    }
}