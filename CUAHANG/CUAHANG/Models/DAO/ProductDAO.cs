using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CUAHANG.Models.DTO;
using CUAHANG.Models.Entities;
using PagedList.Mvc;
using PagedList;
using System.Data.SqlClient;
using System.IO;
using System.Web.Mvc;

namespace CUAHANG.Models.DAO
{
    public class ProductDAO
    {
        Model1 db;

        public ProductDAO()
        {
            db = new Model1();
        }

        //public IQueryable<PRODUCT> ListProduct()
        //{
        //    var res = (from s in db.PRODUCTs
        //               where s.GiaTien > 50
        //               orderby s.TenSP descending
        //               select s);
        //    return res;
        //}


        //public IEnumerable<ProductDTO> list(int Pagenum, int PageSize)
        //{
        //    var lst = db.Database.SqlQuery<ProductDTO>("select " +
        //        " pro.idTL as idTL, " +
        //        " c.TenTL as TenTL " +
        //        " pro.Image as Image, " +
        //        " pro.TenSP as TenSP, " +
        //        " pro.MoTa as MoTa, " +
        //        " pro.GiaTien as GiaTien, " +
        //        " pro.SoLuong as SoLuong, " +
        //        " from PRODUCT pro left join CATEGORY c on pro.idTL=c.idTL")
        //        .ToPagedList<ProductDTO>(Pagenum, PageSize);
        //    return lst;
        //}

        public IEnumerable<PRODUCT> list(int page, int pageSize)
        {
            return db.PRODUCTs.OrderBy(x=>x.id).ToPagedList(page, pageSize);
        }

        public int Insert( int idTL, string Image, string TenSP, string MoTa, decimal GiaTien, int SoLuong, HttpPostedFileBase uploadImage)
        {
            PRODUCT product = new PRODUCT();
            product.idTL = idTL;
            product.TenSP = TenSP;
            product.Image = Image;
            product.MoTa = MoTa;
            product.GiaTien = GiaTien;
            product.SoLuong = SoLuong;

            var fileName = Path.GetFileName(uploadImage.FileName);
            var path = Path.Combine(HttpContext.Current.Server.MapPath("~/uploadImg"), fileName);
            uploadImage.SaveAs(path);

            product.Image= uploadImage.FileName;

            db.PRODUCTs.Add(product);
            db.SaveChanges();

            return product.id;
        }

        public void Update(PRODUCT n)
        {
            PRODUCT product = db.PRODUCTs.Find(n.id);
            if (product != null)
            {
                product.idTL = n.idTL;
                product.CATEGORY.TenTL = n.CATEGORY.TenTL;
                product.Image = n.Image;
                product.TenSP = n.TenSP;
                product.MoTa = n.MoTa;
                product.GiaTien = n.GiaTien;
                product.SoLuong = n.SoLuong;
                db.SaveChanges();
            }
        }

        public void Updatepro(int id, int idTL, string TenTL, string Image, string TenSP, string MoTa, decimal GiaTien, int SoLuong, HttpPostedFileBase uploadImage)
        {
            PRODUCT product = db.PRODUCTs.Find(id);
            if (product != null)
            {
                product.idTL = idTL;
                product.CATEGORY.TenTL = TenTL;
                //product.Image = Image;
                product.TenSP = TenSP;
                product.MoTa = MoTa;
                product.GiaTien = GiaTien;
                product.SoLuong = SoLuong;

                if(uploadImage != null)
                {
                    var filename = Path.GetFileName(uploadImage.FileName);
                    var path = Path.Combine(HttpContext.Current.Server.MapPath("~/uploadImg"), filename);
                    uploadImage.SaveAs(path);

                    product.Image = uploadImage.FileName;
                }    
                

                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            PRODUCT product = db.PRODUCTs.Find(id);
            if (product != null)
            {
                db.PRODUCTs.Remove(product);
                db.SaveChanges();
            }
        }

        public PRODUCT FindByid(int id)
        {
            return db.PRODUCTs.Find(id);
        }

        public PRODUCT ViewDetail(int id)
        {
            return db.PRODUCTs.Find(id);
        }
    }
}
