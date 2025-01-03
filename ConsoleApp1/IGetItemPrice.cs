namespace ConsoleApp1;

public interface IGetItemPrice
{
       Task<SteamItemPrice> GetItemPriceAsync(string appId, string marketHashName);
}