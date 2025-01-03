using Httpclient.AppSettings.Teamplates;

namespace Httpclient.AppSettings.Reader;

public interface IJsonReaderForConfig
{
    T Read<T>(string filePath) where T : ITemplates;
}