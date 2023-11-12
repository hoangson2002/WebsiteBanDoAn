using Dapper;
using Microsoft.IdentityModel.Tokens;
using QuanLyBanDoAnNhanh.Context;
using QuanLyBanDoAnNhanh.ExtendModels.Login;
using QuanLyBanDoAnNhanh.RepoContracts;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.Repository
{
    public class LoginRepository: ILoginRepository
    {
        private readonly DapperContext _context;
        public LoginRepository(DapperContext context)
        {
            _context = context;
        }
        
        private static string Encrypt(string toEncrypt)
       {

                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

                const string key = "!hoang@tuan#son$";
                
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                //set the secret key for the tripleDES algorithm
                tdes.Key = keyArray;
                //mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)
                tdes.Mode = CipherMode.ECB;
                //padding mode(if any extra byte added)
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                //transform the specified region of bytes array to resultArray
                byte[] resultArray = cTransform.TransformFinalBlock
                        (toEncryptArray, 0, toEncryptArray.Length);
                //Release resources held by TripleDes Encryptor
                tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        private string TaoToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("DoAnTotNgiep2023@hihi");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("username", username) }),//lấy user tạo mã
                Expires = DateTime.UtcNow.AddMinutes(20), //thời gian token hiệu lực
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private int KiemTraTaiKhoan(DauVaoDangNhapViewModel user)
        {
            try
            {
                var query = "DECLARE @re INT = 0;" +
                            "DECLARE @ID_TaiKhoan INT = CASE WHEN EXISTS (SELECT 1 FROM dbo.DM_TaiKhoan t WHERE t.TenDangNhap = @TenDangNhap AND t.MatKhau = @MatKhau AND t.Delete_Mark = 0) " +
                            "THEN (SELECT TOP 1 t.ID_TaiKhoan FROM dbo.DM_TaiKhoan t WHERE t.TenDangNhap = @TenDangNhap AND t.MatKhau = @MatKhau AND t.Delete_Mark = 0)" +
                            "ELSE -1 END;" +
                            "IF(@ID_TaiKhoan <= 0)" +
                            "   BEGIN" +
                            "       SET @re = -1;" + //TÀI KHOẢN MẬT KHẨU KHÔNG ĐÚNG
                            "   END;" +
                            "ELSE" +
                            "   BEGIN" +
                            "      IF EXISTS (SELECT 1 FROM dbo.DM_TaiKhoan t WHERE t.ID_TaiKhoan = @ID_TaiKhoan AND t.IsLock = 0)" +
                            "          BEGIN" +
                            "               SET @re = 1;" +//TÀI KHOẢN MẬT KHẨU CHÍNH XÁC VÀ ĐANG HOẠT ĐỘNG	
                            "          END" + 	
                            "      ELSE" +
                            "           BEGIN" +
                            "               SET @re = 0;" + //TÀI KHOẢN MẬT KHẨU CHÍNH XÁC NHƯNG BỊ VÔ HIỆU HÓA
                            "           END" +
                            "   END;" +
                            "SELECT @re AS re;";
                var parameters = new DynamicParameters();
                parameters.Add("TenDangNhap", user.TenDangNhap, DbType.String, ParameterDirection.Input);
                parameters.Add("MatKhau", Encrypt(user.MatKhau), DbType.String, ParameterDirection.Input);

                using (var connection = _context.CreateConnection())
                {
                    var flag = connection.ExecuteScalar<int>(query, parameters);
                    return flag;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("KiemTraTaiKhoan", ex);
            }
        }

        public async Task<ThongTinNguoiDungViewModel> LayThongTinTheoTenDangNhap(string TenDangNhap)
        {
            try
            {
                var procedureName = "DM_TaiKhoan_Get_InFo_By_TenDangNhap";
                var parameters = new DynamicParameters();
                parameters.Add("TenDangNhap", TenDangNhap, DbType.String, ParameterDirection.Input);

                using (var connection = _context.CreateConnection())
                {
                    var user = await connection.QueryFirstOrDefaultAsync<ThongTinNguoiDungViewModel>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);

                    return user;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("LayThongTinTheoTenDangNhap", ex);
            }
        }

        public async Task<DauRaDangNhapViewModel> DangNhap(DauVaoDangNhapViewModel user)
        {
            try
            {
                int flag = KiemTraTaiKhoan(user);
                if (flag == 1)
                {
                    string tokenString = TaoToken(user.TenDangNhap);

                    ThongTinNguoiDungViewModel userForSessionModel = await LayThongTinTheoTenDangNhap(user.TenDangNhap);

                    return new DauRaDangNhapViewModel(userForSessionModel, tokenString, flag);
                }
                else
                {
                    ThongTinNguoiDungViewModel model = new ThongTinNguoiDungViewModel();
                    return new DauRaDangNhapViewModel(model, "", flag);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("DangNhap", ex);
            }
        }

        public async Task<DauRaDangNhapViewModel> TaoTokenMoi(ThongTinNguoiDungViewModel user)
        {
            try
            {
                string tokenString = TaoToken(user.TenDangNhap);

                ThongTinNguoiDungViewModel userForSessionModel = await LayThongTinTheoTenDangNhap(user.TenDangNhap);

                return new DauRaDangNhapViewModel(userForSessionModel, tokenString);

            }
            catch (Exception ex)
            {
                throw new ArgumentException("TaoTokenMoi", ex);
            }
        }

    }
}
