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
    
    public partial class Voucher
    {
        public int Id { get; set; }
        public string Serial { get; set; }
        public string ActivationCode { get; set; }
        public double Volume { get; set; }
        public Nullable<int> PaymentServiceId { get; set; }
        public int UserID { get; set; }
        public string BatchNum { get; set; }
        public int VoucherType { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}
