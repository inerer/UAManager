using System;
using System.IO.Compression;
using System.Threading.Tasks;
using UAM.Core.Api;
using UAM.Core.SaveInfo;
using Settings = UAM.Core.SaveInfo.AppSettings;

namespace UAM.Core.Installer;

public class Installer
{
    private readonly AppSettingsBase _appSettings = Settings.Get();
    private ApiUpdate ApiUpdate => new(_appSettings.ServerName.First());

    public async Task Install(string version)
    {
        // var arguments = Environment.GetCommandLineArgs();
        //
        // if (arguments.Length < 2)
        //     throw new ArgumentNullException("version not specified");
        Thread.Sleep(2000);

        await ApiUpdate.GetUpdate(version);

        var path = Environment.CurrentDirectory;

        var files = Directory.GetFiles(path);

        var directories = Directory.GetDirectories(path);

        foreach (var file in files)
        {
            if (file.Contains("LoadLauncher"))
                continue;
        
            if (file.EndsWith(".dll"))
                continue;
            
            if(file.EndsWith(".pdb"))
                continue;
            
            if(file.EndsWith(".json"))
                continue;
        
            File.Delete(file);
        }
        
        foreach (var directory in directories)
        {
            if (directory.Contains("updates"))
                continue;
        
            var filesFromDirectory = Directory.GetFiles(directory);
        
            foreach (var file in filesFromDirectory)
            {
                if (file.EndsWith(".dll"))
                    continue;
        
                File.Delete(file);
            }
        
            if (filesFromDirectory.Length == 0)
                Directory.Delete(directory);
        }

        ZipFile.ExtractToDirectory($"updates/{version}.zip",
            path,
            true);
    }

    public async Task InstallLastUpdate()
    {
        await ApiUpdate.GetLastUpdate();
        var path = Environment.CurrentDirectory;

        var files = Directory.GetFiles(path);

        var directories = Directory.GetDirectories(path);

        foreach (var file in files)
        {
            if (file.EndsWith(".exe"))
                continue;

            if (file.EndsWith(".dll"))
                continue;

            File.Delete(file);
        }

        foreach (var directory in directories)
        {
            if (directory.EndsWith("updates"))
                continue;

            foreach (var file in Directory.GetFiles(directory))
            {
                File.Delete(file);
            }

            Directory.Delete(directory);
        }

        ZipFile.ExtractToDirectory("updates/1.0.0.zip", path);
    }
}