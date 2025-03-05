using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace foodAPI.Models
{
	[MetadataType(typeof(z_metacategory))]
	public partial class category
	{
		[NotMapped]
		public List<menu> products;

	}
}

public abstract class z_metacategory
{
	[Key]
	public string catId { get; set; }
	[Display(Name = "屬性名稱")]
	[Required(ErrorMessage = "不可空白!!")]
	[Columns(CheckBox = false, Hidden = false, DropdownClass = "")]
	[Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
	public string catName { get; set; }
}
