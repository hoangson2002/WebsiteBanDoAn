using Dapper;
using QuanLyBanDoAnNhanh.Context;
using QuanLyBanDoAnNhanh.ExtendModels;
using QuanLyBanDoAnNhanh.RepoContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.Repository
{
    public class ThanhToanRepository: IThanhToanRepository
    {
        private readonly DapperContext _context;
        public ThanhToanRepository(DapperContext context)
        {
            _context = context;
        }

		public async Task<long> ThanhToanDonHangInsertOrUpdate(ThanhToanDonHangViewModel obj, int ID_TaiKhoan, string NguoiCapNhat)
		{
			try
			{
				var procedureName = "DonHang_ThanhToan_Insert_Or_Update";
				var parameters = new DynamicParameters();
				parameters.Add("ID_TaiKhoan", ID_TaiKhoan, DbType.Int32, ParameterDirection.Input);
				parameters.Add("ID_HinhThuc_ThanhToan", obj.ID_HinhThuc_ThanhToan, DbType.Int32, ParameterDirection.Input);
				parameters.Add("ID_KhuyenMai", obj.ID_KhuyenMai, DbType.Int32, ParameterDirection.Input);
				parameters.Add("NguoiNhan_HoTen", obj.NguoiNhan_HoTen, DbType.String, ParameterDirection.Input);
				parameters.Add("NguoiNhan_SoDienThoai", obj.NguoiNhan_SoDienThoai, DbType.String, ParameterDirection.Input);
				parameters.Add("TongTienDonHang", obj.TongTienDonHang, DbType.Decimal, ParameterDirection.Input);
				parameters.Add("IsThanhToan", obj.IsThanhToan, DbType.Boolean, ParameterDirection.Input);
				parameters.Add("SoNha", obj.SoNha, DbType.String, ParameterDirection.Input);
				parameters.Add("DiaChiNhanHang", obj.DiaChiNhanHang, DbType.String, ParameterDirection.Input);
				parameters.Add("ID_TinhThanh", obj.ID_TinhThanh, DbType.Int32, ParameterDirection.Input);
				parameters.Add("ID_QuanHuyen", obj.ID_QuanHuyen, DbType.Int32, ParameterDirection.Input);
				parameters.Add("ID_PhuongXa", obj.ID_PhuongXa, DbType.Int32, ParameterDirection.Input);
				parameters.Add("GhiChu", obj.GhiChu, DbType.String, ParameterDirection.Input);
				parameters.Add("NguoiCapNhat", NguoiCapNhat, DbType.String, ParameterDirection.Input);

				using (var connection = _context.CreateConnection())
				{
					connection.Open();
					long ID_ThanhToan = 0;
					using (var transaction = connection.BeginTransaction())
					{
						ID_ThanhToan = await connection.ExecuteScalarAsync<long>(procedureName, parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

						if (ID_ThanhToan > 0 && obj.listID_DonHang.Length > 0)
						{
							var donHangChiTiet = "ThanhToan_DonHang_ChiTiet_Insert_Or_Update";

							var parametersDonHangChiTiet = new DynamicParameters();
							parametersDonHangChiTiet.Add("ID_ThanhToan", ID_ThanhToan, DbType.Int64, ParameterDirection.Input);
							parametersDonHangChiTiet.Add("listID_DonHang", obj.listID_DonHang, DbType.String, ParameterDirection.Input);
							parametersDonHangChiTiet.Add("NguoiCapNhat", NguoiCapNhat, DbType.String, ParameterDirection.Input);

							int detail = connection.ExecuteScalar<int>(donHangChiTiet, parametersDonHangChiTiet, commandType: CommandType.StoredProcedure, transaction: transaction);
						}
						transaction.Commit();
					}

					return ID_ThanhToan;
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException("ThanhToanDonHangInsertOrUpdate", ex);
			}
		}
	}
}
