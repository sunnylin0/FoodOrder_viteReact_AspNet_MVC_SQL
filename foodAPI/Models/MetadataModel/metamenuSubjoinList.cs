using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace foodAPI.Models 
{
    [MetadataType(typeof(z_metamenuSubjoinList))]
    public partial class menuSubjoinList
    {
    }
}

public abstract class z_metamenuSubjoinList
{
    [Key]
    public int menuSubListId { get; set; }
    [Display(Name = "屬性名稱")]
    [Columns(CheckBox = false , Hidden = false , DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string menuId { get; set; }
    [Display(Name = "屬性名稱")]
    [Columns(CheckBox = false , Hidden = false , DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string subCatId { get; set; }
}
