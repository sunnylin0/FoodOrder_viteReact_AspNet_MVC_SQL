using foodAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
/// detailSubjoin CRUD
/// </summary>
public class z_repodetailSubjoin : BaseClass
{
	#region 建構子及 CRUD
	/// <summary>
	/// Repository 變數
	/// <summary>
	public IEFGenericRepository<detailSubjoin> repo;
	/// <summary>
	/// 建構子
	/// <summary>
	public z_repodetailSubjoin()
	{
		repo = new EFGenericRepository<detailSubjoin>(new dbEntities());
	}
	/// <summary>
	/// 以 Dapper 來讀取資料集合
	/// <summary>
	/// <param name="searchText">查詢條件</param>
	/// <returns></returns>
	public List<detailSubjoin> GetDapperDataList(string searchText)
	{
		using (DapperRepository dp = new DapperRepository())
		{
			string str_query = GetSQLSelect();
			str_query += GetSQLWhere(searchText);
			str_query += GetSQLOrderBy();
			//DynamicParameters parm = new DynamicParameters();
			//parm.Add("parmName", "parmValue");
			var model = dp.ReadAll<detailSubjoin>(str_query);
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
dsId, detailId, subId FROM detailSubjoin 
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
		}
		return str_query;
	}
	/// <summary>
	/// 取得 SQL 排序
	/// <summary>
	/// <returns></returns>
	private string GetSQLOrderBy()
	{
		return " ORDER BY  dsId";
	}
	/// <summary>
	/// 新增或修改
	/// <summary>
	/// <param name="model"></param>
	public void CreateEdit(detailSubjoin model)
	{
		repo.CreateEdit(model, model.dsId);
	}
	/// <summary>
	/// 刪除
	/// <summary>
	/// <param name="id">Id</param>
	public void Delete(int id)
	{
		var model = repo.ReadSingle(m => m.dsId == id);
		if (model != null) repo.Delete(model, true);
	}
	/// <summary>
	/// 檢查 Id 是否存在
	/// <summary>
	/// <param name="id">主鍵值</param>
	/// <returns></returns>
	public bool IdExists(int id)
	{
		var model = repo.ReadSingle(m => m.dsId == id);
		return (model != null);
	}
	#endregion
	#region 自定義事件及函數
	/// <summary>
	/// 以 Dapper 來讀取資料集合
	/// </summary>
	/// <param name="detailId">detailId</param>
	/// <returns></returns>
	public List<subjoin> GetOrderSubJoin(int detailId)
	{

		using (DapperRepository dp = new DapperRepository())
		{
			string str_query = "SELECT " +
	" dsId, detailId, subId FROM detailSubjoin " +
	"WHERE (detailId = " + detailId + ")";

			var dsModel = dp.ReadAll<detailSubjoin>(str_query).ToList();
			List<subjoin> subjoinList = new List<subjoin>();
			using (z_reposubjoin subjoinObj = new z_reposubjoin())
			{
				if (dsModel != null)
				{
					foreach (var dsItem in dsModel)
					{
						var model = subjoinObj.repo.ReadSingle(m => m.subId == dsItem.subId);
						subjoinList.Add(model);
					}
				}
			}
			return subjoinList;
		}
	}


	/// <summary>
	/// 以 Dapper 插入資料
	/// <summary>
	/// <returns></returns>
	public bool AddThis(detailSubjoin ths)
	{
		bool bln_value = false;
		using (DapperRepository dp = new DapperRepository())
		{
			dp.CommandText = @"INSERT INTO [detailSubjoin] (detailId, subId) VALUES(@detailId,@subId)";
			dp.ParametersAdd("detailId", ths.detailId, true);
			dp.ParametersAdd("subId", ths.subId, false);
			var result = dp.Execute();
			return result > 0;
		}
	}

	//public bool AddList(List<detailSubjoin> detsubList)
	//{
	//	detsubList.ForEach(dd => {
	//		AddThis(dd);
	//	});
	//	return true;
	//}

	#endregion
}
