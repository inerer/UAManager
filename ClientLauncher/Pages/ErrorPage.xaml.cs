using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using UAM.Core.EmailException;

namespace ClientLauncher.Pages;

public partial class ErrorPage : Page
{
    private string _error;
    private string _email;

    public ErrorPage(string error)
    {
        InitializeComponent();
        _error = error;
    }

    private async void RestartButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (EmailTextBox.Text != string.Empty)
        {
            if (!new EmailAddressAttribute().IsValid(EmailTextBox.Text))
            {
                MessageBox.Show("Неверный формат почты");
                return;
            }

            _email = EmailTextBox.Text;
        }

        await ExceptionSender.SendEmailAsync(_error, _email);


        NavigationService.Navigate(new SettingsPage());
    }

    private async void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (EmailTextBox.Text != string.Empty)
        {
            if (!new EmailAddressAttribute().IsValid(EmailTextBox.Text))
            {
                MessageBox.Show("Неверный формат почты");
                return;
            }

            _email = EmailTextBox.Text;
        }

        await ExceptionSender.SendEmailAsync(_error, _email);

        Application.Current.Shutdown();
    }
}