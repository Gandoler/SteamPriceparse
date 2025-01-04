using Newtonsoft.Json;

namespace Httpclient.AppSettings.Teamplates;

public class RootConfig: ITemplates
{
    [JsonProperty("serverSettings")] // Укажите правильное имя из JSON
    public serverSetings ServerSettings { get; set; } = new serverSetings();

    


    public string GetConnectionString()
    {
        return ServerSettings?.ToString();
    }
   
}