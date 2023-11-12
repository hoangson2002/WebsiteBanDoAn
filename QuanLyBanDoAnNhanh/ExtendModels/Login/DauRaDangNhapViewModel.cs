using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.ExtendModels.Login
{
    public class DauRaDangNhapViewModel //trả về trạng thái login
    {
        public DauRaDangNhapViewModel()
        {
            this.TaiKhoan = null;
            this.Token = "";
        }
        public DauRaDangNhapViewModel(ThongTinNguoiDungViewModel user, string token, int ercode = 0)
        {
            this.TaiKhoan = user;
            this.Token = token;
            this.erCode = ercode;
        }

        public ThongTinNguoiDungViewModel TaiKhoan { get; set; }
        public string Token { get; set; }
        public int erCode { get; set; }
    }

}

