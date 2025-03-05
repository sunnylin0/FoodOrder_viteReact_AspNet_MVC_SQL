using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;

namespace foodAPI
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// 啟用 CORS 跨網域存取
			var cors = new EnableCorsAttribute("*", "*", "*"); // 允許所有來源、所有標頭、所有方法
			config.EnableCors(cors);


			// 啟用 JWT 驗證
			//ConfigureOAuth(app);



			// Web API 設定和服務

			// Web API 路由
			config.MapHttpAttributeRoutes();
			config.Routes.MapHttpRoute(
					name: "DefaultApi",
					routeTemplate: "api/{controller}/{id}",
					defaults: new { id = RouteParameter.Optional }
			);
		}

	} 
	}
