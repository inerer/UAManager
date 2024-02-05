using System.Windows;
using System.Windows.Controls;


namespace ClientLauncher.Views;

public partial class ConfirmUserControl : UserControl
{
    
    public ConfirmUserControl( string message)
    {
        InitializeComponent();
        MessageTextBlock.Text = message;
    }
    
}