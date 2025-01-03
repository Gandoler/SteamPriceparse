namespace Httpclient;

public interface IGetItemPrice
{
       Task<SteamItemPrice> GetItemPriceAsync(string appId, string marketHashName,int moneyType);
}