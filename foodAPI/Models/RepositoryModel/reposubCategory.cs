using foodAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

/// <summary>
/// subCategory CRUD
/// </summary>
public class z_reposubCategory : BaseClass
{
    #region 建構子及 CRUD
    /// <summary>
    /// Repository 變數
    /// <summary>
    public IEFGenericRepository<subCategory> repo;
    /// <summary>
    /// 建構子
    /// <summary>
    public z_reposubCategory()
    {
        repo = new EFGenericRepository<subCategory>(new dbEntities());
    }
    /// <summary>
    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <param name="searchText">查詢條件</param>
    /// <returns></returns>
    public List<subCategory> GetDapperDataList(string searchText)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere(searchText);
            str_query += GetSQLOrderBy();
            //DynamicParameters parm = new DynamicParameters();
            //parm.Add("parmName", "parmValue");
            var model = dp.ReadAll<subCategory>(str_query);
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
subCatId, subCatName, isMulti FROM subCategory 
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
            str_query += $"subCatId LIKE '%{searchText}%'  OR ";
            str_query += $"subCatName LIKE '%{searchText}%'  OR ";
            str_query += $"isMulti LIKE '%{searchText}%'  ";
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
        return " ORDER BY  subCatId";
    }
    /// <summary>
    /// 新增或修改
    /// <summary>
    /// <param name="model"></param>
    public void CreateEdit(subCategory model)
    {
        repo.CreateEdit(model, model.subCatId);
    }
    /// <summary>
    /// 刪除
    /// <summary>
    /// <param name="id">Id</param>
    public void Delete(string id)
    {
        var model = repo.ReadSingle(m => m.subCatId == id);
        if (model != null) repo.Delete(model, true);
    }
    /// <summary>
    /// 檢查 Id 是否存在
    /// <summary>
    /// <param name="id">主鍵值</param>
    /// <returns></returns>
    public bool IdExists(string id)
    {
        var model = repo.ReadSingle(m => m.subCatId == id);
        return (model != null);
    }
    #endregion
    #region 自定義事件及函數
    #endregion
}
