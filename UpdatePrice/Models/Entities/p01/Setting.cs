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
    
    public partial class Setting
    {
        public int Id { get; set; }
        public string SendUserAuthenticationDocumentsText { get; set; }
        public string HelpPage { get; set; }
        public string SupportPhone { get; set; }
        public decimal MinimumAmountWithdraw { get; set; }
        public int MobileConfirmCodeExpireMinutes { get; set; }
        public int EmailConfirmCodeExpireMinutes { get; set; }
    }
}
