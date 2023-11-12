using Dapper;
using QuanLyBanDoAnNhanh.Context;
using QuanLyBanDoAnNhanh.RepoContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.Repository
{
	public class TrangChuRepository : ITrangChuRepository
	{
		private readonly DapperContext _context;
		public TrangChuRepository(DapperContext context)
		{
			_context = context;
		}
		#region Tìm kiếm đồ ăn
		public DataTable TimKiemDoAn(int ID_MonAnPhanLoai, string TenMonAn, int SapXep)
		{
			try
			{
				var procedureName = "TrangChu_GetList_MonAn";

				using (var connection = _context.CreateConnection())
				{
					var parameters = new DynamicParameters();
					parameters.Add("ID_MonAnPhanLoai", ID_MonAnPhanLoai, DbType.Int32, ParameterDirection.Input);
					parameters.Add("TenMonAn", TenMonAn, DbType.String, ParameterDirection.Input);
					parameters.Add("SapXep", SapXep, DbType.Int32, ParameterDirection.Input);
					var reader = connection.ExecuteReader(procedureName, parameters, commandType: CommandType.StoredProcedure);
					DataTable table = new DataTable();
					table.Load(reader);
					return table;
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException("TimKiemDoAn", ex);
			}
		}

		public DataTable GetDoAnByID(int ID_MonAn)
		{
			try
			{
				var procedureName = "DM_MonAn_Get_By_ID";

				using (var connection = _context.CreateConnection())
				{
					var parameters = new DynamicParameters();
					parameters.Add("ID_MonAn", ID_MonAn, DbType.Int32, ParameterDirection.Input);
					var reader = connection.ExecuteReader(procedureName, parameters, commandType: CommandType.StoredProcedure);
					DataTable table = new DataTable();
					table.Load(reader);
					return table;
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException("TimKiemDoAn", ex);
			}
		}
		#endregion

		#region Đơn hàng được mua nhiều nhất
		public DataTable MonAnDuocMuaNhieuNhat()
		{
			try
			{
				var procedureName = "DonHang_MuaNhieuNhat";

				using (var connection = _context.CreateConnection())
				{
					var parameters = new DynamicParameters();
					var reader = connection.ExecuteReader(procedureName, parameters, commandType: CommandType.StoredProcedure);
					DataTable table = new DataTable();
					table.Load(reader);
					return table;
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException("TimKiemDoAn", ex);
			}
		}

		#endregion
	}
}
