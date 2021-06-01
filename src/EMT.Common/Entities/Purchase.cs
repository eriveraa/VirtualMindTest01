using System;
using System.ComponentModel.DataAnnotations;

namespace EMT.Common.Entities
{
    public class Purchase
    {
        [Key]
        [Required]
        public int PurchaseId { get; set; } //  PK / int / 0 /  Not Nullable
        
        [Required] 
        public int UserId { get; set; } // int / 0 /  Not Nullable
        
        [Required] 
        public decimal AmmountARS { get; set; } // decimal / 0 /  Not Nullable

        [Required] 
        public string IsoCurrencyCode { get; set; } // varchar / 3 /  Not Nullable

        [Required] 
        public decimal PurchaseAmmount { get; set; }    // decimal / 0 /  Not Nullable

        [Required] 
        public DateTime PurchaseDateTime { get; set; }  // datetime / 0 /  Not Nullable
    }
}