using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace foodAPI.Controllers
{
	/// <summary>
	/// Demo 用的控制器
	/// </summary>
	public class ValuesController : ApiController
	{
		
		/// <summary>
		/// 取得兩個值 value1 及 value2
		/// </summary>
		/// <returns></returns>
		//[CorsHandle]
		//[JwtAuthActionFilter]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		/// <summary>
		/// 取得指定的值
		/// </summary>
		/// <param name="id">索引值</param>
		/// <returns></returns>
		public string Get(int id)
		{
			return "value";
		}

		// POST api/values
		public void Post([FromBody] string value)
		{
		}

		// PUT api/values/5
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		public void Delete(int id)
		{
		}
	}
}
