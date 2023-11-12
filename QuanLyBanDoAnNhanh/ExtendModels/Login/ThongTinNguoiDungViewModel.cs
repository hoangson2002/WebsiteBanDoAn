using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.ExtendModels.Login
{
    public class ThongTinNguoiDungViewModel
    {
        public ThongTinNguoiDungViewModel()
        {
            this.ID_TaiKhoan = 0;
            this.SoLuongDonDaDat = 0;
            this.TenDayDu = "";
            this.TenDangNhap = "";
            this.Email = "";
            this.SoDienThoai = "";
            this.IsGioiTinh = false;
            this.NgaySinh = new DateTime();
            this.SoNha = "";
            this.DiaChiCuThe = "";
            this.ID_PhuongXa = 0;
            this.ID_QuanHuyen = 0;
            this.ID_TinhThanh = 0;
        }


        public int ID_TaiKhoan { get; set; }
        public int SoLuongDonDaDat { get; set; }
        public string TenDangNhap { get; set; }
        public string TenDayDu { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public bool IsGioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoNha { get; set; }
        public string DiaChiCuThe { get; set; }
        public int ID_PhuongXa { get; set; }
        public int ID_QuanHuyen { get; set; }
        public int ID_TinhThanh { get; set; }

    }
}
