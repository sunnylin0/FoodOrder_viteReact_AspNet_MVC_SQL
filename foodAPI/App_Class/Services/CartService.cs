using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using foodAPI.Models;

public static class CartService
{
	#region 公開屬性
	/// <summary>
	/// 訂單編號
	/// </summary>
	public static string OrderNo { get; set; }

	/// <summary>
	/// 購物批號 LotNo
	/// </summary>
	public static string LotNo
	{
		get { return GetLotNo(); }
		set { HttpContext.Current.Session["CartLotNo"] = value; }
	}
	/// <summary>
	/// 購物批號建立時間
	/// </summary>
	public static DateTime LotCreateTime
	{
		get { return GetLotCreateTime(); }
		set { HttpContext.Current.Session["CartCreateTime"] = value; }
	}
	/// <summary>
	/// 購物車筆數
	/// </summary>
	public static int Counts { get { return GetCartCount(); } }

	/// <summary>
	/// 購物車合計
	/// </summary>
	public static int Totals { get { return GetCartTotals(); } }
	#endregion
	#region 公用函數
	/// <summary>
	/// 更新購物批號
	/// </summary>
	/// <returns></returns>
	public static string NewLotNo()
	{
		string str_lot_no = "";
		if (!UserService.IsLogin)
			str_lot_no = Guid.NewGuid().ToString().Substring(0, 15).ToUpper();
		LotNo = str_lot_no;
		LotCreateTime = DateTime.Now;
		return str_lot_no;
	}
	#endregion
	#region 公用事件
	/// <summary>
	/// 登入時將現有遊客的購物車加入客戶的購物車
	/// </summary>
	public static void LoginCart()
	{
		if (!string.IsNullOrEmpty(LotNo))
		{
			int int_qty = 0;
			using (z_repoCarts carts = new z_repoCarts())
			{
				var datas = carts.repo.ReadAll(m => m.LotNo == LotNo);
				if (datas != null)
				{
					foreach (var item in datas)
					{
						int_qty = item.OrderQty;
						AddCart(item.ProdNo, item.ProdSpec, int_qty);
						carts.repo.Delete(item);
					}
					carts.repo.SaveChanges();
				}
			}
			NewLotNo();
		}
	}
	/// <summary>
	/// 加入購物車
	/// </summary>
	/// <param name="productNo">商品編號</param>
	public static void AddCart(string productNo)
	{
		AddCart(productNo, "", 1);
	}

	/// <summary>
	/// 加入購物車
	/// </summary>
	/// <param name="productNo">商品編號</param>
	/// <param name="buyQty">數量</param>
	public static void AddCart(string productNo, int buyQty)
	{
		AddCart(productNo, "", buyQty);
	}

	/// <summary>
	/// 加入購物車
	/// </summary>
	/// <param name="productNo">商品編號</param>
	/// <param name="prod_Spec">商品規格</param>
	/// <param name="buyQty">數量</param>
	public static void AddCart(string productNo, string prod_Spec, int buyQty)
	{
		using (z_repoCarts carts = new z_repoCarts())
		{
			carts.AddCart(productNo, prod_Spec, buyQty);
		}
	}

	/// <summary>
	/// 更新購物車
	/// </summary>
	/// <param name="rowID">row ID</param>
	/// <param name="qty">數量</param>
	public static void UpdateCart(int rowID, int qty)
	{
		using (z_repoCarts carts = new z_repoCarts())
		{
			carts.UpdateCart(rowID, qty);
		}
	}

	/// <summary>
	/// 刪除購物車
	/// </summary>
	/// <param name="rowID">row ID</param>
	public static void DeleteCart(int rowID)
	{
		using (z_repoCarts carts = new z_repoCarts())
		{
			carts.DeleteCart(rowID);
		}
	}

	/// <summary>
	/// 消費者付款
	/// </summary>
	public static int CartPayment(vmOrders model)
	{
		int int_order_id = 0;
		OrderNo = CreateNewOrderNo(model);
		using (z_repoCarts carts = new z_repoCarts())
		{
			using (z_repoOrders orders = new z_repoOrders())
			{
				using (z_repoOrderDetails ordersDetail = new z_repoOrderDetails())
				{
					using (z_repoProducts prod = new z_repoProducts())
					{
						using (z_repoCategorys cate = new z_repoCategorys())
						{
							var datas = carts.repo.ReadAll(m => m.MemberNo == UserService.UserNo);
							if (datas != null)
							{
								int int_amount = datas.Sum(m => m.OrderAmount);
								decimal dec_tax = 0;
								if (int_amount > 0)
								{
									dec_tax = Math.Round((decimal)(int_amount * 5 / 100), 0);
								}
								int int_total = int_amount + (int)dec_tax;

								var data = orders.repo.ReadSingle(m => m.SheetNo == OrderNo);
								if (data != null)
								{
									data.OrderAmount = int_amount;
									data.TaxAmount = (int)dec_tax;
									data.TotalAmount = int_total;

									orders.repo.Update(data);
									orders.repo.SaveChanges();
								}

								foreach (var item in datas)
								{
									OrderDetails detail = new OrderDetails();
									detail.ParentNo = OrderNo;
									detail.ProdNo = item.ProdNo;
									detail.ProdName = item.ProdName;
									detail.VendorNo = "";
									detail.CategoryName = "";
									detail.ProdSpec = item.ProdSpec;
									detail.OrderQty = item.OrderQty;
									detail.OrderPrice = item.OrderPrice;
									detail.OrderAmount = item.OrderAmount;
									detail.Remark = "";

									ordersDetail.repo.Create(detail);
									ordersDetail.repo.SaveChanges();
								}
							}
						}
					}
				}
			}
		}
		return int_order_id;
	}
	/// <summary>
	/// 清除購物車
	/// </summary>
	public static void ClearCart()
	{
		using (z_repoCarts carts = new z_repoCarts())
		{
			carts.ClearCart();
		}
	}

	#endregion
	#region 私有函數
	/// <summary>
	/// 取得購物批號建立時間
	/// </summary>
	/// <returns></returns>
	private static DateTime GetLotCreateTime()
	{
		object obj_time = HttpContext.Current.Session["CartCreateTime"];
		return (obj_time == null) ? DateTime.Now : DateTime.Parse(obj_time.ToString());
	}

	/// <summary>
	/// 取得購物批號
	/// </summary>
	/// <returns></returns>
	private static string GetLotNo()
	{
		return (HttpContext.Current.Session["CartLotNo"] == null) ? NewLotNo() : HttpContext.Current.Session["CartLotNo"].ToString();
	}

	/// <summary>
	/// 取得目前購物車筆數
	/// </summary>
	/// <returns></returns>
	private static int GetCartCount()
	{
		int int_count = 0;
		using (z_repoCarts carts = new z_repoCarts())
		{
			if (UserService.IsLogin)
			{
				var data1 = carts.repo.ReadAll(m => m.MemberNo == UserService.UserNo);
				if (data1 != null) int_count = data1.Count();
			}
			else
			{
				var data2 = carts.repo.ReadAll(m => m.LotNo == LotNo);
				if (data2 != null) int_count = data2.Count();
			}
		}
		return int_count;
	}

	private static int GetCartTotals()
	{
		int? int_totals = 0;
		using (z_repoCarts carts = new z_repoCarts())
		{
			if (UserService.IsLogin)
			{
				var data1 = carts.repo.ReadAll(m => m.MemberNo == UserService.UserNo);
				if (data1 != null) int_totals = data1.Sum(m => m.OrderAmount);
			}
			else
			{
				var data2 = carts.repo.ReadAll(m => m.LotNo == LotNo);
				if (data2 != null) int_totals = data2.Sum(m => m.OrderAmount);
			}
		}
		if (int_totals == null) int_totals = 0;
		return int_totals.GetValueOrDefault();
	}

	private static string CreateNewOrderNo(vmOrders model)
	{
		ShopService.OrderID = 0;
		ShopService.OrderNo = "0";
		string str_order_no = "";
		string str_guid = Guid.NewGuid().ToString().Substring(0, 25).ToUpper();
		using (z_repoOrders orders = new z_repoOrders())
		{
			Orders newOrders = new Orders();
			newOrders.IsClosed = false;
			newOrders.IsValid = false;
			newOrders.SheetNo = "";
			newOrders.SheetDate = DateTime.Now;
			newOrders.CustNo = UserService.UserNo;
			newOrders.StatusCode = "ON";
			newOrders.GuidNo = str_guid;
			newOrders.PaymentNo = model.payment_no;
			newOrders.ShippingNo = model.shipping_no;
			newOrders.ReceiverName = model.receive_name;
			newOrders.ReceiverEmail = model.receive_email;
			newOrders.ReceiverAddress = model.receive_address;
			newOrders.Remark = "";

			orders.repo.Create(newOrders);
			orders.repo.SaveChanges();

			var neword = orders.repo.ReadSingle(m => m.GuidNo == str_guid);
			if (neword != null)
			{
				str_order_no = neword.Id.ToString().PadLeft(8, '0');
				neword.SheetNo = str_order_no;
				orders.repo.Update(neword);
				orders.repo.SaveChanges();

				ShopService.OrderID = neword.Id;
				ShopService.OrderNo = str_order_no;
			}
		}
		return str_order_no;
	}
	#endregion
}