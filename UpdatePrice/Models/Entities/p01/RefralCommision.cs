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
    
    public partial class RefralCommision
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal Percent { get; set; }
        public int RefralType { get; set; }
        public System.DateTime UpdateDate { get; set; }
    }
}
