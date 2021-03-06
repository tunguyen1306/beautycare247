﻿//------------------------------------------------------------------------------
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
   using System.Data.Entity;
   using System.Data.Entity.Infrastructure;
   
   public partial class WebSiteEntities : DbContext
   {
       public WebSiteEntities()
           : base("name=WebSiteEntities")
       {
       }
   
       protected override void OnModelCreating(DbModelBuilder modelBuilder)
       {
           throw new UnintentionalCodeFirstException();
       }
   
       public DbSet<tblMenu> tblMenus { get; set; }
       public DbSet<tblProduct> tblProducts { get; set; }
       public DbSet<tblType> tblTypes { get; set; }
       public DbSet<tblCategory> tblCategories { get; set; }
       public DbSet<tblUser> tblUsers { get; set; }
       public DbSet<tblTypeUser> tblTypeUsers { get; set; }
       public DbSet<tblPromotion> tblPromotions { get; set; }
       public DbSet<tblPicture> tblPictures { get; set; }
       public DbSet<tblHistory> tblHistories { get; set; }
       public DbSet<tblSlider> tblSliders { get; set; }
       public DbSet<tblBlog> tblBlogs { get; set; }
       public DbSet<tblVocabulary> tblVocabularies { get; set; }
       public DbSet<Country> Countries { get; set; }
       public DbSet<CountryText> CountryTexts { get; set; }
       public DbSet<District> Districts { get; set; }
       public DbSet<DistrictText> DistrictTexts { get; set; }
       public DbSet<GeoTableView> GeoTableViews { get; set; }
       public DbSet<Location> Locations { get; set; }
       public DbSet<LocationText> LocationTexts { get; set; }
       public DbSet<Quarter> Quarters { get; set; }
       public DbSet<QuarterText> QuarterTexts { get; set; }
       public DbSet<State> States { get; set; }
       public DbSet<StateText> StateTexts { get; set; }
       public DbSet<GeoIP2CityText> GeoIP2CityText { get; set; }
       public DbSet<Ip2City> Ip2City { get; set; }
       public DbSet<Ip2City2> Ip2City2 { get; set; }
       public DbSet<phuong> phuongs { get; set; }
       public DbSet<VNELocation> VNELocations { get; set; }
       public DbSet<tblShip> tblShips { get; set; }
       public DbSet<tblOrderDetail> tblOrderDetails { get; set; }
       public DbSet<tblOrder> tblOrders { get; set; }
       public DbSet<tblSavePromotion> tblSavePromotions { get; set; }
   }
}
