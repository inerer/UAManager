using System.Windows;
using System.Windows.Controls;
using UAM.Core.Enteties;

namespace WorkerApp.Pages;

public partial class AuthPage : Page
{
    private readonly UaVersionsContext _uaVersionsContext = new UaVersionsContext();

    public AuthPage()
    {
        InitializeComponent();
    }

    private void LoginButton_OnClick(object sender, RoutedEventArgs e)
    {
        var worker = _uaVersionsContext.Workers.FirstOrDefault(w => w.FullName == LoginTextBox.Text);

        if (worker.Password != PasswordBox.Password)
            throw new Exception("Неверный пароль!");

        if (worker.RoleId == 1)
            NavigationService.Navigate(new ProblemEditPage());
        else
            NavigationService.Navigate(new WorkerPage(worker));
    }
}