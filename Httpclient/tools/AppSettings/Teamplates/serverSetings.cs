using Serilog;

namespace Httpclient.AppSettings.Teamplates;

public class serverSetings : ITemplates
{
    public string Server { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Database { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string GetConnectionString()
    {
        if (string.IsNullOrWhiteSpace(Server) ||
            string.IsNullOrWhiteSpace(Database) ||
            string.IsNullOrWhiteSpace(User) ||
            string.IsNullOrWhiteSpace(Password))
        {
            Log.Information("ServerSettings: now in template:" + this.ToString());
            Log.Error("ServerSettings:One or more required fields are missing for the connection string.");
               
        }
            
        return $"Server={Server};Port={Port};Database={Database};User Id={User};Password={Password};";
    }
   
}