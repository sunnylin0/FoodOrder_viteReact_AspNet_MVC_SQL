//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace foodAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class detail
    {
        public int detailId { get; set; }
        public string orderId { get; set; }
        public string menuId { get; set; }
        public string menuName { get; set; }
        public Nullable<int> price { get; set; }
        public Nullable<int> subPrice { get; set; }
        public Nullable<int> qty { get; set; }
        public string remark { get; set; }
    }
}
