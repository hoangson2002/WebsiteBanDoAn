using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.ExtendModels.Login
{
    public class DauVaoDangNhapViewModel //thông tin login
    {
        public DauVaoDangNhapViewModel()
        {
            this.TenDangNhap = "";
            this.MatKhau = "";
        }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
    }
}
