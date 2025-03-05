using foodAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

/// <summary>
/// menu CRUD
/// </summary>
public class z_repomenu : BaseClass
{
	#region 建構子及 CRUD
	/// <summary>
	/// Repository 變數
	/// <summary>
	public IEFGenericRepository<menu> repo;
	/// <summary>
	/// 建構子
	/// <summary>
	public z_repomenu()
	{
		repo = new EFGenericRepository<menu>(new dbEntities());
	}
	/// <summary>
	/// 以 Dapper 來讀取資料集合
	/// <summary>
	/// <param name="searchText">查詢條件</param>
	/// <returns></returns>
	public List<menu> GetDapperDataList(string searchText)
	{
		using (DapperRepository dp = new DapperRepository())
		{
			string str_query = GetSQLSelect();
			str_query += GetSQLWhere(searchText);
			str_query += GetSQLOrderBy();
			//DynamicParameters parm = new DynamicParameters();
			//parm.Add("parmName", "parmValue");
			var model = dp.ReadAll<menu>(str_query);
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
menuId, catId, menuNameEn, menuName, comment
, price, img, isSoldOut FROM menu 
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
			str_query += $"menuId LIKE '%{searchText}%'  OR ";
			str_query += $"catId LIKE '%{searchText}%'  OR ";
			str_query += $"menuNameEn LIKE '%{searchText}%'  OR ";
			str_query += $"menuName LIKE '%{searchText}%'  OR ";
			str_query += $"comment LIKE '%{searchText}%'  OR ";
			str_query += $"img LIKE '%{searchText}%'  OR ";
			str_query += $"isSoldOut LIKE '%{searchText}%'  ";
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
		return " ORDER BY  menuId";
	}
	/// <summary>
	/// 新增或修改
	/// <summary>
	/// <param name="model"></param>
	public void CreateEdit(menu model)
	{
		repo.CreateEdit(model, model.menuId);
	}
	/// <summary>
	/// 刪除
	/// <summary>
	/// <param name="id">Id</param>
	public void Delete(string id)
	{
		var model = repo.ReadSingle(m => m.menuId == id);
		if (model != null) repo.Delete(model, true);
	}
	/// <summary>
	/// 取得名稱
	/// <summary>
	/// <param name="dataNo">編號</param>
	/// <returns></returns>
	public string GetDataName(string dataNo)
	{
		string str_value = "";
		var model = repo.ReadSingle(m => m.menuId == dataNo);
		if (model != null) str_value = model.menuName;
		return str_value;
	}
	/// <summary>
	/// 檢查 Id 是否存在
	/// <summary>
	/// <param name="id">主鍵值</param>
	/// <returns></returns>
	public bool IdExists(string id)
	{
		var model = repo.ReadSingle(m => m.menuId == id);
		return (model != null);
	}
	#endregion
	#region 自定義事件及函數

	/// <summary>
	/// 以 Dapper 來讀取資料集合
	/// <summary>
	/// <param name="catId">查詢條件</param>
	/// <returns></returns>
	public List<menu> GetMenuPage(string catId)
	{
		using (z_repomenu menuObj = new z_repomenu())
		{
			var menusModel = menuObj.repo.ReadAll().ToList();
			using (z_repomenuSubjoinList menuSubJoinList = new z_repomenuSubjoinList())
			{
				if (menusModel != null)
				{
					foreach (var menuItem in menusModel)
					{
						var menuSubItems = menuSubJoinList.GetMenuSubItems(menuItem.menuId);
						menuItem.subjoinIds = new List<string>();
						foreach (var m in menuSubItems)
						{
							menuItem.subjoinIds.Add(m.subCatId);
						}
					}
				}
			}
			return menusModel.Where(m => m.catId == catId).ToList();
		}
	}

	#endregion
}
