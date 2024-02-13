using System.Windows;
using System.Windows.Controls;
using ClientLauncher.Enteties;
using Microsoft.EntityFrameworkCore;

namespace ClientLauncher.Pages;

public partial class AuthPage : Page
{
    private TicketCreatorContext _ticketCreatorContext;

    public AuthPage()
    {
        _ticketCreatorContext = new TicketCreatorContext();
        InitializeComponent();
    }

    private async void LoginButton_OnClick(object sender, RoutedEventArgs e)
    {
        var worker = await _ticketCreatorContext.Workers.FirstOrDefaultAsync(w => w.Username == LoginTextBox.Text);

        if (worker == null || worker.Password != PasswordBox.Password)
            throw new Exception("Неверный логин или парооль");

        NavigationService.Navigate(new MainPage(worker));
    }

    private void RegButton_OnClickButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
}