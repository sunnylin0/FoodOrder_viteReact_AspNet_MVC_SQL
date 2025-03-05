using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace foodAPI.Models
{
	[MetadataType(typeof(z_metadetail))]
	public partial class detail
	{
		[NotMapped]
		public List<subjoin> subItems;

		[NotMapped]
		public List<int> subjoinIdList;


	}
}

public abstract class z_metadetail
{
	[Key]
	public int detailId { get; set; }
	[Display(Name = "屬性名稱")]
	[Columns(CheckBox = false, Hidden = false, DropdownClass = "")]
	[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
	public string orderId { get; set; }
	[Display(Name = "屬性名稱")]
	[Columns(CheckBox = false, Hidden = false, DropdownClass = "")]
	[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
	public string menuId { get; set; }
	[Display(Name = "屬性名稱")]
	[Columns(CheckBox = false, Hidden = false, DropdownClass = "")]
	[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
	public string menuName { get; set; }
	[Display(Name = "屬性名稱")]
	[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:N0}")]
	[Columns(CheckBox = false, Hidden = false, DropdownClass = "")]
	[Default(DefaultValueType = enDefaultValueType.Int_0, DefaultValue = "")]
	public Nullable<int> price { get; set; }
	[Display(Name = "屬性名稱")]
	[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:N0}")]
	[Columns(CheckBox = false, Hidden = false, DropdownClass = "")]
	[Default(DefaultValueType = enDefaultValueType.Int_0, DefaultValue = "")]
	public Nullable<int> subPrice { get; set; }
	[Display(Name = "屬性名稱")]
	[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:N0}")]
	[Columns(CheckBox = false, Hidden = false, DropdownClass = "")]
	[Default(DefaultValueType = enDefaultValueType.Int_0, DefaultValue = "")]
	public Nullable<int> qty { get; set; }
	[Display(Name = "備註")]
	[Columns(CheckBox = false, Hidden = false, DropdownClass = "")]
	[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
	public string remark { get; set; }
}
