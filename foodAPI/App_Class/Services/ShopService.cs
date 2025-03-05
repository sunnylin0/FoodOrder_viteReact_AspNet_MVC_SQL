﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// 購物商城頁專用類別
/// </summary>
public static class ShopService
{
	/// <summary>
	/// 分類方式
	/// </summary>
	public static string CategoryNo { get { return GetSessionValue("ShopCategoryNo", ""); } set { HttpContext.Current.Session["ShopCategoryNo"] = value; } }
	/// <summary>
	/// 最低售價
	/// </summary>
	public static int PriceLow { get { return GetSessionIntegerValue("ShopPriceLow", 100); } set { HttpContext.Current.Session["ShopPriceLow"] = value; } }
	/// <summary>
	/// 最高售價
	/// </summary>
	public static int PriceHigh { get { return GetSessionIntegerValue("ShopPriceHigh", 5000); } set { HttpContext.Current.Session["ShopPriceHigh"] = value; } }
	public static int OrderID { get; set; }
	public static string OrderNo { get; set; }
	/// <summary>
	/// 排序方式
	/// </summary>
	public static string SortNo { get { return GetSessionValue("ShopSortNo", "NameAsc"); } set { HttpContext.Current.Session["ShopSortNo"] = value; } }
	/// <summary>
	/// 搜尋文字
	/// </summary>
	public static string SearchText { get { return GetSessionValue("ShopSearchText", ""); } set { HttpContext.Current.Session["ShopSearchText"] = value; } }
	/// <summary>
	/// 目前頁數
	/// </summary>
	public static int Page { get { return GetSessionIntegerValue("ShopPage", 1); } set { HttpContext.Current.Session["ShopPage"] = value; } }
	/// <summary>
	/// 總頁數
	/// </summary>
	public static int Pages
	{
		get
		{
			int int_page_count = PageRowCount / PageSize;
			if (PageRowCount % PageSize > 0) int_page_count++;
			return int_page_count;
		}

	}
	/// <summary>
	/// 每頁筆數
	/// </summary>
	public static int PageSize { get { return GetSessionIntegerValue("ShopPageSize", 10); } set { HttpContext.Current.Session["ShopPageSize"] = value; } }
	/// <summary>
	/// 每頁顯示頁數
	/// </summary>
	public static int PageCount { get { return GetSessionIntegerValue("ShopPageCount", 10); } set { HttpContext.Current.Session["ShopPageCount"] = value; } }
	/// <summary>
	/// 總筆數
	/// </summary>
	public static int PageRowCount { get { return GetSessionIntegerValue("ShopRowCount", 0); } set { HttpContext.Current.Session["ShopRowCount"] = value; } }
	/// <summary>
	/// 開始頁數
	/// </summary>
	public static int PageStart
	{
		get
		{
			int int_start = 1;
			if (Page > PageCount)
			{
				int int_count = Page / PageCount;
				if (Page % PageCount == 0) int_count--;
				int_start = (int_count * PageCount) + 1;
			}
			return int_start;
		}
	}
	/// <summary>
	/// 結束頁數
	/// </summary>
	public static int PageEnd
	{
		get
		{
			int int_page = PageStart;
			int int_row_count = PageRowCount;
			if (PageStart > PageCount)
			{
				int_row_count -= (PageSize * (PageStart - 1));
			}
			if (int_row_count > 0)
			{
				int int_count = PageSize / int_row_count;
				if (PageSize % int_row_count > 0) int_count++;
				if (int_count > PageCount) int_count = PageCount;
				int_page += (PageCount - 1);
				if (int_page > Pages) int_page = Pages;
			}
			return int_page;
		}
	}
	public static int PriorPage()
	{
		return (PageStart - 1);
	}
	public static int NextPage()
	{
		return (PageEnd + 1);
	}
	public static int GetAllProdCount()
	{
		using (z_repoProducts prod = new z_repoProducts())
		{
			return prod.repo.ReadAll().Count();
		}
	}
	/// <summary>
	/// 分類名稱
	/// </summary>
	public static string CategoryName
	{
		get
		{
			if (!string.IsNullOrEmpty(SearchText)) return string.Format("搜尋：{0}", SearchText);
			if (string.IsNullOrEmpty(CategoryNo)) return "全部商品";
			using (z_repoCategorys cate = new z_repoCategorys())
			{
				return cate.GetCategoryName(CategoryNo);
			}
		}
	}
	/// <summary>
	/// 排序名稱
	/// </summary>
	public static string SortName
	{
		get
		{
			if (SortNo == "Hot") return "熱門商品";
			if (SortNo == "NameAsc") return "依名稱,由小到大";
			if (SortNo == "NameDesc") return "依名稱,由大到小";
			if (SortNo == "PriceAsc") return "依價格,由小到大";
			if (SortNo == "PriceDesc") return "依價格,由大到小";
			return "";
		}
	}
	/// <summary>
	/// 取得 Session 值-文字型別
	/// </summary>
	/// <param name="sessionName">Session 名稱</param>
	/// <returns></returns>
	public static string GetSessionValue(string sessionName, string defauleValue)
	{
		return (HttpContext.Current.Session[sessionName] == null) ? defauleValue : HttpContext.Current.Session[sessionName].ToString();
	}
	/// <summary>
	/// 取得 Session 值-數字型別
	/// </summary>
	/// <param name="sessionName">Session 名稱</param>
	/// <returns></returns>
	public static int GetSessionIntegerValue(string sessionName, int defauleValue)
	{
		object obj_value = HttpContext.Current.Session[sessionName];
		if (obj_value == null) return defauleValue;
		string str_value = obj_value.ToString();
		int int_value = 0;
		if (int.TryParse(str_value, out int_value)) return int_value;
		return defauleValue;
	}
	/// <summary>
	/// 取得圖片路徑
	/// </summary>
	/// <param name="productNo">商品編號</param>
	/// <returns></returns>
	public static string GetProductImageUrl(string productNo)
	{
		string str_url = string.Format("~/Images/Product/{0}.jpg", productNo);
		if (!File.Exists(HttpContext.Current.Server.MapPath(str_url)))
			str_url = "~/Images/Product/None.jpg";
		return str_url;
	}
}