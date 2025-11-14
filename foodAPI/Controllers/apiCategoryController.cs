using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Services;
using foodAPI.Models;
using Jose;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace foodAPI.Controllers
{
	/// <summary>
	/// Category 用的控制器
	/// </summary>

	public class apiCategoryController : ApiController
	{
		/// <summary>
		/// 取得兩個值 value1 及 value2
		/// </summary>
		/// <returns></returns>
		//[CorsHandle]
		//[JwtAuthActionFilter]
		[HttpGet]
		[EnableCors(origins: "*", headers: "*", methods: "*")]
		[Route("getCategory")]
		public IHttpActionResult getCategory()
		{

			z_repocategory cate = new z_repocategory();
			var model = cate.repo.ReadAll().ToList();
			//return new string[] { "value1", "value2" };
			//return model.ToString().Split(new char[] { ',' });
			return Ok(model);
		}


		/// <summary>
		/// 取得使用者資料
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[EnableCors(origins: "*", headers: "*", methods: "*")]
		[Route("login")]
		public IHttpActionResult login()
		{

			z_repousers users = new z_repousers();
			var model = users.repo.ReadAll().ToList();
			//return new string[] { "value1", "value2" };
			//return model.ToString().Split(new char[] { ',' });
			return Ok(model);
		}

		/// <summary>
		/// 取得使用者資料
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		[EnableCors(origins: "*", headers: "*", methods: "*")]
		[Route("loginTO")]
		[WebMethod(EnableSession = true)]
		public IHttpActionResult loginTO([FromBody] LoginData request)
		{

			// 檢查輸入是否為空
			if (request?.email == null || request?.password == null)
			{
				return Content(HttpStatusCode.BadRequest, new { error = "Email and password are required." });
			}

			// 比對資料庫中的使用者資訊
			using (z_repousers users = new z_repousers())
			{
				var userItem = users.Login(request.email, request.password);
				if (userItem == null)
				{
					return Content(HttpStatusCode.Unauthorized, new { error = "Invalid email or password." });
				}
				// 生成 JWT Token
				var token = GenerateToken(userItem, "CityFood");

				// 返回 Token
				return Ok(new { user = userItem, accessToken = token });
			}
		}

		/// <summary>
		///  取得食品附加項目		
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[EnableCors(origins: "*", headers: "*", methods: "*")]
		[Route("subjoin")]
		public IHttpActionResult subjoin()
		{

			using (z_reposubCategory subCat = new z_reposubCategory())
			{
				var subCatModel = subCat.repo.ReadAll().ToList();
				using (z_reposubjoin subJoin = new z_reposubjoin())
				{
					if (subCatModel != null)
					{
						foreach (var scItem in subCatModel)
						{
							var sCatItems = subJoin.GetCategoryItems(scItem.subCatId);
							scItem.items = new List<subjoin>();
							foreach (var m in sCatItems)
							{
								scItem.items.Add(m);
							}
						}
					}
				}
				return Ok(subCatModel);
			}
		}
		/// <summary>
		///  取得食品 菜單資料		
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[EnableCors(origins: "*", headers: "*", methods: "*")]
		[Route("getMenu")]
		public IHttpActionResult getMenu()
		{
			using (z_repocategory cat = new z_repocategory())
			{

				return Ok(cat.GetMenuPage());

			}
		}

		/// <summary>
		///  取得訂單資料		
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[EnableCors(origins: "*", headers: "*", methods: "*")]
		[Route("getOrder")]
		public IHttpActionResult getOrder()
		{
			using (z_repoorder orders = new z_repoorder())
			{
				return Ok(orders.GetOrderPage());
			}
		}

		/// <summary>
		/// 取得指定使用者的歷史訂單
		/// </summary>
		/// <param name="userId">使用者ID</param>
		/// <returns></returns>
		[HttpGet]
		[EnableCors(origins: "*", headers: "*", methods: "*")]
		[Route("orders")] // 這裡使用與前端 axios.get(`${urlDomain}/orders?userId=${userId}`) 匹配的路由
		public IHttpActionResult getUserOrders([FromUri] string userId)
		{
			// 檢查 userId 是否為空或 null
			if (string.IsNullOrEmpty(userId))
			{
				return Content(HttpStatusCode.BadRequest, new { error = "User ID is required." });
			}

			try
			{
				using (z_repoorder orders = new z_repoorder())
				{
					// 假設 z_repoorder 裡面有一個方法 GetOrdersByUserId(string userId)
					// 用來查詢特定使用者的所有訂單
					var userOrders = orders.GetOrdersByUserId(userId);

					// 如果查不到資料，也可以返回 Ok(new List<order>()) 或 NotFound()
					if (userOrders == null || !userOrders.Any())
					{
						// 這裡返回 OK with empty list 讓前端可以順利處理沒有訂單的情況
						return Ok(new List<order>());
					}

					return Ok(userOrders);
				}
			}
			catch (Exception ex)
			{
				// 處理異常
				return Content(HttpStatusCode.InternalServerError, new { error = $"Error retrieving user orders: {ex.Message}" });
			}
		}


		/// <summary>
		/// 更新產品資料
		/// </summary>
		/// <param name="menuId"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		[HttpPut]
		[EnableCors(origins: "*", headers: "*", methods: "*")]
		[Route("products/{menuId}")]
		public IHttpActionResult products(string menuId, [FromBody] menu value)
		{
			try
			{
				using (z_repomenu mItem = new z_repomenu())
				{

					mItem.CreateEdit(value);
					return Ok();
				}
			}
			catch (Exception ex)
			{
				// 處理異常
				return Content(HttpStatusCode.InternalServerError, new { error = ex.Message });
			}
		}
		/// <summary>
		/// 更新訂單資料		
		/// </summary>
		/// <returns></returns>
		[HttpPut]
		[EnableCors(origins: "*", headers: "*", methods: "*")]
		[Route("orders/{orderid}")]
		public IHttpActionResult orders(string orderid, [FromBody] order value)
		{
			using (z_repoorder mItem = new z_repoorder())
			{

				mItem.CreateEdit(value);
				return Ok();
			}
		}


		/// <summary>
		/// 更新訂單資料		
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		[EnableCors(origins: "*", headers: "*", methods: "*")]
		[Route("mypost")]
		public IHttpActionResult mypost([FromBody] dmOrderPost value)
		{
			var blt_val = false;
			try
			{
				using (z_repoorder orderItem = new z_repoorder())
				{
					using (z_repodetail detailItem = new z_repodetail())
					{
						order ord = new order()
						{

							orderId = value.orderId,
							userId = value.userId,
							userName = value.userName,
							remark = value.remark,
							totalPrice = value.totalPrice,
							dateTime = value.dateTime,
							isDone = value.isDone,
							//details = value.details
						};
						orderItem.AddThis(ord);

						List<detail> detlList = new List<detail>();
						foreach (dmDetailPost item in value.details)
						{
							detail dd = new detail()
							{
								//detailId=
								orderId = value.orderId,
								menuId = item.menuId,
								menuName = item.menuName,
								price = item.price,
								subPrice = item.subjoinTotalPrice,
								qty = item.qty,
								remark = item.remark,
								subjoinIdList = item.subjoinItems
							};
							detlList.Add(dd);
						}

						detailItem.AddList(detlList);

						return Ok();
					}
				}
			}
			catch (Exception ex)
			{
				// 處理異常
				return Content(HttpStatusCode.InternalServerError, new
				{
					error = ex.Message
				});
			}
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


		/// <summary>
		/// 生成 JWT Token（使用 jose-jwt）
		/// </summary>
		/// <param name="user"> users 物件</param>
		/// <param name="secret">JWT 口令</param>
		/// <returns></returns>
		private string GenerateToken(users user, string secret)
		{
			var payload = new JwtAuthObject
			{
				//使用者
				userName = user.userName,
				//發證者
				Issuser = user.userName,
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

			var secretKey = Encoding.UTF8.GetBytes(secret);
			var token = Jose.JWT.Encode(payload, secretKey, JwsAlgorithm.HS256);

			return token;
		}

		// 密碼雜湊（範例，建議使用更安全的演算法如 bcrypt）
		private string HashPassword(string password)
		{
			return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));
		}

	}
}
