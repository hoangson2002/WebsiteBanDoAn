using QuanLyBanDoAnNhanh.ExtendModels.Login;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.RepoContracts
{
   public interface ILoginRepository
    {
        public Task<DauRaDangNhapViewModel> DangNhap(DauVaoDangNhapViewModel user);
        public Task<ThongTinNguoiDungViewModel> LayThongTinTheoTenDangNhap(string TenDangNhap);
        public Task<DauRaDangNhapViewModel> TaoTokenMoi(ThongTinNguoiDungViewModel user);
    }
}
