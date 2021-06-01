using EMT.Common.Entities;

namespace EMT.BLL.DTOs
{
    public class PurchaseResponseDto : Purchase
    {
        public decimal TotalPurchasesAmmount { get; set; }
        public decimal Exchange { get; set; }
        
    }
}