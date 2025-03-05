using foodAPI.Models;
using Jose;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPIServerDemo.Controllers
{
	public class TokenController : ApiController
	{
		/// <summary>
		/// Post
		/// </summary>
		/// <param name="loginData">登入資料</param>
		/// <returns></returns>
		/// <exception cref="UnauthorizedAccessException"></exception>
		[EnableCors(origins: "*", headers: "*", methods: "*")]
		[HttpPost]
		public object Post(LoginData loginData)
		{
			// JWT 口令
			var secret = "1qaz@wsx";
			if (loginData == null) loginData = new LoginData { username = "", password = "" };
			//判斷登入帳密正確性
			bool bln_valid = false;
			string str_name = "";
			using(dbEntities db = new dbEntities())
			{
				var data = db.users.Where(m => m.userName == loginData.username && m.password == loginData.password).FirstOrDefault();
				if (data != null)
				{
					str_name = data.userName;
					bln_valid = true;
				}
			}

			// TODO: 真實世界檢查帳號密碼
			if (bln_valid)
			{
				var payload = new JwtAuthObject()
				{
					//使用者
					userName = loginData.username,
					//發證者
					Issuser = str_name,
					//主旨內容
					Subject = "WebAPIDemo",
					//簽發時間 = 中原標準時間」（GMT+8）
					IssueAt = DateTimeOffset.Now.AddHours(8).ToUnixTimeSeconds(),
					//生效時間 = 中原標準時間」（GMT+8）
					Effective = DateTimeOffset.Now.AddHours(8).ToUnixTimeSeconds(),
					//過期時間 = 中原標準時間」（GMT+8）+ 10 分鐘 
					Expiration = DateTimeOffset.Now.AddHours(8).AddMinutes(10).ToUnixTimeSeconds(),
					//JWT ID = GUID 亂碼
					JwtId = Guid.NewGuid().ToString().Replace("-", "")
				};
				return new
				{
					Result = true,
					token = Jose.JWT.Encode(payload, Encoding.UTF8.GetBytes(secret), JwsAlgorithm.HS256)
				};
			}
			else
			{
				return new
				{
					Result = false,
					token = "帳密不正確!!"
				};
			}
		}
	}
}
