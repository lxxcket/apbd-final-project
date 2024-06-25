using APBDFinalProject.Exceptions;
using Newtonsoft.Json.Linq;

namespace APBDFinalProject.Helpers;

public static class CurrencyConversionHelper
{
    private static readonly string API_CALL =
        "https://v6.exchangerate-api.com/v6/4bef60de43e080d482884712/pair";

    private static readonly HttpClient _httpClient = new HttpClient();

    public static async Task<(string targetCode, decimal conversionResult)> ConvertCurrency(string targetCurrency,
        decimal amount)
    {
        try
        {
            string amountString = Convert.ToString(amount);
            amountString = amountString.Replace(",", ".");
            string apiUrl = $"{API_CALL}/PLN/{targetCurrency}/{amountString}";
            Console.WriteLine(apiUrl);
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(json);

                string targetCode = data["target_code"].ToString();
                decimal conversionResult = (decimal)data["conversion_result"];

                return (targetCode, conversionResult);
            }
            else
            {
                throw new DomainException($"API call failed with status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            
            throw new DomainException("Error occurred while calling the API");
        }
    }
}