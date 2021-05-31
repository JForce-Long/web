namespace CUAHANG.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PRODUCT")]
    public partial class PRODUCT
    {
        public int id { get; set; }

        public int? idTL { get; set; }

        [StringLength(50)]
        public string Image { get; set; }

        [StringLength(50)]
        public string TenSP { get; set; }

        [StringLength(50)]
        public string MoTa { get; set; }

        public decimal? GiaTien { get; set; }

        public int? SoLuong { get; set; }

        public virtual CATEGORY CATEGORY { get; set; }
    }
}
