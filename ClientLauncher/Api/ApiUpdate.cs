using System.IO;
using System.Text.Json;
using System.Windows;
using Version = ClientLauncher.Models.Version;

namespace ClientLauncher.Api;

public class ApiUpdate : ApiBase
{
    public async Task<List<Version>> GetAllVersions()
    {
        var client = HttpClient;

        var res = await client.GetStringAsync("api/Update/GetAllVersions");

        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<List<Version>>(res, options)!;
    }

    public async Task GetUpdate(string build)
    {
        var client = HttpClient;

        var filePath = "updates";

        var res = await client.GetAsync($"api/Update/GetUpdate?build={build}");

        if (res.IsSuccessStatusCode)
        {
            Stream fileStream = await res.Content.ReadAsStreamAsync();
            SaveStreamAsFile(filePath, fileStream, $"{build}.zip");
        }
        else
        {
            throw new Exception("Ошибка при получении файла");
        }
    }

    private void SaveStreamAsFile(string filePath, Stream inputStream, string fileName)
    {
        DirectoryInfo info = new DirectoryInfo(filePath);
        if (!info.Exists)
        {
            info.Create();
        }

        string path = Path.Combine(filePath, fileName);
        using FileStream outputFileStream = new FileStream(path, FileMode.Create);
        inputStream.CopyTo(outputFileStream);
    }
}