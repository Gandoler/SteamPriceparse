using Httpclient;
using Newtonsoft.Json;
using Serilog;

public class GetItemPriceClass : IGetItemPrice
{
    // Статический HttpClient для повторного использования
    private static readonly HttpClient client = new HttpClient();

    // Метод для получения цены предмета
    public async Task<SteamItemPrice> GetItemPriceAsync(string appId, string marketHashName,int moneyType)
    {
        try
        {
           
            var baseUrl = "https://steamcommunity.com/market/priceoverview/";
            var url = $"{baseUrl}?appid={appId}&market_hash_name={Uri.EscapeDataString(marketHashName)}&currency={moneyType}";

            // Выполняем запрос к API
            var response = await client.GetStringAsync(url);

            // Десериализация ответа в объект SteamItemPrice
            var priceData = JsonConvert.DeserializeObject<SteamItemPrice>(response);

            // Проверяем, что данные были получены
            if (priceData != null)
            {
                Log.Information($"Цена для предмета {marketHashName} успешно получена.");
                return priceData;
            }
            else
            {
                // Логируем предупреждение, если цена не найдена
                Log.Warning($"Цена для предмета {marketHashName} не найдена.");
                throw new ArgumentException($"Цена для предмета {marketHashName} не найдена.");
            }
        }
        catch (Exception ex)
        {
            // Логируем ошибку с дополнительной информацией
            Log.Error($"Ошибка при запросе цены для предмета {marketHashName}. {ex.Message} в методе {nameof(GetItemPriceAsync)}.");
            throw new ArgumentException("Не удалось получить цену для предмета.", ex);
        }
    }
}
