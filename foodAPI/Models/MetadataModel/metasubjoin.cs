using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace foodAPI.Models 
{
    [MetadataType(typeof(z_metasubjoin))]
    public partial class subjoin
    {
    }
}

public abstract class z_metasubjoin
{
    [Key]
    public int subId { get; set; }
    [Display(Name = "屬性名稱")]
    [Columns(CheckBox = false , Hidden = false , DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string subCatId { get; set; }
    [Display(Name = "屬性名稱")]
    [Columns(CheckBox = false , Hidden = false , DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.String_Space, DefaultValue = "")]
    public string subName { get; set; }
    [Display(Name = "屬性名稱")]
    [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:N0}")]
    [Columns(CheckBox = false , Hidden = false , DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Int_0, DefaultValue = "")]
    public Nullable<int> subPrice { get; set; }
}
