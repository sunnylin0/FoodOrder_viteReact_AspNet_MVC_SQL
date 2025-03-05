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

	#endregion
}
