using System.Configuration;
using System.Data;
using System.Windows;
using UAM.Core.Installer;

namespace LoadLauncher;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private async void App_OnStartup(object sender, StartupEventArgs e)
    {
        Installer installer = new Installer();
        await installer.Install();
    }
}