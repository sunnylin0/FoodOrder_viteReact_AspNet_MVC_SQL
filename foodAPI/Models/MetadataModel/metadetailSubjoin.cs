using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace foodAPI.Models 
{
    [MetadataType(typeof(z_metadetailSubjoin))]
    public partial class detailSubjoin
    {
    }
}

public abstract class z_metadetailSubjoin
{
    [Key]
    public int dsId { get; set; }
    [Display(Name = "屬性名稱")]
    [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:N0}")]
    [Columns(CheckBox = false , Hidden = false , DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Int_0, DefaultValue = "")]
    public Nullable<int> detailId { get; set; }
    [Display(Name = "屬性名稱")]
    [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:N0}")]
    [Columns(CheckBox = false , Hidden = false , DropdownClass = "")]
    [Default(DefaultValueType = enDefaultValueType.Int_0, DefaultValue = "")]
    public Nullable<int> subId { get; set; }
}
