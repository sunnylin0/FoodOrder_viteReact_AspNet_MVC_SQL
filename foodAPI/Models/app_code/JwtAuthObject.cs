using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// JWT 認證物件
/// </summary>
public class JwtAuthObject
{
	/// <summary>
	/// issuser : 發證者
	/// </summary>
	public string Issuser { get; set; }
	/// <summary>
	/// subject : 主體內容
	/// </summary>
	public string Subject { get; set; }
	/// <summary>
	/// Expiration Time
	/// </summary>
	public long Expiration { get; set; }
	/// <summary>
	/// issue at : 簽發時間
	/// </summary>
	public long IssueAt { get; set; }
	/// <summary>
	/// not before : 生效時間
	/// Effective Time
	/// </summary>
	public long Effective { get; set; }
	/// <summary>
	/// JWT ID
	/// </summary>
	public string JwtId { get; set; }
	/// <summary>
	/// 使用者名稱
	/// </summary>
	public string userName { get; set; }

	/// <summary>
	/// E-mail
	/// </summary>
	public string email { get; set; }
}
