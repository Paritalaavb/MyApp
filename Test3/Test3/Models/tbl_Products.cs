//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Test3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Products()
        {
            this.tbl_OrderDetails = new HashSet<tbl_OrderDetails>();
        }
    
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string note { get; set; }
        public Nullable<int> Units { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_OrderDetails> tbl_OrderDetails { get; set; }
    }
}
