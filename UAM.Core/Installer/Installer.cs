using System;
using System.IO.Compression;
using System.Threading.Tasks;
using UAM.Core.Api;

namespace UAM.Core.Installer;

public class Installer
{
    private ApiUpdate _apiUpdate = new ApiUpdate();

    public async Task Install()
    {
        // var arguments = Environment.GetCommandLineArgs();
        //
        // if (arguments.Length < 2)
        //     throw new ArgumentNullException("version not specified");

        await _apiUpdate.GetUpdate("1.0.0");

        var path = "C:\\Users\\arshi\\RiderProjects\\UAManager\\ClientLauncher\\bin\\Release\\net8.0-windows";

        var files = Directory.GetFiles(path);

        var directories = Directory.GetDirectories(path);

        foreach (var file in files)
        {
            if (file.EndsWith("ClientLauncher.exe"))
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

        ZipFile.ExtractToDirectory("updates/1.0.0", path);
    }
}