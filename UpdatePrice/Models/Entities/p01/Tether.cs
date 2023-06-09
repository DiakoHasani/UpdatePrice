//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UpdatePrice.Models.Entities.p01
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tether
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tether()
        {
            this.Transactions = new HashSet<Transaction>();
        }
    
        public int Id { get; set; }
        public string Address { get; set; }
        public double Amount { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int Type { get; set; }
        public int UserId { get; set; }
        public decimal PurchasePrice { get; set; }
        public int Status { get; set; }
        public string Txid { get; set; }
        public Nullable<int> CryptoWalletId { get; set; }
    
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
