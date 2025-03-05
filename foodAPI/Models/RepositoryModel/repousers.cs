using foodAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

/// <summary>
/// users CRUD
/// </summary>
public class z_repousers : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// <summary>
    public IEFGenericRepository<users> repo;
    /// <summary>
    /// 建構子
    /// <summary>
    public z_repousers()
    {
        repo = new EFGenericRepository<users>(new dbEntities());
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<users> GetDapperDataList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText);
            str_query += GetSQLOrderBy();
            //DynamicParameters parm = new DynamicParameters();
            //parm.Add("parmName", "parmValue");
            var model = dp.ReadAll<users>(str_query);
            return model;
        }
    }
    /// <summary>
    /// 取得 SQL 欄位及表格名稱
    /// <summary>
    /// <returns></returns>
    private string GetSQLSelect()
    {
        string str_query = @"
SELECT 
userId, userName, password, phone, email
, role FROM users 
";
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 條件式
    /// <summary>
    /// <param name="searchText">查詢文字</param>
    /// <returns></returns>
    private string GetSQLWhere(string searchText)
    {
        string str_query = "";
        if (!string.IsNullOrEmpty(searchText))
        {
            str_query += " WHERE (";
            str_query += $"userName LIKE '%{searchText}%'  OR ";
            str_query += $"password LIKE '%{searchText}%'  OR ";
            str_query += $"phone LIKE '%{searchText}%'  OR ";
            str_query += $"email LIKE '%{searchText}%'  OR ";
            str_query += $"role LIKE '%{searchText}%'  ";
            str_query += ") ";
        }
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 排序
    /// <summary>
    /// <returns></returns>
    private string GetSQLOrderBy()
    {
        return " ORDER BY  userId";
    }
    /// <summary>
    /// 新增或修改
    /// <summary>
    /// <param name="model"></param>
    public void CreateEdit(users model)
    {
        repo.CreateEdit(model, model.userId);
    }
    /// <summary>
    /// 刪除
    /// <summary>
    /// <param name="id">Id</param>
    public void Delete(int id)
    {
        var model = repo.ReadSingle(m => m.userId == id);
        if (model != null) repo.Delete(model, true);
    }
    /// <summary>
    /// 檢查 Id 是否存在
    /// <summary>
    /// <param name="id">主鍵值</param>
    /// <returns></returns>
    public bool IdExists(int id)
    {
        var model = repo.ReadSingle(m => m.userId == id);
        return (model != null);
    }
	#endregion
	#region 自定義事件及函數

	/// <summary>
	/// 設定帳號部門及職稱
	/// </summary>
	public void SetUserInfo()
	{
		var model = repo.ReadSingle(m => m.email == UserService.UserEmail);
		if (model != null)
		{
					UserService.DeptNo = "";
					UserService.DeptName = "";
					UserService.TitleNo = "";
					UserService.TitleName = "";
			
		}
	}
	/// <summary>
	/// 帳號登入作業
	/// </summary>
	/// <param name="email">E-Mail</param>
	/// <param name="password">密碼</param>
	/// <returns></returns>
	public users Login(string email, string password)
	{
		users bln_value = null;
		UserService.Logout();
		////處理帳號密碼加密
		//if (AppService.EncryptionMode)
		//{
		//	using (CryptographyService cryp = new CryptographyService())
		//	{ password = cryp.SHA256Encode(password); }
		//	if (AppService.DebugMode)
		//	{
		//		var user = repo.ReadSingle(m => m.email == email);
		//		if (user != null && string.IsNullOrEmpty(user.password))
		//		{
		//			user.password = password;
		//			repo.Update(user);
		//			repo.SaveChanges();
		//		}
		//	}
		//}
		//檢查登入帳密正確性
		var data = repo.ReadSingle(m => m.email == email && m.password == password);
		if (data != null)
		{
			UserService.Login(data.email, data.userName, data.role);
			bln_value = data;
		}
		return bln_value;
	}
	/// <summary>
	/// 重設密碼
	/// </summary>
	/// <param name="email">email</param>
	public bool ResetPassword(string email)
	{
		bool bln_value = false;
		//檢查舊密碼正確性
		var data = repo.ReadSingle(m => m.email == email);
		if (data != null)
		{
			string str_password = "0000";
			////處理帳號密碼加密
			//if (AppService.EncryptionMode)
			//{
			//	using (CryptographyService cryp = new CryptographyService())
			//	{
			//		str_password = cryp.SHA256Encode(str_password);
			//	}
			//}
			//變更為新密碼
			using (DapperRepository dp = new DapperRepository())
			{
				dp.CommandText = "UPDATE users SET password =  @password  WHERE email = @email";
				dp.ParametersAdd("email", email, true);
				dp.ParametersAdd("password", str_password, false);
				dp.Execute();
				bln_value = true;
			}


			//data.Password = str_password;
			//repo.Update(data);
			//repo.SaveChanges();
			//bln_value = true;
		}
		return bln_value;
	}
	/// <summary>
	/// 變更密碼
	/// </summary>
	/// <param name="oldPassword">舊密碼</param>
	/// <param name="newPassword">新密碼</param>
	/// <returns></returns>
	public bool ChangePassword(string oldPassword, string newPassword)
	{
		bool bln_value = false;
		//處理帳號密碼加密
		if (AppService.EncryptionMode)
		{
			using (CryptographyService cryp = new CryptographyService())
			{
				oldPassword = cryp.SHA256Encode(oldPassword);
				newPassword = cryp.SHA256Encode(newPassword);
			}
		}
		//檢查舊密碼正確性
		var data = repo.ReadSingle(m =>
				m.email == UserService.UserEmail &&
				m.role == UserService.RoleNo &&
				m.password == oldPassword);
		if (data != null)
		{
			//變更為新密碼
			using (DapperRepository dp = new DapperRepository())
			{
				dp.CommandText = "UPDATE Users SET password = @Password WHERE role = @RoleNo AND email = @UserEmail";
				dp.ParametersAdd("RoleNo", UserService.RoleNo, true);
				dp.ParametersAdd("UserEmail", UserService.UserEmail, false);
				dp.ParametersAdd("Password", newPassword, false);
				dp.Execute();
				string str_message = dp.ErrorMessage;
			}
			//data.Password = newPassword;
			//repo.Update(data);
			//repo.SaveChanges();
			bln_value = true;
		}
		return bln_value;
	}

	#endregion
}
