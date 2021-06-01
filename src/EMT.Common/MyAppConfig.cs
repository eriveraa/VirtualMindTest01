using System.Collections.Generic;

namespace EMT.Common
{
    public class MyAppConfig
    {
        public Dictionary<string, ExchangeToARSItem> ExchangeToARS { get; set; }
    }

    public class ExchangeToARSItem
    {
        public string Url { get; set; }
        public decimal MaxPurchasePerMonth { get; set; }
    }
}
