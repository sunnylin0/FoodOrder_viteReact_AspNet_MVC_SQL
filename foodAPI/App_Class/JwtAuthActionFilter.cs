using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Jose;

/// <summary>
/// JwtAuthActionFilter
/// </summary>
public class JwtAuthActionFilter : ActionFilterAttribute
{
	/// <summary>
	/// 事件執行
	/// </summary>
	/// <param name="actionContext"></param>
	public override void OnActionExecuting(HttpActionContext actionContext)
	{
		// JWT 口令
		var secret = "1qaz@wsx";

		if (actionContext.Request.Headers.Authorization == null || actionContext.Request.Headers.Authorization.Scheme != "Bearer")
		{
			setErrorResponse(actionContext, "驗證錯誤");
		}
		else
		{
			try
			{
				var jwtObject = Jose.JWT.Decode<JwtAuthObject>(
						actionContext.Request.Headers.Authorization.Parameter,
						Encoding.UTF8.GetBytes(secret),
						JwsAlgorithm.HS256);
				if (jwtObject != null)
				{
					if (jwtObject.Expiration < DateTimeOffset.Now.AddHours(8).ToUnixTimeSeconds())
					{
						setErrorResponse(actionContext, "Token 已過期!!");
					}
				}
			}
			catch (Exception ex)
			{
				setErrorResponse(actionContext, ex.Message);
			}
		}

		base.OnActionExecuting(actionContext);
	}

	private static void setErrorResponse(HttpActionContext actionContext, string message)
	{
		var response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, message);
		actionContext.Response = response;
	}
}
