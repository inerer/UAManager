using System.Windows;
using System.Windows.Controls;
using ClientLauncher.Enteties;
using Microsoft.EntityFrameworkCore;
using Worker = UAM.Core.Enteties.Worker;
using NewWorker = ClientLauncher.Enteties.Worker;

namespace ClientLauncher.Pages;

public partial class AddNewTicketPage : Page
{
    private Ticket _ticket;
    private ClientInfo _clientInfo;
    private NewWorker _worker;
    private TicketCreatorContext _ticketCreatorContext;

    public AddNewTicketPage(ClientInfo clientInfo, NewWorker worker)
    {
        InitializeComponent();
        _ticket = new Ticket();
        _clientInfo = clientInfo;
        _worker = worker;
        _ticketCreatorContext = new TicketCreatorContext();
        EditTicketButton.Visibility = Visibility.Collapsed;
        ComboBoxRendered();
        // this.DataContext = _ticket;
    }
    public AddNewTicketPage(Ticket ticket, NewWorker worker)
    {
        InitializeComponent();
        _ticket = ticket;
        _worker = worker;
        _ticketCreatorContext = new TicketCreatorContext();
        AddNewTicketButton.Visibility = Visibility.Collapsed;
        ComboBoxRendered();
        // this.DataContext = _ticket;
    }

    private async void AddNewTicketButton_OnClick(object sender, RoutedEventArgs e)
    {
        _ticket.CountStation = Convert.ToInt32(CountStationTextBox.Text);
        _ticket.ClientInfoId = _clientInfo.Id;
        _ticket.Train = (Train)TrainComboBox.SelectedItem;
        _ticket.TypeShipping = (TypeShipping)TypeShippingComboBox.SelectedItem;
        _ticket.Rate = (Rate)RateComboBox.SelectedItem;
        _ticket.DateDeparture = Convert.ToDateTime(DepartureDatePicker.SelectedDate).ToUniversalTime();
        _ticket.DateArrival = Convert.ToDateTime(ArrivalDatePicker.SelectedDate).ToUniversalTime();
        _ticket.WorkerId = _worker.Id;
        _ticket.Price = _ticket.CountStation * _ticket.Rate.PricePerStation;
        await _ticketCreatorContext.AddAsync(_ticket);
        await _ticketCreatorContext.SaveChangesAsync();
        NavigationService.Navigate(new MainPage(_worker));
    }

    private async void EditTicketButton_OnClick(object sender, RoutedEventArgs e)
    {
        _ticket.CountStation = Convert.ToInt32(CountStationTextBox.Text);
        _ticket.Train = (Train)TrainComboBox.SelectedItem;
        _ticket.TypeShipping = (TypeShipping)TypeShippingComboBox.SelectedItem;
        _ticket.Rate = (Rate)RateComboBox.SelectedItem;
        _ticket.DateDeparture = Convert.ToDateTime(DepartureDatePicker.SelectedDate).ToUniversalTime();
        _ticket.DateArrival = Convert.ToDateTime(ArrivalDatePicker.SelectedDate).ToUniversalTime();
        _ticket.Price = _ticket.CountStation * _ticket.Rate.PricePerStation;
        _ticketCreatorContext.Tickets.Update(_ticket);
       await _ticketCreatorContext.SaveChangesAsync();
       NavigationService.Navigate(new MainPage(_worker));
    }

    private async void ComboBoxRendered()
    {
        TrainComboBox.ItemsSource = await _ticketCreatorContext.Trains.ToListAsync();
        TypeShippingComboBox.ItemsSource = await _ticketCreatorContext.TypeShippings.ToListAsync();
        RateComboBox.ItemsSource = await _ticketCreatorContext.Rates.ToListAsync();
    }
}