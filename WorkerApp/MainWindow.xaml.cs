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
using Microsoft.EntityFrameworkCore;
using UAM.Core.Enteties;
using WorkerApp.Pages;

namespace WorkerApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private UaVersionsContext _context = new UaVersionsContext();

    public MainWindow()
    {
        InitializeComponent();
        ComboBoxRendered();
    }

    private void RoleComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var worker = (Worker)RoleComboBox.SelectedItem;

        if (worker.RoleId == 1)
            MainFrame.Navigate(new ProblemEditPage());
        else
            MainFrame.Navigate(new WorkerPage(worker));
    }

    private async void ComboBoxRendered()
    {
        RoleComboBox.ItemsSource = await _context.Workers.Include(w => w.Role).ToListAsync();
    }
}