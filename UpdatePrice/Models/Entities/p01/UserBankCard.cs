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
    
    public partial class UserBankCard
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string CardNumber { get; set; }
        public string Shaba { get; set; }
        public int Status { get; set; }
        public int BankID { get; set; }
        public int UserID { get; set; }
        public string FirstSeprateCardNumber { get; set; }
        public string LastSeprateCardNumber { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}
