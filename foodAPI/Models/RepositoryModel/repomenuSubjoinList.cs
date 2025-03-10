using foodAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

/// <summary>
/// menuSubjoinList CRUD
/// </summary>
public class z_repomenuSubjoinList : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// <summary>
    public IEFGenericRepository<menuSubjoinList> repo;
    /// <summary>
    /// 建構子
    /// <summary>
    public z_repomenuSubjoinList()
    {
        repo = new EFGenericRepository<menuSubjoinList>(new dbEntities());
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<menuSubjoinList> GetDapperDataList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText);
            str_query += GetSQLOrderBy();
            //DynamicParameters parm = new DynamicParameters();
            //parm.Add("parmName", "parmValue");
            var model = dp.ReadAll<menuSubjoinList>(str_query);
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
menuSubListId, menuId, subCatId FROM menuSubjoinList 
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
            str_query += $"subCatId LIKE '%{searchText}%'  ";
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
        return " ORDER BY  menuSubListId";
    }
    /// <summary>
    /// 新增或修改
    /// <summary>
    /// <param name="model"></param>
    public void CreateEdit(menuSubjoinList model)
    {
        repo.CreateEdit(model, model.menuSubListId);
    }
    /// <summary>
    /// 刪除
    /// <summary>
    /// <param name="id">Id</param>
    public void Delete(int id)
    {
        var model = repo.ReadSingle(m => m.menuSubListId == id);
        if (model != null) repo.Delete(model, true);
    }
    /// <summary>
    /// 檢查 Id 是否存在
    /// <summary>
    /// <param name="id">主鍵值</param>
    /// <returns></returns>
    public bool IdExists(int id)
    {
        var model = repo.ReadSingle(m => m.menuSubListId == id);
        return (model != null);
    }
	  #endregion
	  #region 自定義事件及函數

	/// <summary>
	/// 以 Dapper 來讀取資料集合
	/// <summary>
	/// <param name="catId">查詢條件</param>
	/// <returns></returns>
	public List<dmMenuSubList> GetMenuSubItems(string menuId)
	{
		using (DapperRepository dp = new DapperRepository())
		{
			string str_query = @"SELECT subCatId FROM menuSubjoinList ";
			str_query += " WHERE (menuId = '" + menuId + "')";
			str_query += GetSQLOrderBy();
			//DynamicParameters parm = new DynamicParameters();
			//parm.Add("parmName", "parmValue");
			var model = dp.ReadAll<dmMenuSubList>(str_query);
			return model;
		}
	}


	#endregion
}
