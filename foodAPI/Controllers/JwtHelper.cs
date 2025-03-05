using System;
using System.Text;
using Jose;
using Newtonsoft.Json.Linq;

public class JwtHelper
{
	public static JwtAuthObject ValidateToken(string token,string secret)
	{
		try
		{
			var secretKey = Encoding.UTF8.GetBytes(secret);
			var payload = Jose.JWT.Decode<JwtAuthObject>(token, secretKey, JwsAlgorithm.HS256);


			// 檢查 Token 是否過期
			if (payload.Expiration < DateTimeOffset.Now.AddHours(8).ToUnixTimeSeconds())
			{
				throw new Exception("Token 已過期!!");
			}

			return payload;
		}
		catch (Exception ex)
		{
			throw new Exception("Invalid token.", ex);
		}
	}
}