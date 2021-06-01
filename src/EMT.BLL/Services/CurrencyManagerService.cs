using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using EMT.Common;
using EMT.Common.ResponseWrappers;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using EMT.BLL.DTOs;
using EMT.DAL.EF;

namespace EMT.BLL.Services
{
    public class CurrencyManagerService : BaseService, ICurrencyManagerService
    {
        private readonly IHttpClientFactory _clientFactory;

        public CurrencyManagerService(IOptionsSnapshot<MyAppConfig> myAppConfig, ILogger<CurrencyManagerService> logger,
            ApplicationDbContext context, IHttpClientFactory clientFactory) 
            : base(myAppConfig, logger, context)
        {
            _clientFactory = clientFactory;
        }

        public async Task<BaseResult<GetExchangeRateResponseDto>> GetExchangeRateByIsoCode(string isoCurrencyCode)
        {
            _logger.LogInformation("*** {Method} {id}", "GetExchangeRateByIsoCode", isoCurrencyCode);         

            // Return the data
            var ret = new BaseResult<GetExchangeRateResponseDto>() { 
                        Data = new GetExchangeRateResponseDto { 
                            IsoCurrencyCode = isoCurrencyCode, 
                            Ammount = await FetchFromServerByCode(isoCurrencyCode) } 
                        };
            return ret;
        }

        public async Task<BaseResult<PurchaseResponseDto>> MakePurchase(PurchaseRequestDto purchaseRequest)
        {
            DateTime now = DateTime.UtcNow;
            var ret = new BaseResult<PurchaseResponseDto>();

            // Get the current month transactions for the user
            var query =
                    (from t in _context.Purchase
                     where t.UserId == purchaseRequest.UserId && t.IsoCurrencyCode == purchaseRequest.IsoCurrencyCode
                        && t.PurchaseDateTime.Year == now.Year && t.PurchaseDateTime.Month == now.Month
                     group t by new { t.UserId, t.IsoCurrencyCode, Year = t.PurchaseDateTime.Year, Month = t.PurchaseDateTime.Month } into grp
                     select new
                     {
                         TotalPurchased = grp.Sum(t => t.PurchaseAmmount)
                     }).SingleOrDefault();

            float exchange = await FetchFromServerByCode(purchaseRequest.IsoCurrencyCode);
            purchaseRequest.AmmountARS = Math.Round(purchaseRequest.AmmountARS, 2, MidpointRounding.ToZero);  // Security rounding
            decimal purchaseAmmount = Math.Round(purchaseRequest.AmmountARS / (decimal)exchange, 2, MidpointRounding.ToZero); // Security rounding
            decimal maxPurchasePerMonth = _myAppConfig.ExchangeToARS[purchaseRequest.IsoCurrencyCode].MaxPurchasePerMonth;
            decimal currentTotalPurchased = purchaseAmmount + (query?.TotalPurchased ?? 0);

            // User does not have any purchases this month
            if (query == null && (purchaseAmmount > maxPurchasePerMonth))
            {
                throw new Exception($"Transaction denied. You are trying to purchase {purchaseAmmount} which is more than allowed ({maxPurchasePerMonth}).");
            }
            else if (query != null && (currentTotalPurchased) > maxPurchasePerMonth)
            {
                throw new Exception($"Transaction denied. You are trying to purchase {purchaseAmmount} plus your previous month purchases {query?.TotalPurchased ?? 0} will exceed the {maxPurchasePerMonth} limit.");
            }

            // Save the transaction
            var newPurchase = new PurchaseResponseDto
            {
                AmmountARS = purchaseRequest.AmmountARS,
                IsoCurrencyCode = purchaseRequest.IsoCurrencyCode,
                PurchaseAmmount = purchaseAmmount,
                PurchaseDateTime = now,
                UserId = purchaseRequest.UserId
            };
            _context.Purchase.Add(newPurchase);
            await _context.SaveChangesAsync();

            ret.Data = newPurchase;
            ret.Data.TotalPurchasesAmmount = currentTotalPurchased;
            ret.Data.Exchange = (decimal)exchange;

            return ret;
        }

        private async Task<float> FetchFromServerByCode(string isoCurrencyCode)
        {
            // Simple cleaning of the iso code
            isoCurrencyCode = isoCurrencyCode.Trim().ToUpper();

            // Verify if the currency-code exists
            if (!_myAppConfig.ExchangeToARS.ContainsKey(isoCurrencyCode))
            {
                throw new Exception($"The currency-code {isoCurrencyCode} does not exist or it is not implemented yet.");
            }

            // Check the country
            if (isoCurrencyCode == "USD")
            {
                return await GetExchangeForUSD();
            }
            else if (isoCurrencyCode == "BRL")
            {
                return (await GetExchangeForUSD()) * 0.25f;
            }
            // Add another countries here....
            else
                return 0;
        }

        private async Task<float> GetExchangeForUSD()
        {
            float valueToReturn = 0;

            // Get the data from the API endpoint
            var request = new HttpRequestMessage(HttpMethod.Get, _myAppConfig.ExchangeToARS["USD"].Url);
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    // Serialize the fetched data
                    var rawData = await JsonSerializer.DeserializeAsync<List<string>>(responseStream);
                    valueToReturn = float.Parse(rawData[0]); // The first element in the array is the Buy-Price
                };
            }
            else
            {
                throw new Exception("Error when trying to get data from converter service.");
            }

            return valueToReturn;
        }
    }
}
