using System;
using System.IO.Compression;
using System.Threading.Tasks;
using UAM.Core.Api;

namespace UAM.Core.Installer;

public class Installer
{
    private readonly ApiUpdate _apiUpdate = new ApiUpdate();

    public async Task Install()
    {
        var arguments = Environment.GetCommandLineArgs();

        if (arguments.Length < 2)
            throw new ArgumentNullException("version not specified");

        await _apiUpdate.GetUpdate(arguments[1]);

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

    public async Task InstallLastUpdate()
    {
        await _apiUpdate.GetLastUpdate();
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