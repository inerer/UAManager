using System.Windows;
using System.Windows.Controls;
using ClientLauncher.Enteties;
using Microsoft.EntityFrameworkCore;

namespace ClientLauncher.Pages;

public partial class MainPage : Page
{
    private TicketCreatorContext _ticketCreatorContext = new TicketCreatorContext();
    private Worker _worker;

    public MainPage(Worker worker)
    {
        InitializeComponent();
       
        _worker = worker;
        ListViewRendered();
    }

    private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new SettingsPage());
    }

    private void TicketListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void AddTicketButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new AddNewUserPage(_worker));
    }

    private void AddSubscription_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private  void ListViewRendered()
    {
        var tickets =  _ticketCreatorContext.Tickets.Where(t => t.WorkerId == _worker.Id)
            .Include(t => t.ClientInfo)
            .Include(t => t.Worker)
            .Include(t => t.Rate)
            .Include(t => t.Train)
            .Include(t => t.TypeShipping).ToList();

        
        TicketsListView.ItemsSource = tickets;
    }

    private void EditButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var selectedItem = (Ticket)TicketsListView.SelectedItem;
            NavigationService.Navigate(new AddNewTicketPage(selectedItem, _worker));
        }
        catch (Exception exception)
        {
            NavigationService.Navigate(new ErrorPage(exception.Message));
        }
        
    }

    private async void DeleteButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var selectedItem = (Ticket)TicketsListView.SelectedItem;
            _ticketCreatorContext.Tickets.Remove(selectedItem);
            await _ticketCreatorContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            NavigationService.Navigate(new ErrorPage(exception.Message));
        }
       
    }

    private void PrintDocumentButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var selectedItem = (Ticket)TicketsListView.SelectedItem;
            DocX docX = new DocX();
            docX.GenerateProtocol(selectedItem);
        }
        catch (Exception exception)
        {
            NavigationService.Navigate(new ErrorPage(exception.Message));
        }
        
    }
}