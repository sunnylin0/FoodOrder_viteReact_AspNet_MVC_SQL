using foodAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Runtime.CompilerServices;
using System.Web.Http.Results;

/// <summary>
/// order CRUD
/// </summary>
public class z_repoorder : BaseClass
{
	#region 建構子及 CRUD
	/// <summary>
	/// Repository 變數
	/// <summary>
	public IEFGenericRepository<order> repo;
	/// <summary>
	/// 建構子
	/// <summary>
	public z_repoorder()
	{
		repo = new EFGenericRepository<order>(new dbEntities());
	}
	/// <summary>
	/// 以 Dapper 來讀取資料集合
	/// <summary>
	/// <param name="searchText">查詢條件</param>
	/// <returns></returns>
	public List<order> GetDapperDataList(string searchText)
	{
		using (DapperRepository dp = new DapperRepository())
		{
			string str_query = GetSQLSelect();
			str_query += GetSQLWhere(searchText);
			str_query += GetSQLOrderBy();
			//DynamicParameters parm = new DynamicParameters();
			//parm.Add("parmName", "parmValue");
			var model = dp.ReadAll<order>(str_query);
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
orderId, userId, userName, totalPrice, dateTime
, takeAway, isDone, remark FROM order 
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
			str_query += $"userName LIKE '%{searchText}%'  OR ";
			str_query += $"takeAway LIKE '%{searchText}%'  OR ";
			str_query += $"isDone LIKE '%{searchText}%'  OR ";
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
	public void CreateEdit(order model)
	{
		repo.CreateEdit(model, model.orderId);
	}
	/// <summary>
	/// 刪除
	/// <summary>
	/// <param name="id">Id</param>
	public void Delete(string id)
	{
		var model = repo.ReadSingle(m => m.orderId == id);
		if (model != null) repo.Delete(model, true);
	}
	/// <summary>
	/// 檢查 Id 是否存在
	/// <summary>
	/// <param name="id">主鍵值</param>
	/// <returns></returns>
	public bool IdExists(string id)
	{
		var model = repo.ReadSingle(m => m.orderId == id);
		return (model != null);
	}
	#endregion
	#region 自定義事件及函數	

	/// <summary>
	/// 以 Dapper 來讀取資料集合
	/// <summary>
	/// <returns></returns>
	public List<order> GetOrderPage()
	{
		using (z_repoorder orderObj = new z_repoorder())
		{
			var orderModel = orderObj.repo.ReadAll().ToList();
			using (z_repodetail detailList = new z_repodetail())
			{
				if (orderModel != null)
				{
					foreach (var orderItem in orderModel)
					{
						var detailOrderItems = detailList.GetOrderPage(orderItem.orderId);
						orderItem.details = new List<detail>();
						foreach (var m in detailOrderItems)
						{
							orderItem.details.Add(m);
						}
					}
				}
			}
			return orderModel.ToList();
		}
	}

	/// <summary>
	/// 以 Dapper 來插入資料
	/// <summary>
	/// <returns>true 成功 / false </returns>
	public bool AddThis(order ths)
	{
		using (DapperRepository dp = new DapperRepository())
		{
			dp.CommandText = @"INSERT INTO [order] (orderId, userId, userName,  totalPrice, dateTime, takeAway, isDone, remark)
                             VALUES(@orderId, @userId, @userName,  @totalPrice, @dateTime, @takeAway, @isDone, @remark);";
			dp.ParametersAdd("orderId", ths.orderId, true);
			dp.ParametersAdd("userId", ths.userId, false);
			dp.ParametersAdd("userName", ths.userName, false);
			dp.ParametersAdd("totalPrice", ths.totalPrice, false);
			dp.ParametersAdd("dateTime", ths.dateTime, false);
			dp.ParametersAdd("takeAway", (ths.takeAway ==true)? true:false, false);
			dp.ParametersAdd("isDone", (ths.isDone == true) ? true : false, false);
			dp.ParametersAdd("remark", ths.remark, false);
			var result = dp.Execute();
			return result > 0;

		}
	}
	/// <summary>
	/// 取得指定使用者的歷史訂單 (包含明細)
	/// <summary>
	/// <param name="userId">使用者ID</param>
	/// <returns></returns>
	public List<order> GetOrdersByUserIdxx(string userId)
	{
		// 1. 取得指定 userId 的訂單主檔
		List<order> userOrders;
		using (DapperRepository dp = new DapperRepository())
		{
			// 使用參數化查詢避免 SQL Injection
			string str_query = @"
				SELECT *
				FROM [order] 
				WHERE userId = @userId
				"; // 按時間倒序排列

			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("userId", userId);

			userOrders = dp.ReadAll<order>(str_query, parameters).ToList();
		}

		// 2. 迭代訂單主檔，取得訂單明細 (與 GetOrderPage 邏輯相同)
		if (userOrders != null && userOrders.Any())
		{
			using (z_repodetail detailList = new z_repodetail())
			{
				foreach (var orderItem in userOrders)
				{
					// 假設 z_repodetail.GetOrderPage(orderId) 可以取得訂單明細
					var detailOrderItems = detailList.GetOrderPage(orderItem.orderId);

					// 初始化 details 列表並加入明細
					orderItem.details = new List<detail>();
					foreach (var m in detailOrderItems)
					{
						orderItem.details.Add(m);
					}
				}
			}
		}

		return userOrders ?? new List<order>();
	}



	/// <summary>
	/// 取得指定訂單主檔 (orderId) 的訂單明細 (detail)，並包含加購項 (subItems)
	/// </summary>
	/// <param name="orderId">訂單 ID</param>
	/// <returns></returns>
	private List<detail> GetOrderDetailListWithSubItems(string orderId)
	{
		List<detail> detailList = new List<detail>();

		// 1. 取得訂單明細 (detail)
		using (DapperRepository dp = new DapperRepository())
		{
			// 這是對應您 JS 程式碼中的 SQL 邏輯 (取得明細)
			string sql_detail = @"
				SELECT detail.detailId, detail.menuId, detail.menuName, detail.price, detail.subPrice, detail.qty, detail.remark
				FROM [order] INNER JOIN detail ON [order].orderId = detail.orderId
				WHERE detail.orderId = @orderId;";

			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("orderId", orderId);

			// 假設您有一個名為 detail 的 Model
			detailList = dp.ReadAll<detail>(sql_detail, parameters).ToList();
		}

		// 2. 對每個明細，取得加購項 (subItems)
		if (detailList != null && detailList.Any())
		{
			using (z_repodetail detailRepo = new z_repodetail())
			{
				foreach (var detailItem in detailList)
				{
					// 呼叫 z_repodetail 中實作的 GetOrderDetailSubjoinList (第一步實作的方法)
					var subjoinList = detailRepo.GetOrderDetailSubjoinList(detailItem.detailId.ToString());

					// 假設 detail Model 中有一個 List<subjoin> 屬性名為 subItems
					detailItem.subItems = subjoinList;
				}
			}
		}

		return detailList;
	}



	/// <summary>
	/// 取得指定使用者的歷史訂單 (包含明細及加購項)
	/// (對應 JS 的 getOrdersByUserId 邏輯)
	/// </summary>
	/// <param name="userId">使用者ID</param>
	/// <returns></returns>
	public List<order> GetOrdersByUserId(string userId)
	{
		List<order> userOrders;

		// 1. 取得指定 userId 的訂單主檔
		using (DapperRepository dp = new DapperRepository())
		{
			string str_query = @"
				SELECT *
				FROM [order] 
				WHERE userId = @userId
				ORDER BY dateTime DESC"; // 按時間倒序排列 (類似您 Node.js 程式碼中隱含的排序)

			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("userId", userId);

			userOrders = dp.ReadAll<order>(str_query, parameters).ToList();
		}

		// 2. 迭代訂單主檔，取得明細 (包含加購項)
		if (userOrders != null && userOrders.Any())
		{
			foreach (var orderItem in userOrders)
			{
				// 呼叫內部方法取得明細與加購項
				orderItem.details = GetOrderDetailListWithSubItems(orderItem.orderId);
			}
		}

		return userOrders ?? new List<order>();
	}






	#endregion
}
