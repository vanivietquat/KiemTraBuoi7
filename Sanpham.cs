namespace Buoi7KT
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sanpham")]
    public partial class Sanpham
    {
        [Key]
        [StringLength(6)]
        public string MaSP { get; set; }

        [StringLength(30)]
        public string TenSP { get; set; }

        public DateTime? NgayNhap { get; set; }

        [StringLength(2)]
        public string MaLoai { get; set; }

        public virtual LoaiSP LoaiSP { get; set; }
    }
}
