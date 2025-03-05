using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace foodAPI.Models 
{
    [MetadataType(typeof(z_metasubCategory))]
    public partial class subCategory
    {
      [NotMapped]
      public List<subjoin> items;
    }
}

public abstract class z_metasubCategory
{
    [Key]
    public string subCatId { get; set; }
    [Display(Name = "屬性名稱")]
    [Columns(CheckBox = false , Hidden = false , DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string subCatName { get; set; }
    [Display(Name = "屬性名稱")]
    [Columns(CheckBox = false , Hidden = false , DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string isMulti { get; set; }
}
