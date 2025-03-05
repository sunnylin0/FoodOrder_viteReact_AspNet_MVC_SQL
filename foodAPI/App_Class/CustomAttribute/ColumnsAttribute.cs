using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 設定欄位客製化屬性
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class ColumnsAttribute : Attribute
{
	/// <summary>
	/// 隱藏欄位
	/// </summary>
	public bool Hidden { get; set; } = false;
	/// <summary>
	/// 唯讀欄位
	/// </summary>
	public bool Readonly { get; set; } = false;
	/// <summary>
	/// 複選欄位
	/// </summary>
	public bool CheckBox { get; set; } = false;
	/// <summary>
	/// 下拉式選單使用的 Class Name
	/// </summary>
	public string DropdownClass { get; set; } = "";

	/// <summary>
	/// 欄位屬性設定
	/// </summary>
	public ColumnsAttribute()
	{
	}
}
