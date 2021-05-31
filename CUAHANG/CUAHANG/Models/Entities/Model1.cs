using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CUAHANG.Models.Entities
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<CATEGORY> CATEGORies { get; set; }
        public virtual DbSet<LOGIN> LOGINs { get; set; }
        public virtual DbSet<PRODUCT> PRODUCTs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CATEGORY>()
                .Property(e => e.TenTL)
                .IsUnicode(false);

            modelBuilder.Entity<CATEGORY>()
                .HasMany(e => e.PRODUCTs)
                .WithOptional(e => e.CATEGORY)
                .HasForeignKey(e => e.idTL);

            modelBuilder.Entity<LOGIN>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<LOGIN>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCT>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCT>()
                .Property(e => e.TenSP)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCT>()
                .Property(e => e.GiaTien)
                .HasPrecision(18, 3);
        }
    }
}
