using System.ComponentModel.DataAnnotations;

namespace EMT.BLL.DTOs
{
    public class PurchaseRequestDto
    {
        [Required]
        public int UserId { get; set; } 

        [Required]
        [Range(1,1000000)] // A security range to avoid 0, null, or big numbers
        public decimal AmmountARS { get; set; } 

        [Required]
        public string IsoCurrencyCode { get; set; } 
    }
}