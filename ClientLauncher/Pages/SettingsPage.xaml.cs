using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;
using UAM.Core;
using UAM.Core.Api;
using UAM.Core.Installer;
using Path = System.IO.Path;
using Version = ClientLauncher.Models.Version;


namespace ClientLauncher.Pages;

public partial class SettingsPage : Page
{
    private ApiUpdate _apiUpdate = new ApiUpdate();

    public SettingsPage()
    {
        InitializeComponent();
        GetListAllVersions();
    }

    private void VersionsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = (UAM.Core.Models.Version)VersionsListView.SelectedItem;
        System.Diagnostics.Process.Start("LoadLauncher.exe", selectedItem.Build);
        Application.Current.Shutdown();
    }

    private async void GetListAllVersions()
    {
        var versions = await _apiUpdate.GetAllVersions();
        VersionsListView.ItemsSource = versions;
    }
}