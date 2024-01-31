using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UAM.Core.Installer;
using Path = System.IO.Path;

namespace LoadLauncher;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var installThread = new Thread(Install);
        installThread.Start();
    }

    private static async void Install()
    {
        var arguments = Environment.GetCommandLineArgs();
        var installer = new Installer();
        await installer.Install(arguments[1]);
        Application.Current.Dispatcher.Invoke(() =>
        {
            var path = "ClientLauncher.exe";
            System.Diagnostics.Process.Start(path);

            Application.Current.Shutdown();
        });
    }
}