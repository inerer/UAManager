using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;
using UAM.Core;
using UAM.Core.Api;
using UAM.Core.Installer;
using UAM.Core.SaveInfo;
using Path = System.IO.Path;
using Version = ClientLauncher.Models.Version;


namespace ClientLauncher.Pages;

public partial class SettingsPage : Page
{
    private readonly ApiUpdate _apiUpdate = new(AppSettings.Get().ServerName.First());
    private AppSettingsBase _appSettings;

    public SettingsPage()
    {
        try
        {
            InitializeComponent();
            GetListAllVersions();
            _appSettings = AppSettings.Get();
            AutoUpdateCheckBox.IsChecked = _appSettings.AutoCheckUpdates;
            StopAutoUpdateForErrorCheckBox.IsChecked = _appSettings.StopAutoCheckWhenErrors;
            ArchiveCheckBox.IsChecked = _appSettings.UseArchiver;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            NavigationService.Navigate(new ErrorPage(e.Message));
        }
        
    }

    private void VersionsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = (UAM.Core.Models.Version)VersionsListView.SelectedItem;
        System.Diagnostics.Process.Start("LoadLauncher.exe", selectedItem.Build);
        Application.Current.Shutdown();
    }

    private async void GetListAllVersions()
    {
        try
        {
            var versions = await _apiUpdate.GetAllVersions();
            VersionsListView.ItemsSource = versions;
        }
        catch (Exception e)
        {
            NavigationService.Navigate(new ErrorPage(e.Message));
        }
        
    }

    private void SaveSettingButton_OnClick(object sender, RoutedEventArgs e)
    {
        _appSettings.AutoCheckUpdates = (bool)AutoUpdateCheckBox.IsChecked!;
        _appSettings.StopAutoCheckWhenErrors = (bool)StopAutoUpdateForErrorCheckBox.IsChecked!;
        _appSettings.UseArchiver = (bool)ArchiveCheckBox.IsChecked!;
        AppSettings.Set(_appSettings);
    }

    private void ResetSettingButton_OnClick(object sender, RoutedEventArgs e)
    {
        AppSettings.SetAppSettingsDefault();
    }
}