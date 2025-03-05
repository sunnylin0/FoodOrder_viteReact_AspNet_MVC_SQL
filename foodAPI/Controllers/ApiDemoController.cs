using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace foodAPI.Controllers
{
    public class ApiDemoController : Controller
    {
        // GET: ApiDemo
        public ActionResult Index()
        {
            return View();
        }

		public async Task<string> GetApiValues()
		{
			using (HttpClient httpClient = new HttpClient())
			{
				//網址
				string str_url = "http://localhost:1667/api/Token";

				// JWT 口令
				string str_secret = "1qaz@wsx";

				//輸入使用者資訊
				LoginData loginData = new LoginData();
				loginData.username = "johnson";
				loginData.password = "1234";

				//檢查 Token
				string str_token = "";
				string str_message = "";
				bool bln_valid = false;
				try
				{
					var authContent = JsonConvert.SerializeObject(loginData);
					StringContent httpContent = new StringContent(authContent, Encoding.UTF8, "application/json");
					HttpResponseMessage tokenResponse = await httpClient.PostAsync(str_url, httpContent);
					if (tokenResponse != null)
					{
						var token = await tokenResponse.Content.ReadAsStringAsync();
						if (token != null)
						{
							var tokenResult = JsonConvert.DeserializeObject<TokenObject>(token);
							if (tokenResult != null && tokenResult.Result)
							{
								httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenResult.Token);
								str_token = tokenResult.Token;
								bln_valid = true;
							}
						}
					}
				}
				catch (Exception ex)
				{
					str_message = ex.Message;
				}

				if (!bln_valid)
				{
					if (string.IsNullOrEmpty(str_message)) str_message = "Token 驗證錯誤";
					return str_message;
				}

				//讀出資料
				str_url = "http://localhost:6060/api/values";
				str_message = "";
				var response = await httpClient.GetStringAsync(str_url);
				if (response != null)
				{
					var data = JsonConvert.DeserializeObject<List<string>>(response);
					foreach (var item in data)
					{
						str_message += item + "</br>";
					}
				}
				return str_message;
			}
		}


	}
}