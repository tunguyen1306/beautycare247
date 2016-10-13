using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyAndCare.Models
{
    public class tblAll
    {
        public tblMenu tblMenu { get; set; }
        public tblProduct tblProducts { get; set; }
        public tblType tblType { get; set; }
        public tblCategory tblCategory { get; set; }
        public tblTypeUser tblTypeUser { get; set; }
        public tblPromotion tblPromotion { get; set; }
        public tblUser tblUser { get; set; }
        public tblAll()
        {
            tblMenu = new tblMenu();
            tblProducts = new tblProduct();
            tblType = new tblType();
            tblCategory = new tblCategory();
            tblTypeUser = new tblTypeUser();
            tblPromotion = new tblPromotion();
            tblUser = new tblUser();
        }
    }
}