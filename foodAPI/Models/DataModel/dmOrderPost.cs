using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class dmOrderPost
{
	public string orderId              { get; set; }
	public int userId             { get; set; }
	public string userName             { get; set; }
	public string phone             { get; set; }
	public string remark             { get; set; }
	public int totalPrice             { get; set; }
	public Nullable<System.DateTime> dateTime { get; set; }
	public bool takeWay             { get; set; }
	public bool isDone             { get; set; }
	public List<dmDetailPost> details { get; set; }


	//orderId: "OD" + (+new Date()).toString(),
	//		userId: getDataFromLocalStorage('_user').userId,
	//		userName: getDataFromLocalStorage('_user').userName,
	//		phone: getDataFromLocalStorage('_user').phone,
	//		remark: cartState.remark,
	//		totalPrice: totalPrice,
	//		dateTime: getTimeNow(),
	//		takeWay: cartState?.pickmeals == 'out' ? 1 : 0,
	//		isDone: false,
	//		details: carts,
}