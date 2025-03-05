using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;
using foodAPI.Models;

namespace foodAPI.Controllers
{
	[Route("api/[controller]")]
	public class UserApiController : ApiController
	{
		/// <summary>
		/// 取得兩個值 value1 及 value2
		/// </summary>
		/// <returns></returns>
		//[CorsHandle]
		[HttpGet]
		[Route("getUser/{id}")]
		//[JwtAuthActionFilter]
		public users Get(string id="")
		{
			using(dbEntities db = new dbEntities())
			{
				var model = db.users.Where(m => m.userId.ToString() == id).FirstOrDefault();
				return model;
			}
			
		}

	}
}
