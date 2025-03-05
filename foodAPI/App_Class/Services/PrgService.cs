using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using DocumentFormat.OpenXml.EMMA;

/// <summary>
/// 程式相關服務
/// </summary>
public static class PrgService
{
	/// <summary>
	/// 角色編號
	/// </summary>
	public static string RoleNo { get { return SessionService.GetValue("RoleNo", ""); } set { SessionService.SetValue("RoleNo", value); } }
	/// <summary>
	/// 程式編號
	/// </summary>
	public static string ModuleNo { get { return SessionService.GetValue("ModuleNo", ""); } set { SessionService.SetValue("ModuleNo", value); } }
	/// <summary>
	/// 程式編號
	/// </summary>
	public static string PrgNo { get { return SessionService.GetValue("PrgNo", ""); } set { SessionService.SetValue("PrgNo", value); } }
	/// <summary>
	/// 程式名稱
	/// </summary>
	public static string PrgName { get { return SessionService.GetValue("PrgName", ""); } set { SessionService.SetValue("PrgName", value); } }
	/// <summary>
	/// 新增權限
	/// </summary>
	public static bool IsAdd { get { return SessionService.GetBoolValue("IsAdd", false); } set { SessionService.SetValue("IsAdd", value); } }
	/// <summary>
	/// 修改權限
	/// </summary>
	public static bool IsEdit { get { return SessionService.GetBoolValue("IsEdit", false); } set { SessionService.SetValue("IsEdit", value); } }
	/// <summary>
	/// 刪除權限
	/// </summary>
	public static bool IsDelete { get { return SessionService.GetBoolValue("IsDelete", false); } set { SessionService.SetValue("IsDelete", value); } }
	/// <summary>
	/// 審核權限
	/// </summary>
	public static bool IsConfirm { get { return SessionService.GetBoolValue("IsConfirm", false); } set { SessionService.SetValue("IsConfirm", value); } }
	/// <summary>
	/// 回復權限
	/// </summary>
	public static bool IsUndo { get { return SessionService.GetBoolValue("IsUndo", false); } set { SessionService.SetValue("IsUndo", value); } }
	/// <summary>
	/// 下載權限
	/// </summary>
	public static bool IsDownload { get { return SessionService.GetBoolValue("IsDownload", false); } set { SessionService.SetValue("IsDownload", value); } }
	/// <summary>
	/// 上傳權限
	/// </summary>
	public static bool IsUpload { get { return SessionService.GetBoolValue("IsUpload", false); } set { SessionService.SetValue("IsUpload", value); } }
	/// <summary>
	/// 列印權限
	/// </summary>
	public static bool IsPrint { get { return SessionService.GetBoolValue("IsPrint", false); } set { SessionService.SetValue("IsPrint", value); } }
	/// <summary>
	/// 作廢權限
	/// </summary>
	public static bool IsInvalid { get { return SessionService.GetBoolValue("IsInvalid", false); } set { SessionService.SetValue("IsInvalid", value); } }
	/// <summary>
	/// 程式資訊
	/// </summary>
	public static string PrgInfo
	{
		get
		{
			if (string.IsNullOrEmpty(PrgNo)) return PrgName;
			return string.Format("{0} {1}", PrgNo, PrgName);
		}
	}
	/// <summary>
	/// 副標題
	/// </summary>
	public static string SubHeader { get { return SessionService.GetValue("SubHeader", ""); } set { SessionService.SetValue("SubHeader", value); } }
	/// <summary>
	/// 是否分頁
	/// </summary>
	public static bool IsPageSize { get { return SessionService.GetBoolValue("IsPageSize", true); } set { SessionService.SetValue("IsPageSize", value); } }
	/// <summary>
	/// 是否可搜尋
	/// </summary>
	public static bool IsSearch { get { return SessionService.GetBoolValue("IsSearch", true); } set { SessionService.SetValue("IsSearch", value); } }
	/// <summary>
	/// 每頁筆數
	/// </summary>
	public static int PageSize { get { return SessionService.GetIntValue("PageSize", 10); } set { SessionService.SetValue("PageSize", value); } }
	/// <summary>
	/// Id 參數值
	/// </summary>
	public static string Id { get { return SessionService.GetValue("Id", ""); } set { SessionService.SetValue("Id", value); } }
	/// <summary>
	/// Name 參數值
	/// </summary>
	public static string Name { get { return SessionService.GetValue("Name", ""); } set { SessionService.SetValue("Name", value); } }
	/// <summary>
	/// Row Id 參數值
	/// </summary>
	public static int RowId { get { return SessionService.GetIntValue("RowId", 0); } set { SessionService.SetValue("RowId", value); } }
	/// <summary>
	/// 已選取的 Row Id 參數值
	/// </summary>
	public static int SelectedId { get { return SessionService.GetIntValue("SelectedId", 0); } set { SessionService.SetValue("SelectedId", value); } }
	/// <summary>
	/// SearchText
	/// </summary>
	public static string SearchText { get { return SessionService.GetValue("SearchText", ""); } set { SessionService.SetValue("SearchText", value); } }
	/// <summary>
	/// 是否為排序模式
	/// </summary>
	public static bool SortMode { get { return SessionService.GetBoolValue("SortMode", true); } set { SessionService.SetValue("SortMode", value); } }
	/// <summary>
	/// 排序欄位
	/// </summary>
	public static string SortColumn { get { return SessionService.GetValue("SortColumn", ""); } set { SessionService.SetValue("SortColumn", value); } }
	/// <summary>
	/// 排序方式
	/// </summary>
	public static enSortDirection SortDirection { get; set; } = enSortDirection.ASC;
	/// <summary>
	/// FormId Id 參數值
	/// </summary>
	public static int FormId { get { return SessionService.GetIntValue("FormId", 0); } set { SessionService.SetValue("FormId", value); } }
	/// <summary>
	/// No 參數值
	/// </summary>
	public static string No { get { return SessionService.GetValue("No", ""); } set { SessionService.SetValue("No", value); } }
	/// <summary>
	/// Pno 參數值
	/// </summary>
	public static string Pno { get { return SessionService.GetValue("Pno", ""); } set { SessionService.SetValue("Pno", value); } }
	/// <summary>
	/// Tag1 參數值
	/// </summary>
	public static string Tag1 { get { return SessionService.GetValue("Tag1", ""); } set { SessionService.SetValue("Tag1", value); } }
	/// <summary>
	/// Tag2 參數值
	/// </summary>
	public static string Tag2 { get { return SessionService.GetValue("Tag2", ""); } set { SessionService.SetValue("Tag2", value); } }
	/// <summary>
	/// Tag3 參數值
	/// </summary>
	public static string Tag3 { get { return SessionService.GetValue("Tag3", ""); } set { SessionService.SetValue("Tag3", value); } }
	/// <summary>
	/// Tag4 參數值
	/// </summary>
	public static string Tag4 { get { return SessionService.GetValue("Tag4", ""); } set { SessionService.SetValue("Tag4", value); } }
	/// <summary>
	/// 區域名稱
	/// </summary>
	public static string Area { get { return SessionService.GetValue("Area", ""); } set { SessionService.SetValue("Area", value); } }
	/// <summary>
	/// 控制器名稱
	/// </summary>
	public static string Controller { get { return SessionService.GetValue("Controller", ""); } set { SessionService.SetValue("Controller", value); } }
	/// <summary>
	/// 動作名稱
	/// </summary>
	public static string Action { get { return SessionService.GetValue("Action", ""); } set { SessionService.SetValue("Action", value); } }
	/// <summary>
	/// 參數名稱
	/// </summary>
	public static string ParameterName { get { return SessionService.GetValue("ParameterName", ""); } set { SessionService.SetValue("ParameterName", value); } }
	/// <summary>
	/// 參數值
	/// </summary>
	public static string ParameterValue { get { return SessionService.GetValue("ParameterValue", ""); } set { SessionService.SetValue("ParameterValue", value); } }
	/// <summary>
	/// 總頁數
	/// </summary>
	public static int PageCount { get { return SessionService.GetIntValue("PageCount", 0); } set { SessionService.SetValue("PageCount", value); } }
	/// <summary>
	/// 目前頁數
	/// </summary>
	public static int PageNumber { get { return SessionService.GetIntValue("PageNumber", 0); } set { SessionService.SetValue("PageNumber", value); } }
	/// <summary>
	/// 程式是否合法
	/// </summary>
	public static bool IsValid
	{
		get
		{
			bool isValid = true;
			if (string.IsNullOrEmpty(PrgNo)) isValid = false;
			if (string.IsNullOrEmpty(Controller)) isValid = false;
			if (string.IsNullOrEmpty(Action)) isValid = false;
			return isValid;
		}
	}
	/// <summary>
	/// 卡片寛度
	/// </summary>
	public static enCardSize CardSize { get; set; }
	/// <summary>
	/// 卡片寛度的 CSS 名稱
	/// </summary>
	public static string CardSizeCss
	{
		get
		{
			string str_cass = "card-size-max";
			if (CardSize == enCardSize.Small) str_cass = "card-size-small";
			if (CardSize == enCardSize.Medium) str_cass = "card-size-medium";
			if (CardSize == enCardSize.Large) str_cass = "card-size-large";
			if (CardSize == enCardSize.Max) str_cass = "card-size-max";
			return str_cass;
		}
	}
	public static bool PrgInit(string prgNo)
	{
			SubHeader = "";
			SelectedId = 0;
			//var data = repos.GetData(UserService.RoleNo, prgNo);
			//if (data == null) return false;
			prgNo = "data.PrgNo";
			PrgName = "data.PrgName";
			return true;
		
	}

	/// <summary>
	/// 初始化作業
	/// </summary>
	public static void Init()
	{
		Init("", enCardSize.Max);
	}
	/// <summary>
	/// 初始化作業
	/// </summary>
	/// <param name="prgName">程式名稱</param>
	public static void Init(string prgName)
	{
		Init();
		PrgName = prgName;
	}
	/// <summary>
	/// 初始化作業
	/// </summary>
	/// <param name="prgName">程式名稱</param>
	/// <param name="actionName">動作名稱</param>
	/// <param name="cardSize">卡片寛度</param>
	public static void Init(string prgName, enAction actionName, enCardSize cardSize)
	{
		Init("", cardSize);
		PrgName = prgName;
		Action = ActionService.GetActionName(actionName);
	}
	/// <summary>
	/// 初始化作業
	/// </summary>
	/// <param name="prgName">程式名稱</param>
	/// <param name="actionName">動作名稱</param>
	/// <param name="cardSize">卡片寛度</param>
	public static void Init(string prgName, string actionName, enCardSize cardSize)
	{
		Init("", cardSize);
		PrgName = prgName;
		Action = actionName;
	}
	/// <summary>
	/// 初始化作業
	/// </summary>
	/// <param name="prgNo">程式編號</param>
	/// <param name="prgName">程式名稱</param>
	/// <param name="controllerName">控制器名稱</param>
	/// <param name="actionName">動作名稱</param>
	public static void Init(string prgNo, string prgName, string areaName, string controllerName, string actionName, enCardSize cardSize)
	{
		Init(prgNo, cardSize);
		RoleNo = UserService.RoleNo;
		PrgNo = prgNo;
		PrgName = prgName;
		Area = areaName;
		Controller = controllerName;
		Action = actionName;
		ParameterName = "";
		ParameterValue = "";
	}
	/// <summary>
	/// 設定程式代號
	/// </summary>
	/// <param name="prgNo">程式代號</param>
	/// <param name="prgName">程式名稱</param>
	public static void SetProgramNoName(string prgNo, string prgName)
	{
		RoleNo = ActionService.Area;
		PrgNo = prgNo;
		PrgName = prgName;
	}

	/// <summary>
	/// 自定程式名稱
	/// </summary>
	/// <param name="subHeader">自定程式名稱</param>
	public static void SetProgram(string subHeader)
	{
		SubHeader = subHeader;
	}

	/// <summary>
	/// 設定程式代號
	/// </summary>
	/// <param name="prgNo">程式代號</param>
	public static void SetProgram(string roleNo, string prgNo)
	{
	
				RoleNo = roleNo;
				PrgName = "data.PrgName";
				Area = "data.AreaName";
				Controller = "data.ControllerName";
				Action = "data.ActionName";
				ParameterName = "id";
				ParameterValue = "data.ParmValue";
				SubHeader = "";
	
		
	}

	/// <summary>
	/// 初始化作業
	/// </summary>
	/// <param name="prgNo">程式編號</param>
	/// <param name="cardSize">卡片寛度</param>
	public static void Init(string prgNo, enCardSize cardSize)
	{
			RoleNo = UserService.RoleNo;
			PrgNo = prgNo;
			PrgName = "";
			SubHeader = "";
			Id = "";
			Name = "";
			RowId = 0;
			FormId = 0;
			Controller = "";
			Area = "";
			Action = "";
			ParameterName = "";
			ParameterValue = "";
		{ 
				PrgName = "data.PrgName";
				Area = "data.AreaName";
				Controller = "data.ControllerName";
				Action = "data.ActionName";
				ParameterName = "id";
				ParameterValue = "data.ParmValue";
			}
			CardSize = cardSize;
		
	}

	/// <summary>
	/// 設定動作資訊
	/// </summary>
	/// <param name="actionName">動作名稱</param>
	/// <param name="cardSize">卡片寛度</param>
	public static void SetAction(enAction actionName)
	{
		Action = ActionService.GetActionName(actionName);
	}

	/// <summary>
	/// 設定程式資訊
	/// </summary>
	public static void SetProgram()
	{
			string str_prg_no = ActionService.Controller.Split('_')[0];
			string str_module_no = "";
			if (string.IsNullOrEmpty(str_prg_no)) str_prg_no = "Dashboard";

			RoleNo = UserService.RoleNo;
			ModuleNo = str_module_no;
			PrgNo = str_prg_no;
			PrgName =  "未命名" ;
			SubHeader = "";
		
	}

	/// <summary>
	/// 設定程式資訊
	/// </summary>
	/// <param name="prgNo">程式代號</param>
	/// <param name="prgName">程式名稱</param>
	public static void SetProgram(string roleNo, string prgNo, string prgName)
	{
			string str_module_no = "";
			if (string.IsNullOrEmpty(prgNo)) prgNo = "Dashboard";

			str_module_no = "data.ModuleNo";
			RoleNo = roleNo;
			ModuleNo = str_module_no;
			PrgNo = prgNo;
			PrgName = prgName;
			SubHeader = "";
		
	}

	/// <summary>
	/// 設定動作資訊
	/// </summary>
	/// <param name="actionName">動作名稱</param>
	/// <param name="cardSize">卡片寛度</param>
	public static void SetAction(enAction actionName, enCardSize cardSize)
	{
		Action = ActionService.GetActionName(actionName);
		CardSize = cardSize;
	}

	/// <summary>
	/// 設定動作資訊
	/// </summary>
	/// <param name="actionName">動作名稱</param>
	/// <param name="cardSize">卡片寛度</param>
	public static void SetAction(string actionName, enCardSize cardSize)
	{
		Action = actionName;
		CardSize = cardSize;
	}

	/// <summary>
	/// 設定動作資訊及頁數資訊
	/// </summary>
	/// <param name="actionName">動作名稱</param>
	/// <param name="cardSize">卡片寛度</param>
	/// <param name="pageNumber">目前頁數</param>
	/// <param name="pageCount">總頁數</param>
	public static void SetAction(enAction actionName, enCardSize cardSize, int pageNumber, int pageCount)
	{
		Action = ActionService.GetActionName(actionName);
		CardSize = cardSize;
		PageNumber = pageNumber;
		PageCount = pageCount;
	}

	/// <summary>
	/// 設定動作資訊及頁數資訊
	/// </summary>
	/// <param name="prgName">程式名稱</param>
	/// <param name="actionName">動作名稱</param>
	/// <param name="cardSize">卡片寛度</param>
	/// <param name="pageNumber">目前頁數</param>
	/// <param name="pageCount">總頁數</param>
	public static void SetAction(string prgName, enAction actionName, enCardSize cardSize, int pageNumber, int pageCount)
	{
		PrgName = prgName;
		Action = ActionService.GetActionName(actionName);
		CardSize = cardSize;
		PageNumber = pageNumber;
		PageCount = pageCount;
	}

	/// <summary>
	/// 設定動作資訊及頁數資訊
	/// </summary>
	/// <param name="actionName">動作名稱</param>
	/// <param name="cardSize">卡片寛度</param>
	/// <param name="pageNumber">目前頁數</param>
	/// <param name="pageCount">總頁數</param>
	public static void SetAction(string actionName, enCardSize cardSize, int pageNumber, int pageCount)
	{
		Action = actionName;
		CardSize = cardSize;
		PageNumber = pageNumber;
		PageCount = pageCount;
	}

	public static void SetProgramSecurity()
	{
		//using (z_repoSecuritys repos = new z_repoSecuritys())
		//{
		//	repos.SetUserSecurity(PrgNo);
		//}
	}

	public static bool IsProgramSecurity(enSecurtyMode securtyMode)
	{
		return IsProgramSecurity(securtyMode, 0);
	}

	public static bool IsProgramSecurity(enSecurtyMode securtyMode, int rowId)
	{
		//除錯模式
		if (AppService.DebugMode) return true;

		//沒有程式權限即不通過驗證 
		if (securtyMode == enSecurtyMode.None) return true;
		if (securtyMode == enSecurtyMode.Index && !string.IsNullOrEmpty(Id)) return true;
		if (securtyMode == enSecurtyMode.Add && IsAdd == false) return false;
		if (securtyMode == enSecurtyMode.CreateEdit && rowId == 0 && IsAdd == false) return false;
		if (securtyMode == enSecurtyMode.CreateEdit && rowId > 0 && IsEdit == false) return false;
		if (securtyMode == enSecurtyMode.Confirm && IsConfirm == false) return false;
		if (securtyMode == enSecurtyMode.Invalid && IsInvalid == false) return false;
		if (securtyMode == enSecurtyMode.Delete && IsDelete == false) return false;
		if (securtyMode == enSecurtyMode.Download && IsDownload == false) return false;
		if (securtyMode == enSecurtyMode.Edit && IsEdit == false) return false;
		if (securtyMode == enSecurtyMode.Print && IsPrint == false) return false;
		if (securtyMode == enSecurtyMode.Undo && IsUndo == false) return false;
		if (securtyMode == enSecurtyMode.Upload && IsUpload == false) return false;
		return true;
	}

	/// <summary>
	/// 取得欄位排序圖示
	/// </summary>
	/// <returns></returns>
	public static string GetSortIcon(string columnName)
	{
		string str_icon = "";
		if (SortMode)
		{
			//str_icon = "◆";
			if (SortColumn == columnName)
			{
				if (SortDirection == enSortDirection.ASC) str_icon = "▲";
				if (SortDirection == enSortDirection.DESC) str_icon = "▼";
			}
		}
		return str_icon;
	}

	public static string SetIndex(int pageNumber = 1, int pageCount = 0, string searchText = "")
	{
		SearchText = searchText;
		SetProgram();
		SetAction(ActionService.IndexName, enCardSize.Max, pageNumber, pageCount);
		return $"第{pageNumber}頁,共{pageCount}頁";
	}

	/// <summary>
	/// 權限初始化
	/// </summary>
	public static void InitSecurity()
	{
		IsAdd = false;
		IsConfirm = false;
		IsDelete = false;
		IsDownload = false;
		IsEdit = false;
		IsInvalid = false;
		IsPrint = false;
		IsUndo = false;
		IsUpload = false;
	}
	/// <summary>
	/// 除錯模式預設使用者權限
	/// </summary>
	public static void DemoSecurity()
	{
		IsAdd = true;
		IsConfirm = true;
		IsDelete = true;
		IsDownload = true;
		IsEdit = true;
		IsInvalid = true;
		IsPrint = true;
		IsUndo = true;
		IsUpload = true;
	}
}