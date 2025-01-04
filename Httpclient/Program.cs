using System;
using System.Net.Http;
using System.Threading.Tasks;
using Dbtools.Data;
using Httpclient;
using Newtonsoft.Json;
using Serilog;

public class Program
{
    private static readonly HttpClient client = new HttpClient();

    public static async Task Main(string[] args)
    { 
        
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()  
            
            .WriteTo.File("/Users/gl.krutoimail.ru/RiderProjects/Solution1/Httpclient/logs/log.log", rollingInterval: RollingInterval.Hour) 
            .CreateLogger();
        Log.Information("Starting App\n" +
                        "#######################");
        
        string appId = "730"; 
        string marketHashName = "AK-47 | Redline (Field-Tested)";
        GetItemPriceClass getItemPriceClass = new GetItemPriceClass();
        
        var priceInfo = await getItemPriceClass.GetItemPriceAsync(appId, marketHashName, 7);

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

        using (var db = new DbContextSteam())
        {
           
            await db.Database.EnsureCreatedAsync();
        }

    }

    
}