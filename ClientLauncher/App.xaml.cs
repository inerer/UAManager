using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using ClientLauncher.Command;
using ClientLauncher.Views;
using ModernWpf.Controls;
using UAM.Core.Api;
using UAM.Core.SaveInfo;

namespace ClientLauncher;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private async void Test()
    {
        var api = new ApiUpdate("https://localhost:7206/");
        var lastVersion = await api.GetLastUpdate();
        Process.Start("LoadLauncher.exe", lastVersion.ToString());
        Current.Shutdown();
    }

    private CommandHandler ch => new(_ => Test());

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var settings = AppSettings.Get();
        if (!settings.AutoCheckUpdates) return;
        try
        {
            var api = new ApiUpdate("https://localhost:7206/");
            var lastVersion = await api.GetLastUpdate();
            var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;

            if (currentVersion < lastVersion)
            {
                ContentDialog contentDialog = new ContentDialog
                {
                    Title = "Подтверждение",
                    Content = new ConfirmUserControl("У вас устаревшая версия. \n Хотите обновить на актуальную?"
                    ),
                    CloseButtonText = "Нет",
                    PrimaryButtonText = "Да",
                    PrimaryButtonCommand = ch
                    
                };
                await contentDialog.ShowAsync();
                
            }
        }
        catch
        {
            settings.AutoCheckUpdates = false;
            AppSettings.Set(settings);
        }
    }
}