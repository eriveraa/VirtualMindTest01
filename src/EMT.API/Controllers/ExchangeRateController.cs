using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using EMT.BLL.Services;
using EMT.Common;
using EMT.API.ApiUtils;
using EMT.BLL.DTOs;
using Microsoft.AspNetCore.Http;
using EMT.Common.ResponseWrappers;

namespace EMT.API.Controllers
{
    public class ExchangeRateController: BaseApiController
    {
        private readonly ICurrencyManagerService _service;

        public ExchangeRateController(IOptionsSnapshot<MyAppConfig> myAppConfig, ILogger<ExchangeRateController> logger, ICurrencyManagerService service) 
                                        : base(myAppConfig, logger)
        {
            _service = service;
        }

        [HttpGet("getexchangetoars/{isoCurrencyCode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseResult<GetExchangeRateResponseDto>>> Get(string isoCurrencyCode)
        {
            var response = await _service.GetExchangeRateByIsoCode(isoCurrencyCode);
            return Ok(response);
        }

        [HttpPost("purchase")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseResult<PurchaseResponseDto>>> Post([FromBody] PurchaseRequestDto purchaseRequest)
        {
            var response = await _service.MakePurchase(purchaseRequest);
            return Created("", response);
        }
    }
}
