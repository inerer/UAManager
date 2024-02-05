using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Version = UAM.Core.Models.Version;
using VersionSystem = System.Version;

namespace UAM.Core.Api;

public class ApiUpdate : ApiBase
{
    public ApiUpdate(string baseUrl) : base(baseUrl)
    {
    }

    public async Task<List<Models.Version>> GetAllVersions()
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

    public async Task GetUpdateById(Guid id)
    {
        var client = HttpClient;

        var filePath = "updates";

        var res = await client.GetAsync($"api/Update/GetUpdate?build={id}");

        if (res.IsSuccessStatusCode)
        {
            Stream fileStream = await res.Content.ReadAsStreamAsync();
            SaveStreamAsFile(filePath, fileStream, $"{id}.zip");
        }
        else
        {
            throw new Exception("Ошибка при получении файла");
        }
    }

    public async Task<VersionSystem> GetLastUpdate()
    {
        var client = HttpClient;

        var res = await client.GetStringAsync("api/Update/GetLastUpdate");

        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        return ConvertStringToVersion(JsonSerializer.Deserialize<Version>(res, options)!.Build);
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
    
    private VersionSystem ConvertStringToVersion(string version)
    {
        int major, minor, build, revision = 0;

        var splitVersion = version.Split(".");

        if (splitVersion.Length is > 4 or < 3)
            throw new Exception("Wrong version view");

        if (int.TryParse(splitVersion[0], out int parseMajor))
            major = parseMajor;
        else
            throw new Exception("Wrong version view");

        if (int.TryParse(splitVersion[1], out int parseMinor))
            minor = parseMinor;
        else
            throw new Exception("Wrong version view");

        if (int.TryParse(splitVersion[2], out int parseBuild))
            build = parseBuild;
        else
            throw new Exception("Wrong version view");

        if (splitVersion.Length == 4)
        {
            if (!int.TryParse(splitVersion[2], out int parseRevision))
                throw new Exception("Wrong version view");

            revision = parseRevision;
            return new VersionSystem(major, minor, build, revision);
        }

        return new VersionSystem(major, minor, build);
    }
}