using Dbtools.models;
using Serilog;

namespace Httpclient.tools.consoleParse;

public static class StringParseClass
{



    public static SteamItem GetSteamItemWeaponFromString(string consoleInput)
    {
        List<string> items = consoleInput.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
        if (items.Count != 3)
        {
            Log.Error("Invalid number of items");
            throw new FormatException("Invalid number of items");
        }
        SteamItem steamItem = new SteamItem
        {
            ItemType = items[0],
            ItemName = items[1],
            Quality = Enum.TryParse<ItemQuality>(items[2], out var quality) && 
                       (Convert.ToInt32(quality) == 0 || Convert.ToInt32(quality) >= 7)
                ? quality 
                : throw new FormatException("Invalid item quality")
        };
        bool exists = await CheckItemExistsAsync(steamItem.ItemName);
        if (!exists)
        {
            throw new ArgumentException($"Item '{steamItem.ItemName}' does not exist in Steam database.");
        }
        return steamItem;
       
    }

    private static async Task<bool> CheckItemExistsAsync(string itemName)
    {
        try
        {
            string apiUrl = $"https://api.steampowered.com/ISteamEconomy/GetItemDetails/v1/?key=YOUR_API_KEY&item_name={Uri.EscapeDataString(itemName)}";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                // Разберите JSON-ответ и определите, существует ли предмет
                using JsonDocument doc = JsonDocument.Parse(responseContent);
                bool exists = doc.RootElement.GetProperty("result").GetProperty("exists").GetBoolean();
                return exists;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error checking item existence: {ex.Message}");
        }

        return false;
        
        
    }
}