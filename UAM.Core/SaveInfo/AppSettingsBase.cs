namespace UAM.Core.SaveInfo;

public class AppSettingsBase
{
    public List<string> ServerName { get; set; } = null!;

    public bool AutoCheckUpdates { get; set; }
    
    public bool StopAutoCheckWhenErrors { get; set; }
    
    public bool UseArchiver { get; set; }
}