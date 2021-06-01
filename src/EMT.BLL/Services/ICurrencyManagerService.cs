using System.Threading.Tasks;
using EMT.BLL.DTOs;
using EMT.Common.ResponseWrappers;

namespace EMT.BLL.Services
{
    public interface ICurrencyManagerService
    {
        Task<BaseResult<GetExchangeRateResponseDto>> GetExchangeRateByIsoCode(string isoCurrencyCode);
        Task<BaseResult<PurchaseResponseDto>> MakePurchase(PurchaseRequestDto purchaseRequest);
    }
}
