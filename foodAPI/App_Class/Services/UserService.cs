using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using foodAPI.Models;

/// <summary>
/// 使用者相關服務
/// </summary>
public static class UserService
{
	public static string UserEmail { get { return SessionService.GetValue("UserEmail"); } set { SessionService.SetValue("UserEmail", value); } }
	public static string UserName { get { return SessionService.GetValue("UserName"); } set { SessionService.SetValue("UserName", value); } }
	public static string RoleNo { get { return SessionService.GetValue("RoleNo"); } set { SessionService.SetValue("RoleNo", value); } }
	//line
	//public static string RoleName { get { using (z_repoRoles roles = new z_repoRoles()) { return roles.GetDataName(RoleNo); } } }
	public static string RoleName { get { return SessionService.GetValue("RoleName"); } set { SessionService.SetValue("RoleName", value); } }
	public static string DeptNo { get { return SessionService.GetValue("DeptNo"); } set { SessionService.SetValue("DeptNo", value); } }
	public static string DeptName { get { return SessionService.GetValue("DeptName"); } set { SessionService.SetValue("DeptName", value); } }
	public static string TitleNo { get { return SessionService.GetValue("TitleNo"); } set { SessionService.SetValue("TitleNo", value); } }
	public static string TitleName { get { return SessionService.GetValue("TitleName"); } set { SessionService.SetValue("TitleName", value); } }

	public static bool IsLogin { get { return SessionService.GetBoolValue("IsLogin"); } set { SessionService.SetValue("IsLogin", value); } }

	/// <summary>
	/// 登入
	/// </summary>
	/// <param name="userEMail">使用者 E-Mail</param>
	/// <param name="userName">使用者姓名</param>
	/// <param name="roleNo">角色代號</param>
	public static void Login(string userEMail, string userName, string roleNo)
	{
		UserEmail = userEMail;
		UserName = userName;
		RoleNo = roleNo;
		DeptNo = "";
		DeptName = "";
		TitleNo = "";
		TitleName = "";
		IsLogin = true;
		using (z_repousers user = new z_repousers()) { user.SetUserInfo(); }
		using (z_repoCompanys user = new z_repoCompanys()) { user.SetDefaultCompany(); }
	}
	/// <summary>
	/// 登出
	/// </summary>
	public static void Logout()
	{
		UserEmail = "";
		UserName = "";
		RoleNo = "";
		DeptNo = "";
		DeptName = "";
		TitleNo = "";
		TitleName = "";
		IsLogin = false;
	}
	/// <summary>
	/// 除錯模式預設使用者
	/// </summary>
	public static void DemoUser()
	{
		UserEmail = "A2@store.com";
		UserName = "測試帳號";
		DeptNo = "Mis";
		DeptName = "資訊部";
		TitleNo = "Mis";
		TitleName = "程式設計師";
		IsLogin = true;
		PrgService.DemoSecurity();
	}
}