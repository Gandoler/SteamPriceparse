using Httpclient.AppSettings.Reader;
using Httpclient.AppSettings.Teamplates;
using Serilog;

namespace Httpclient.AppSettings;

public class ConnectionStringManager
{
    static RootConfig rootConfig = new();
    static string ConfigFilePath => "appsettings.json";
    static ConnectionStringManager()
    {
        GoDeserialiseObject();
    }
    private static void GoDeserialiseObject()
    {
          
        JsonReaderForConfig jsonReader = new();
        try
        {
            rootConfig = jsonReader.Read<RootConfig>(ConfigFilePath);
            Log.Information($"ConnectionStringManager: settings:{rootConfig}");
        }
        catch (Exception ex)
        {
            Log.Error($"In ConnectionStringManager {ex.Message}");
        }
            
    }

    public static string? GetConnectionString()
    {
        return rootConfig.ServerSettings.GetConnectionString();
        return null;
    }
  
}