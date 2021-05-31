using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CUAHANG.Models.DTO
{
    public class ProductDTO
    {
        public int id { get; set; }

        public int? idTL { get; set; }

        public string Image { get; set; }

        public string TenSP { get; set; }

        public string MoTa { get; set; }

        public decimal? GiaTien { get; set; }

        public int? SoLuong { get; set; }
    }
}