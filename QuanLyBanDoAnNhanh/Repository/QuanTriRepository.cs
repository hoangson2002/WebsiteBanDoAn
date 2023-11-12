using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using QuanLyBanDoAnNhanh.Context;
using QuanLyBanDoAnNhanh.ExtendModels;
using QuanLyBanDoAnNhanh.ExtendModels.DanhMuc;
using QuanLyBanDoAnNhanh.RepoContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.Repository
{
	public class QuanTriRepository : IQuanTriRepository
	{
		private readonly DapperContext _context;
		private readonly IHostingEnvironment _env;
		private readonly string uploadpath = "/Files/Upload/MonAn_Images";
		public QuanTriRepository(DapperContext context, IHostingEnvironment env)
		{
			_context = context;
			_env = env;
		}

		private const string stringToSplit = "DECLARE @stringToSplit VARCHAR(MAX) = @ListID, @character varchar(10) = ','; " +
				"DECLARE @name NVARCHAR(255), @pos INT; " +
				"DECLARE @tempTable TABLE ([Name] [nvarchar] (500)); " +
				"WHILE CHARINDEX(@character, @stringToSplit) > 0 " +
				"BEGIN " +
				"SELECT @pos = CHARINDEX(@character, @stringToSplit); " +
				"SELECT @name = SUBSTRING(@stringToSplit, 1, @pos-1); " +
				"INSERT INTO @tempTable SELECT @name; " +
				"SELECT @stringToSplit = SUBSTRING(@stringToSplit, @pos+1, LEN(@stringToSplit)-@pos); " +
				"END; " +
				"INSERT INTO @tempTable SELECT @stringToSplit; ";

		#region Cấu hình banner
		public async Task<List<CauHinhBannerViewModel>> GetListBannerConfig()
		{
			try
			{
				var query = "SELECT ID_Banner, LienKet, NgayBatDau, NgayKetThuc, NoiDung, TieuDe, '' TrangThai, URL FROM dbo.CH_Banner WHERE Delete_Mark = 0";

				using (var connection = _context.CreateConnection())
				{
					var list = await connection.QueryAsync<CauHinhBannerViewModel>(query);

					return list.ToList();
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException("GetListBannerConfig", ex);
			}
		}

		public bool DeleteBannerConfig(string ListID)
		{
			try
			{
				var query = stringToSplit +
							"UPDATE dbo.CH_Banner SET Delete_Mark = 1 WHERE ID_Banner IN(SELECT [Name] FROM @tempTable); " +
							"SELECT 1 re;";
				var parameters = new DynamicParameters();
				parameters.Add("ListID", ListID, DbType.String, ParameterDirection.Input);

				using (var connection = _context.CreateConnection())
				{
					var result = connection.ExecuteScalar<bool>(query, parameters);

					return result;
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException("DeleteBannerConfig", ex);
			}
		}

		public int BannerConfigInsertOrUpdate(CauHinhBannerViewModel obj, string NguoiCapNhat)
		{
			try
			{
				var query = "IF @ID_Banner > 0               " +
			   "         BEGIN                                   " +
			   "             UPDATE dbo.CH_Banner                " +
			   "             SET URL = @URL,                     " +
				"	            NgayBatDau = @NgayBatDau,        " +
				"	            NgayKetThuc = @NgayKetThuc,      " +
				"	            LienKet = @LienKet,              " +
				"	            TieuDe = @TieuDe,                " +
				"	            NoiDung = @NoiDung,              " +
				"	            Modified_Date = GETDATE(),       " +
				"	            Modified_By = @NguoiCapNhat      " +
			   "                                                 " +
			   "             WHERE ID_Banner = @ID_Banner" +
			   "			SELECT ID_Banner AS ID_Banner        " +
			   "         END                                     " +
			   "     ELSE                                        " +
			   "                                                 " +
			   "     BEGIN                                       " +
			   "         INSERT INTO dbo.CH_Banner               " +
			   "		(                                        " +
			   "		URL,                                    " +
			   "		NgayBatDau,                             " +
			   "		NgayKetThuc,                            " +
			   "		LienKet,                                " +
			   "		TieuDe,                                 " +
			   "		NoiDung,                                " +
			   "		Create_Date,                            " +
			   "		Create_By,                              " +
			   "		Modified_Date,                          " +
			   "		Modified_By,                            " +
			   "		Delete_Mark                             " +
			   "		)                                       " +
			   "		VALUES                                  " +
			   "		(										" +
			   "		@URL, --URL - nvarchar(500)             " +
			   "		@NgayBatDau, --NgayBatDau - datetime    " +
			   "		@NgayKetThuc, --NgayKetThuc - datetime  " +
			   "		@LienKet, --LietKet - nvarchar(200)     " +
			   "		@TieuDe, --TieuDe - nvarchar(200)       " +
			   "		@NoiDung, --NoiDung - nvarchar(500)     " +
			   "		GETDATE(), --Create_Date - datetime     " +
			   "		@NguoiCapNhat, --Create_By - varchar(50)" +
			   "		GETDATE(), --Modified_Date - datetime   " +
			   "		'', --Modified_By - varchar(50)         " +
			   "		0-- Delete_Mark - bit                   " +
			   "		) " +
			   "		SELECT ID_Banner AS ID_Banner			" +
			   "     END";
				var parameters = new DynamicParameters();
				parameters.Add("ID_Banner", obj.ID_Banner, DbType.Int32, ParameterDirection.Input);
				parameters.Add("URL", obj.URL, DbType.String, ParameterDirection.Input);
				parameters.Add("NgayBatDau", obj.NgayBatDau, DbType.DateTime, ParameterDirection.Input);
				parameters.Add("NgayKetThuc", obj.NgayKetThuc, DbType.DateTime, ParameterDirection.Input);
				parameters.Add("TieuDe", obj.TieuDe, DbType.String, ParameterDirection.Input);
				parameters.Add("NoiDung", obj.NoiDung, DbType.String, ParameterDirection.Input);
				parameters.Add("LienKet", obj.LienKet, DbType.String, ParameterDirection.Input);
				parameters.Add("NguoiCapNhat", NguoiCapNhat, DbType.String, ParameterDirection.Input);

				using (var connection = _context.CreateConnection())
				{
					var result = connection.ExecuteScalar<int>(query, parameters);

					return result;
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException("GetListBannerConfig", ex);
			}
		}
		#endregion

		#region Danh mục phân loại đồ ăn
		public DataTable GetListMonAnPhanLoai()
		{
			try
			{
				var query = "SELECT ID_MonAnPhanLoai, ID_MonAnPhanLoai as ID, TenMonAnPhanLoai, TenMonAnPhanLoai as Value,  CAST(0 as BIT) AS active FROM dbo.DM_MonAn_PhanLoai WHERE Delete_Mark = 0";

				using (var connection = _context.CreateConnection())
				{
					var reader = connection.ExecuteReader(query);
					DataTable table = new DataTable();
					table.Load(reader);
					return table;
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException("GetListMonAnPhanLoai", ex);
			}
		}

		public bool DeletePhanLoaiMonAn(string ListID)
		{
			try
			{
				string query = stringToSplit +
							"UPDATE dbo.DM_MonAn_PhanLoai SET Delete_Mark = 1 WHERE ID_MonAnPhanLoai IN(SELECT [Name] FROM @tempTable); " +
							"SELECT 1 re;";
				var parameters = new DynamicParameters();
				parameters.Add("ListID", ListID, DbType.String, ParameterDirection.Input);

				using (var connection = _context.CreateConnection())
				{
					var result = connection.ExecuteScalar<bool>(query, parameters);

					return result;
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException("DeletePhanLoaiMonAn", ex);
			}
		}

		public int PhanLoaiMonAnInsertOrUpdate(int ID_MonAnPhanLoai, string TenMonAnPhanLoai, string NguoiCapNhat)
		{
			try
			{
				var query = "IF @ID_MonAnPhanLoai > 0               " +
			   "         BEGIN                                   " +
			   "             UPDATE dbo.DM_MonAn_PhanLoai                " +
			   "             SET TenMonAnPhanLoai = @TenMonAnPhanLoai,     " +
				"	            Modified_Date = GETDATE(),       " +
				"	            Modified_By = @NguoiCapNhat      " +
			   "             WHERE ID_MonAnPhanLoai = @ID_MonAnPhanLoai" +
			   "			SELECT @ID_MonAnPhanLoai AS ID_MonAnPhanLoai     " +
			   "         END                                     " +
			   "     ELSE                                        " +
			   "                                                 " +
			   "     BEGIN                                       " +
			   "         INSERT INTO dbo.DM_MonAn_PhanLoai               " +
			   "		(                                        " +
			   "		TenMonAnPhanLoai,                                    " +
			   "		Created_Date,                            " +
			   "		Created_By,                              " +
			   "		Modified_Date,                          " +
			   "		Modified_By,                            " +
			   "		Delete_Mark                             " +
			   "		)                                       " +
			   "		VALUES                                  " +
			   "		(										" +
			   "		@TenMonAnPhanLoai,						" +// - nvarchar(500)
			   "		GETDATE(),								" +//--Create_Date - datetime     
			   "		@NguoiCapNhat,							" +// --Create_By - varchar(50)
			   "		GETDATE(),								 " +// --Modified_Date - datetime  
			   "		@NguoiCapNhat,									     " +//--Modified_By - varchar(50)    
			   "		0						                 " +//-- Delete_Mark - bit  
			   "		) " +
			   "		SELECT 1 AS ID_MonAnPhanLoai			" +
			   "     END";
				var parameters = new DynamicParameters();
				parameters.Add("ID_MonAnPhanLoai", ID_MonAnPhanLoai, DbType.Int32, ParameterDirection.Input);
				parameters.Add("TenMonAnPhanLoai", TenMonAnPhanLoai, DbType.String, ParameterDirection.Input);
				//parameters.Add("HinhAnh", HinhAnh, DbType.String, ParameterDirection.Input);
				parameters.Add("NguoiCapNhat", NguoiCapNhat, DbType.String, ParameterDirection.Input);

				using (var connection = _context.CreateConnection())
				{
					var result = connection.ExecuteScalar<int>(query, parameters);

					return result;
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException("PhanLoaiMonAnInsertOrUpdate", ex);
			}
		}
		#endregion

		#region Danh mục nguyên liệu
		public DataTable GetListNguyenLieu()
		{
			try
			{
				var query = "SELECT * FROM dbo.DM_NguyenLieu WHERE Delete_Mark = 0";

				using (var connection = _context.CreateConnection())
				{
					var reader = connection.ExecuteReader(query);
					DataTable table = new DataTable();
					table.Load(reader);
					return table;
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException("GetListNguyenLieu", ex);
			}
		}

		public bool DeleteNguyenLieu(string ListID)
		{
			try
			{
				string query = stringToSplit +
							"UPDATE dbo.DM_NguyenLieu SET Delete_Mark = 1 WHERE ID_NguyenLieu IN(SELECT [Name] FROM @tempTable); " +
							"SELECT 1 re;";
				var parameters = new DynamicParameters();
				parameters.Add("ListID", ListID, DbType.String, ParameterDirection.Input);

				using (var connection = _context.CreateConnection())
				{
					var result = connection.ExecuteScalar<bool>(query, parameters);

					return result;
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException("DeleteNguyenLieu", ex);
			}
		}

		public int NguyenLieuAnInsertOrUpdate(int ID_NguyenLieu, string TenNguyenLieu, string NguoiCapNhat)
		{
			try
			{
				var procedureName = "DM_NguyenLieu_Insert_Or_Update";
				var parameters = new DynamicParameters();
				parameters.Add("ID_NguyenLieu", ID_NguyenLieu, DbType.Int32, ParameterDirection.Input);
				parameters.Add("TenNguyenLieu", TenNguyenLieu, DbType.String, ParameterDirection.Input);
				//parameters.Add("HinhAnh", HinhAnh, DbType.String, ParameterDirection.Input);
				parameters.Add("NguoiCapNhat", NguoiCapNhat, DbType.String, ParameterDirection.Input);

				using (var connection = _context.CreateConnection())
				{
					var result = connection.ExecuteScalar<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);

					return result;
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException("DM_NguyenLieu_Insert_Or_Update", ex);
			}
		}
		#endregion

		#region Danh mục món ăn
		public DataTable GetListMonAn(int ID_MonAnTrangThai, int ID_MonAnPhanLoai, string TenMonAn)
		{
			try
			{
				var procedureName = "DM_MonAn_GetList";

				var parameters = new DynamicParameters();
				parameters.Add("ID_MonAnTrangThai", ID_MonAnTrangThai, DbType.Int32, ParameterDirection.Input);
				parameters.Add("ID_MonAnPhanLoai", ID_MonAnPhanLoai, DbType.Int32, ParameterDirection.Input);
				parameters.Add("TenMonAn", TenMonAn, DbType.String, ParameterDirection.Input);

				using (var connection = _context.CreateConnection())
				{
					var reader = connection.ExecuteReader(procedureName, parameters, commandType: CommandType.StoredProcedure);
					DataTable table = new DataTable();
					table.Load(reader);
					return table;
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException("GetListMonAn", ex);
			}
		}

		public int MonAnInSertOrUpdate(MonAnViewModel obj, string NguoiCapNhat)
		{
			try
			{
				var procedureName = "DM_MonAn_Insert_Or_Update";
				var parameters = new DynamicParameters();
				parameters.Add("ID_MonAn", obj.ID_MonAn, DbType.Int32, ParameterDirection.Input);
				parameters.Add("ID_MonAnPhanLoai", obj.ID_MonAnPhanLoai, DbType.Int32, ParameterDirection.Input);
				parameters.Add("ID_MonAnTrangThai", obj.ID_MonAnTrangThai, DbType.Int32, ParameterDirection.Input);
				parameters.Add("TenMonAn", obj.TenMonAn, DbType.String, ParameterDirection.Input);
				parameters.Add("MaGiamGia", obj.MaGiamGia, DbType.String, ParameterDirection.Input);
				parameters.Add("DonGia", obj.DonGia, DbType.Decimal, ParameterDirection.Input);
				parameters.Add("MoTa", obj.MoTa, DbType.String, ParameterDirection.Input);
				parameters.Add("NguoiCapNhat", NguoiCapNhat, DbType.String, ParameterDirection.Input);

				using (var connection = _context.CreateConnection())
				{
					int ID_MonAn = 0;
					connection.Open();
					using (var transaction = connection.BeginTransaction())
					{
						ID_MonAn = connection.ExecuteScalar<int>(procedureName, parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

						if (ID_MonAn > 0)
						{
							if (obj.ListXoaNguyenLieu.Length > 0)
							{
								var procedureNameDeleteNguyenLieu = "DM_MonAn_NguyenLieu_Delete";
								var parametersNguyenLieuDelete = new DynamicParameters();
								parametersNguyenLieuDelete.Add("ListID", obj.ListXoaNguyenLieu, DbType.String, ParameterDirection.Input);
								parametersNguyenLieuDelete.Add("NguoiCapNhat", NguoiCapNhat, DbType.String, ParameterDirection.Input);

								var result = connection.ExecuteScalar<int>(procedureNameDeleteNguyenLieu, parametersNguyenLieuDelete, commandType: CommandType.StoredProcedure, transaction: transaction);
							}

							if(obj.listNguyenLieu.Count > 0)
							{
								var procedureNameInsertNguyenLieu = "DM_MonAn_NguyenLieu_Insert_Or_Update";

								foreach (NguyenLieuMonAnViewModel objNguyenLieu in obj.listNguyenLieu)
								{
									var parametersNguyenLieu = new DynamicParameters();
									parametersNguyenLieu.Add("ID", objNguyenLieu.ID, DbType.Int32, ParameterDirection.Input);
									parametersNguyenLieu.Add("ID_MonAn", ID_MonAn, DbType.Int32, ParameterDirection.Input);
									parametersNguyenLieu.Add("ID_NguyenLieu", objNguyenLieu.ID_NguyenLieu, DbType.Int32, ParameterDirection.Input);
									parametersNguyenLieu.Add("NguoiCapNhat", NguoiCapNhat, DbType.String, ParameterDirection.Input);

									int detail = connection.ExecuteScalar<int>(procedureNameInsertNguyenLieu, parametersNguyenLieu, commandType: CommandType.StoredProcedure, transaction: transaction);
								}
								
							}

						}
						transaction.Commit();
					}

					return ID_MonAn;
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException("MonAnInSertOrUpdate", ex);
			}
		}

		public bool DeleteMonAn(string ListID)
		{
			try
			{
				var procedureName = "DM_MonAn_Delete";
				var parameters = new DynamicParameters();
				parameters.Add("ListID", ListID, DbType.String, ParameterDirection.Input);

				using (var connection = _context.CreateConnection())
				{
					var result = connection.ExecuteScalar<bool>(procedureName, parameters, commandType: CommandType.StoredProcedure);

					return result;
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException("DeleteMonAn", ex);
			}
		}

		public DataTable GetListNguyenLieuMonAn(int ID_MonAn)
		{
			try
			{
				var procedureName = "DM_MonAn_NguyenLieu_Get_By_ID_MonAn";
				var parameters = new DynamicParameters();
				parameters.Add("ID_MonAn", ID_MonAn, DbType.Int32, ParameterDirection.Input);

				using (var connection = _context.CreateConnection())
				{
					var reader = connection.ExecuteReader(procedureName, parameters, commandType: CommandType.StoredProcedure);
					DataTable table = new DataTable();
					table.Load(reader);
					return table;
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException("GetListNguyenLieuMonAn", ex);
			}
		}
		#endregion
		#region Combobox
		public async Task<List<ComboboxViewModel>> ComboboxPhanLoaiMonAn()
		{
			try
			{
				var query = "SELECT ID_MonAnPhanLoai as ID, TenMonAnPhanLoai as Value FROM dbo.DM_MonAn_PhanLoai WHERE Delete_Mark = 0";

				using (var connection = _context.CreateConnection())
				{
					var result = await connection.QueryAsync<ComboboxViewModel>(query);
					
					return result.ToList();
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException("ComboboxPhanLoaiMonAn", ex);
			}
		}
		public async Task<List<ComboboxViewModel>> ComboboxTrangThaiMonAn()
		{
			try
			{
				var query = "SELECT ID_MonAnTrangThai as ID, TenTrangThai as Value FROM dbo.DM_MonAn_TrangThai WHERE Delete_Mark = 0";

				using (var connection = _context.CreateConnection())
				{
					var result = await connection.QueryAsync<ComboboxViewModel>(query);

					return result.ToList();
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException("GetListMonAnPhanLoai", ex);
			}
		}

		public async Task<List<ComboboxViewModel>> ComboboxNguyenLieu()
		{
			try
			{
				var query = "SELECT ID_NguyenLieu as ID, TenNguyenLieu as Value FROM dbo.DM_NguyenLieu WHERE Delete_Mark = 0";

				using (var connection = _context.CreateConnection())
				{
					var result = await connection.QueryAsync<ComboboxViewModel>(query);

					return result.ToList();
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException("ComboboxNguyenLieu", ex);
			}
		}
		#endregion

		#region save file
		public async Task<HoSoAttachmentViewModel> UploadFile(IFormFileCollection file, int ID_MonAn)
		{
			try
			{
				HoSoAttachmentViewModel obj = new HoSoAttachmentViewModel();

				foreach (var file_upload in file)
				{
					string fileName = file_upload.FileName;
					string ticks = DateTime.Now.Ticks.ToString();
					fileName = fileName.Trim();
					fileName = fileName.Replace(" ", "_");
					string extention = fileName.Substring(fileName.LastIndexOf('.') + 1);
					fileName = fileName.Substring(0, fileName.LastIndexOf('.')).Replace('.', '_').ToLower();
					string path_date = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/";
					string folder_upload = _env.ContentRootPath + uploadpath + "/" + path_date;

					if (!Directory.Exists(folder_upload))
						Directory.CreateDirectory(folder_upload);

					string path_file_upload = folder_upload + @"\" + fileName + ticks + "." + extention;
					using (var fileStream = new FileStream(path_file_upload, FileMode.Create))
					{
						await file_upload.CopyToAsync(fileStream);
					};

					HoSoAttachmentViewModel attachment = new HoSoAttachmentViewModel();
					attachment.FileName = fileName + ticks + "." + extention;
					attachment.FilePath = "MonAn_Images/" + path_date + fileName + ticks + "." + extention;
					attachment.ID_MonAn = ID_MonAn;
					obj = attachment;
				}

				return obj;
			}
			catch (Exception ex)
			{
				throw new ArgumentException("UploadFile", ex);
			}
		}

		public async Task<int> luuThongTinHinhAnhMonAn(string url, int ID_MonAn, string NguoiCapNhat)
		{
			try
			{
				var procedureName = "DM_MonAn_Update_File_Path";

				var parameters = new DynamicParameters();
				parameters.Add("ID_MonAn", ID_MonAn, DbType.Int32, ParameterDirection.Input);
				parameters.Add("url", url, DbType.String, ParameterDirection.Input);

				using (var connection = _context.CreateConnection())
				{
					int result = await connection.ExecuteScalarAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
					return result;
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException("luuThongTinHinhAnhMonAn", ex);
			}
		}
		#endregion

		#region Tìm phiếu giảm giá
		public async Task<PhieuGiamGiaViewModel> TimPhieuGiamGia(string MaKhuyenMai)
		{
			var procedureName = "DM_KhuyenMai_Get_By_MaKhuyenMai";
			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("MaKhuyenMai", MaKhuyenMai, DbType.String, ParameterDirection.Input);

				using (var connection = _context.CreateConnection())
				{
					var result = await connection.QueryFirstOrDefaultAsync<PhieuGiamGiaViewModel>
						(procedureName, parameters, commandType: CommandType.StoredProcedure);
					return result;
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException(procedureName, ex);
			}
		}
		#endregion
	}
}
