using System.Windows;
using System.Windows.Controls;
using ClientLauncher.Enteties;
using Microsoft.EntityFrameworkCore;

namespace ClientLauncher.Pages;

public partial class AddNewUserPage : Page
{
    private ClientInfo _clientInfo;
    private TicketCreatorContext _ticketCreatorContext = new TicketCreatorContext();
    private PassportInfo _passportInfo;
    private Worker _worker;

    public AddNewUserPage(Worker worker)
    {
        InitializeComponent();
        _clientInfo = new ClientInfo();
        // _ticketCreatorContext = new TicketCreatorContext();
        _passportInfo = new PassportInfo();
        _worker = worker;
        ComboBoxRendered();
        this.DataContext = _clientInfo;
    }

    private async void NextButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (FirstNameBox.Text == string.Empty || LastNameBox.Text == string.Empty ||
            MiddleNameBox.Text == string.Empty || SeriesPassport.Text == string.Empty ||
            NumberPassport.Text == string.Empty)
        {
            MessageBox.Show("Строка пустая");
            return;
        }


        var time = Convert.ToDateTime(DateOfBirthDatePicker.SelectedDate);

        _passportInfo.Series = SeriesPassport.Text;

        _passportInfo.Number = NumberPassport.Text;

        _passportInfo.DateOfBirth = time.ToUniversalTime();
        _passportInfo.Citizenship = (Citizenship)CitizenshipComboBox.SelectedItem;

        _clientInfo.PassportInfo = _passportInfo;

        await _ticketCreatorContext.ClientInfos.AddAsync(_clientInfo);
        await _ticketCreatorContext.SaveChangesAsync();

        var client =
            _ticketCreatorContext.ClientInfos.FirstOrDefault(cl => cl.PassportInfo.Series == _passportInfo.Series);

        NavigationService.Navigate(new AddNewTicketPage(client, _worker));
    }

    private async void ComboBoxRendered()
    {
        CitizenshipComboBox.ItemsSource = await _ticketCreatorContext.Citizenships.ToListAsync();
    }

    public static double GetTimestamp(DateTime value)
    {
        return new TimeSpan(DateTime.UtcNow.Ticks - value.Ticks).TotalSeconds;
    }
}