using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace UAM.Core.SaveInfo;

public class AppSettings
{
    private static string AppSettingsPath => Environment.CurrentDirectory + "/appsettings.json"; 

    private static AppSettingsBase DefaultSettings => new()
    {
        ServerName = new List<string>{"http://5.188.141.251/"},
        AutoCheckUpdates = true,
        StopAutoCheckWhenErrors = true,
        UseArchiver = false
    };

    public static void SetAppSettingsDefault()
    {
        Set(DefaultSettings);
    }

    public static AppSettingsBase Get()
    {
        try
        {
            return JsonSerializer.Deserialize<AppSettingsBase>(File.ReadAllText(AppSettingsPath))!;
        }
        catch
        {
            SetAppSettingsDefault();
            return JsonSerializer.Deserialize<AppSettingsBase>(File.ReadAllText(AppSettingsPath))!;
        }
    }
    
    public static void Set(AppSettingsBase appSettings)
    {
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
        };
        
        var json = JsonSerializer.Serialize(appSettings, options);
        File.WriteAllText(AppSettingsPath, json);
    }
}