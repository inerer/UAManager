using System.Windows;
using System.Windows.Controls;

namespace ClientLauncher.Pages;

public partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new SettingsPage());
    }

    private void TicketListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        throw new NotImplementedException();
    }
}