using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class DiemLopSau
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public float Toan { get; set; }
        public float NguVan { get; set; }
        public float NgoaiNgu { get; set; }
        public float Tin { get; set; }
        public float LichSu { get; set; }
        public float DiaLy { get; set; }
        public float GiaoDucCongDan { get; set; }
        public float CongNghe { get; set; }
        public float VatLy { get; set; }
        public float SinhHoc { get; set; }
        public float AmNhac { get; set; }
        public float MyThuat { get; set; }
        public float DiemTrungBinhCong { get; set; }
        public int loai { get; set; }
    }
}
