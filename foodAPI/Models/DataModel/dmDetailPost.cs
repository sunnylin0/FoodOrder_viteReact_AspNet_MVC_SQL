using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class dmDetailPost
{
	public int id { get; set; }
	public string catId { get; set; }
	public string comment { get; set; }
	public string img { get; set; }
	public bool isSoldOut { get; set; }
	public string menuId { get; set; }
	public string menuName { get; set; }
	public string menuNameEn { get; set; }
	public string name { get; set; }
	public int price { get; set; }
	public int qty { get; set; }
	public string remark { get; set; }
	public List<string> subjoinIds { get; set; }
	public List<int> subjoinItems { get; set; }
	public int subjoinTotalPrice { get; set; }
	public string tokenId { get; set; }
	public int total { get; set; }


//	catId: "c01"
//comment: "極致鮮美的海陸首選"
//id: ""
//img: "./Img/PC/aa2.jpg"
//isSoldOut: 0
//menuId: "p015"
//menuName: "炙燒黑松露海膽"
//menuNameEn: "uni_truffle"
//name: ""
//price: 200
//qty: 1
//remark: ""
//subjoinIds: ['AH01']
//subjoinItems: (3) ['3', '2', '1']
//subjoinTotalPrice: 0
//tokenId: "0p3ms0oii48"
//total: 200
}