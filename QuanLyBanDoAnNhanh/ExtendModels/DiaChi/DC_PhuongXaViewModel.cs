using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.ExtendModels.DiaChi
{
    public class DC_PhuongXaViewModel
    {
        public int ID_PhuongXa { get; set; }
        public string MaPhuongXa { get; set; }
        public string TenPhuongXa { get; set; }
        public int ID_QuanHuyen { get; set; }
        public int ID_TinhThanh { get; set; }
        public string DiaChi { get; set; }
    }
}
