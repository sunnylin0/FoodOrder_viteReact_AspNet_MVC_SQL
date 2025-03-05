using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace foodAPI.Models 
{
    [MetadataType(typeof(z_metamenu))]
    public partial class menu
    {
      [NotMapped]
      public List<string> subjoinIds;

		}
}

public abstract class z_metamenu
{
    [Key]
    public string menuId { get; set; }
    [Display(Name = "屬性名稱")]
    [Columns(CheckBox = false , Hidden = false , DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string catId { get; set; }
    [Display(Name = "屬性名稱")]
    [Columns(CheckBox = false , Hidden = false , DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string menuNameEn { get; set; }
    [Display(Name = "屬性名稱")]
    [Columns(CheckBox = false , Hidden = false , DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string menuName { get; set; }
    [Display(Name = "屬性名稱")]
    [Columns(CheckBox = false , Hidden = false , DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string comment { get; set; }
    [Display(Name = "屬性名稱")]
    [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:N0}")]
    [Columns(CheckBox = false , Hidden = false , DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Int_0, DefaultValue = "")]
    public Nullable<int> price { get; set; }
    [Display(Name = "屬性名稱")]
    [Columns(CheckBox = false , Hidden = false , DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string img { get; set; }
    [Display(Name = "屬性名稱")]
    [Columns(CheckBox = false , Hidden = false , DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string isSoldOut { get; set; }
}
