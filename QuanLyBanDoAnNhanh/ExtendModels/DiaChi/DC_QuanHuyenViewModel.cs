using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.ExtendModels.DiaChi
{
    public class DC_QuanHuyenViewModel
    {
        public int ID_QuanHuyen { get; set; }
        public string MaQuanHuyen { get; set; }
        public int ID_TinhThanh { get; set; }
        public string TenQuanHuyen { get; set; }
    }
}
