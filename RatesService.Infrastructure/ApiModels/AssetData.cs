
namespace RatesService
{
   public class AssetData
    {
        public string Symbol { get; set; }
        public Dictionary<string, RateDetail> Quote { get; set; }
    }
}
