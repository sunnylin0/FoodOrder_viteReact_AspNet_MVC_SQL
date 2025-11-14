using foodAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Diagnostics;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
/// detail CRUD
/// </summary>
public class z_repodetail : BaseClass
{
	#region 建構子及 CRUD
	/// <summary>
	/// Repository 變數
	/// <summary>
	public IEFGenericRepository<detail> repo;
	/// <summary>
	/// 建構子
	/// <summary>
	public z_repodetail()
	{
		repo = new EFGenericRepository<detail>(new dbEntities());
	}
	/// <summary>
	/// 以 Dapper 來讀取資料集合
	/// <summary>
	/// <param name="searchText">查詢條件</param>
	/// <returns></returns>
	public List<detail> GetDapperDataList(string searchText)
	{
		using (DapperRepository dp = new DapperRepository())
		{
			string str_query = GetSQLSelect();
			str_query += GetSQLWhere(searchText);
			str_query += GetSQLOrderBy();
			//DynamicParameters parm = new DynamicParameters();
			//parm.Add("parmName", "parmValue");
			var model = dp.ReadAll<detail>(str_query);
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
detailId, orderId, menuId, menuName, price
, subPrice, qty, remark FROM detail 
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
			str_query += $"orderId LIKE '%{searchText}%'  OR ";
			str_query += $"menuId LIKE '%{searchText}%'  OR ";
			str_query += $"menuName LIKE '%{searchText}%'  OR ";
			str_query += $"remark LIKE '%{searchText}%'  ";
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
		return " ORDER BY  orderId";
	}
	/// <summary>
	/// 新增或修改
	/// <summary>
	/// <param name="model"></param>
	public void CreateEdit(detail model)
	{
		repo.CreateEdit(model, model.detailId);
	}
	/// <summary>
	/// 刪除
	/// <summary>
	/// <param name="id">Id</param>
	public void Delete(int id)
	{
		var model = repo.ReadSingle(m => m.detailId == id);
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
	public bool IdExists(int id)
	{
		var model = repo.ReadSingle(m => m.detailId == id);
		return (model != null);
	}
	#endregion
	#region 自定義事件及函數
	/// <summary>
	/// 以 Dapper 來讀取資料集合
	/// <summary>
	/// <param name="catId">查詢條件</param>
	/// <returns></returns>
	public List<detail> GetOrderPage(string orderId)
	{
		//using (z_repodetail detailObj = new z_repodetail())
		using (DapperRepository dp = new DapperRepository())
		{
			string str_query = "select * from detail where (orderId ='" + orderId + "')";
			var detailsModel = dp.ReadAll<detail>(str_query).ToList();
			using (z_repodetailSubjoin detailSubJoinList = new z_repodetailSubjoin())
			{
				if (detailsModel != null)
				{
					foreach (var detailItem in detailsModel)
					{
						var orderSubJoinItems = detailSubJoinList.GetOrderSubJoin(detailItem.detailId);
						detailItem.subItems = new List<subjoin>();
						foreach (var m in orderSubJoinItems)
						{
							detailItem.subItems.Add(m);
						}
					}
				}
			}
			return detailsModel.ToList();
		}
	}

	/// <summary>
	/// 以 Dapper 插入資料
	/// <summary>
	/// <returns></returns>
	public bool AddThis(detail ths)
	{
		bool bln_value = false;

		using (DapperRepository dp = new DapperRepository())
		{
			dp.CommandText = @"INSERT INTO [detail] (orderId, menuId, menuName, price, subPrice, qty, remark) 
                             VALUES(@orderId, @menuId, @menuName, @price, @subPrice, @qty, @remark);
												SELECT CAST(SCOPE_IDENTITY() AS INT); ";  //重要取得自動生成的 detailId
			dp.ParametersAdd("orderId", ths.orderId, true);
			dp.ParametersAdd("menuId", ths.menuId, false);
			dp.ParametersAdd("menuName", ths.menuName, false);
			dp.ParametersAdd("price", ths.price, false);
			dp.ParametersAdd("subPrice", ths.subPrice, false);
			dp.ParametersAdd("qty", ths.qty, false);
			dp.ParametersAdd("remark", ths.remark, false);
			//dp.Execute();
			int generatedDetailId = dp.ReadSingle<int>();

			using (z_repodetailSubjoin detailSubItem = new z_repodetailSubjoin())
			{
				foreach (int subjoinId in ths.subjoinIdList)
				{
					detailSubItem.AddThis(new detailSubjoin { detailId = generatedDetailId, subId = subjoinId });
				}
			}
			return false;
		}
	}

	public bool AddList(List<detail> detailList)
	{
		using (z_repodetailSubjoin detailSubItem = new z_repodetailSubjoin())
		{

			detailList.ForEach(dd =>
			{
				AddThis(dd);
			});
		}
		return true;
	}


	/// <summary>
	/// 取得指定訂單明細 (detailId) 的加購項 (subjoin)
	/// </summary>
	/// <param name="detailId">訂單明細 ID</param>
	/// <returns></returns>
	public List<subjoin> GetOrderDetailSubjoinList(string detailId)
	{
		using (DapperRepository dp = new DapperRepository())
		{
			// 這是對應您 JS 程式碼中的 SQL 邏輯
			string str_query = @"
            SELECT T1.subId, T1.subCatId, T1.subName, T1.subPrice
            FROM subjoin AS T1 
            INNER JOIN detailSubjoin AS T2 ON T1.subId = T2.subId
            WHERE T2.detailId = @detailId";

			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("detailId", detailId);

			// 假設您有一個名為 subjoin 的 Model
			var subjoinList = dp.ReadAll<subjoin>(str_query, parameters).ToList();

			return subjoinList;
		}
	}

	#endregion


}