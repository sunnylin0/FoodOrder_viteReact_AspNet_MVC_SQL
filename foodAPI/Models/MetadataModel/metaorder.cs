using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace foodAPI.Models
{
	[MetadataType(typeof(z_metaorder))]
	public partial class order
	{
		[NotMapped]
		public List<detail> details;
	}
}

public abstract class z_metaorder
{
	[Key]
	public string orderId { get; set; }
	[Display(Name = "屬性名稱")]
	[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:N0}")]
	[Columns(CheckBox = false, Hidden = false, DropdownClass = "")]
	[Default(DefaultValueType = enDefaultValueType.Int_0, DefaultValue = "")]
	public Nullable<int> userId { get; set; }
	[Display(Name = "發佈人員")]
	[Columns(CheckBox = false, Hidden = false, DropdownClass = "")]
	[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
	public string userName { get; set; }
	[Display(Name = "屬性名稱")]
	[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:N0}")]
	[Columns(CheckBox = false, Hidden = false, DropdownClass = "")]
	[Default(DefaultValueType = enDefaultValueType.Int_0, DefaultValue = "")]
	public Nullable<int> totalPrice { get; set; }
	[Display(Name = "屬性名稱")]
	[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
	[Columns(CheckBox = false, Hidden = false, DropdownClass = "")]
	[Default(DefaultValueType = enDefaultValueType.Date_Today, DefaultValue = "")]
	public Nullable<System.DateTime> dateTime { get; set; }
	[Display(Name = "屬性名稱")]
	[Columns(CheckBox = false, Hidden = false, DropdownClass = "")]
	[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
	public string takeAway { get; set; }
	[Display(Name = "屬性名稱")]
	[Columns(CheckBox = false, Hidden = false, DropdownClass = "")]
	[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
	public string isDone { get; set; }
	[Display(Name = "備註")]
	[Columns(CheckBox = false, Hidden = false, DropdownClass = "")]
	[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
	public string remark { get; set; }
}
