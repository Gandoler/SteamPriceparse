using System;
using System.Net.Http;
using System.Threading.Tasks;
using ConsoleApp1;
using Newtonsoft.Json;
using Serilog;

public class Program
{
    private static readonly HttpClient client = new HttpClient();

    public static async Task Main(string[] args)
    { 
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()  // Уровень логирования
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Hour) 
            .CreateLogger();

        string appId = "730"; // ID игры CS:GO
        string marketHashName = "AK-47 | Redline (Field-Tested)"; // Имя предмета
        GetItemPriceClass getItemPriceClass = new GetItemPriceClass();
        
        var priceInfo = await getItemPriceClass.GetItemPriceAsync(appId, marketHashName);

        if (priceInfo.success)
        {
            Console.WriteLine($"Цена на {marketHashName}:");
            Console.WriteLine($"  Минимальная цена: {priceInfo.lowest_price}");
            Console.WriteLine($"  Объем продаж: {priceInfo.volume}");
            Console.WriteLine($"  Средняя цена: {priceInfo.median_price}");
        }
        else
        {
            Console.WriteLine("Ошибка получения данных.");
        }
    }

    
}