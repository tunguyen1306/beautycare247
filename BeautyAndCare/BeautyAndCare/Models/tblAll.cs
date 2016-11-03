using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public List<tblCategory> ListCategory { get; set; }
        public List<tblType> ListType { get; set; }
        public List<tblPicture> ListPicture { get; set; }
        public tblPicture clPicture { get; set; }

        public List<tblProduct> tblPro { get; set; }
        public List<tblProduct> tblProductRel { get; set; }
        public List<tblMenu> ListMenu { get; set; }
        public tblAll()
        {
            tblMenu = new tblMenu();
            tblProducts = new tblProduct();
            tblType = new tblType();
            tblCategory = new tblCategory();
            tblTypeUser = new tblTypeUser();
            tblPromotion = new tblPromotion();
            tblUser = new tblUser();
            ListCategory = new List<tblCategory>();
            ListType = new List<tblType>();
            ListMenu = new List<tblMenu>();
        }

        public int idProducts { get; set; }
        public string cfile { get; set; }
        public string nameImg { get; set; }
        public byte isactive { get; set; }
        public int idpicture { get; set; }
        public int index { get; set; }
        public string smallImage()
        {
            return String.Format(clPicture.ConvertedFilename, 2);
        }
        public string LargeImage()
        {
            return String.Format(clPicture.ConvertedFilename,1);
           

        }
        public string GetFilePathPhysical(PictureSize size)
        {
            // check if we have converted files
            //if (IsConverted)
            return DirectoryPhysical + FileName(size);
            //else
            //    return clPicture.originalFilepath;
        }
        public enum PictureSize : int
        {
            Original = 853, // The summ of ascii values of the word "original"
            Large = 1,
            Medium = 2,
            Small = 3,
            Tiny = 4
        }
        public string DirectoryPhysical
        {
            get { return "~/fileUpload"; }
        }
        public string FileName(PictureSize size)
        {
            // check if we have converted files
            //if (IsConverted)
            //{
            switch (AngelType)
            {
                case RotationAngle.Rotated0:
                    return string.Format(clPicture.ConvertedFilename, (int)size);
                    break;

                case RotationAngle.Rotated90:
                    if (!string.IsNullOrWhiteSpace(clPicture.ConvertedFilename90))
                        return string.Format(clPicture.ConvertedFilename90, (int)size);
                    break;

                case RotationAngle.Rotated180:
                    if (!string.IsNullOrWhiteSpace(clPicture.ConvertedFilename180))
                        return string.Format(clPicture.ConvertedFilename180, (int)size);
                    break;

                case RotationAngle.Rotated270:
                    if (!string.IsNullOrWhiteSpace(clPicture.ConvertedFilename270))
                        return string.Format(clPicture.ConvertedFilename270, (int)size);
                    break;
            }

            return "";
            //}
            //else
            //{
            //    if (clPicture.originalFilepath.StartsWith("http:", StringComparison.InvariantCultureIgnoreCase))
            //    {
            //        return Path.GetFileName(clPicture.originalFilepath);
            //    }
            //    return clPicture.originalFilepath;
            //}
        }
        //public bool IsConverted
        //{
        //    get
        //    {
        //        switch (AngelType)
        //        {
        //            default:
        //                return !string.IsNullOrWhiteSpace(clPicture.ConvertedFilename);

        //            case RotationAngle.Rotated90:
        //                return !string.IsNullOrWhiteSpace(clPicture.ConvertedFilename90);

        //            case RotationAngle.Rotated180:
        //                return !string.IsNullOrWhiteSpace(clPicture.ConvertedFilename180);

        //            case RotationAngle.Rotated270:
        //                return !string.IsNullOrWhiteSpace(clPicture.ConvertedFilename270);
        //        }
        //    }
        //}
        public RotationAngle AngelType
        {
            get { return (RotationAngle)AngelTypeId; }
            set { AngelTypeId = (int)value; }
        }
        public enum RotationAngle : int
        {
            Rotated0 = 0,
            Rotated90 = 1,
            Rotated180 = 2,
            Rotated270 = 3,
        }
        public int AngelTypeId { get; set; }
        public string GetConvertedFileName()
        {
            // check if we have converted files
            //if (IsConverted)
            //{
            switch (AngelType)
            {
                case RotationAngle.Rotated0:
                    return clPicture.ConvertedFilename;
                    break;

                case RotationAngle.Rotated90:
                    return clPicture.ConvertedFilename90;
                    break;

                case RotationAngle.Rotated180:
                    return clPicture.ConvertedFilename180;
                    break;

                case RotationAngle.Rotated270:
                    return clPicture.ConvertedFilename270;
                    break;
            }

            return null;
            //}
            //else
            //    return null;
        }
        public string SetFileName(string filenamePattern)
        {
            // check if we have converted files

            switch (AngelType)
            {
                case RotationAngle.Rotated0:
                    clPicture.ConvertedFilename = filenamePattern;
                    break;

                case RotationAngle.Rotated90:
                    clPicture.ConvertedFilename90 = filenamePattern;
                    break;

                case RotationAngle.Rotated180:
                    clPicture.ConvertedFilename180 = filenamePattern;
                    break;

                case RotationAngle.Rotated270:
                    clPicture.ConvertedFilename270 = filenamePattern;
                    break;
            }

            return "";
        }
        public string CreateFilename()
        {
            string encoded = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            encoded = encoded.Replace("/", "_").Replace("+", "-");
            return encoded.Substring(0, 22);
        }
        public string GetFilePath(PictureSize size)
        {
            // check if we have converted files

            return FileName(size);

        }
        public WatermarkType WaterMarkLarge { get; set; }
        public enum WatermarkType
        {
            [Description("none")]
            None = 0,
            [Description("image")]
            Image,
            [Description("text")]
            Text
        }
    }
}