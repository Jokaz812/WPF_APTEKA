//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApteka.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class INVOICE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public INVOICE()
        {
            this.INVOICE_LIST = new HashSet<INVOICE_LIST>();
        }
    
        public int ID_I { get; set; }
        public string NAME { get; set; }
        public Nullable<System.DateTime> BDATE { get; set; }
        public Nullable<int> ID_S { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INVOICE_LIST> INVOICE_LIST { get; set; }
        public virtual SUPPLIER SUPPLIER { get; set; }
    }
}