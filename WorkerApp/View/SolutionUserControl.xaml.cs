using System.Windows;
using System.Windows.Controls;
using MailKit.Net.Smtp;
using MimeKit;
using UAM.Core.Enteties;

namespace WorkerApp.View;

public partial class SolutionUserControl : UserControl
{
    private string _email;
    private int _problemId;
    
    private UaVersionsContext _uaVersionsContext = new UaVersionsContext();
    public SolutionUserControl(string email, int problemId)
    {
        InitializeComponent();
        _email = email;
        _problemId = problemId;
    }

    private async void SendButton_OnClick(object sender, RoutedEventArgs e)
    {
        var problem = _uaVersionsContext.Problems.FirstOrDefault(c => c.Id == _problemId);
        problem.Solution = SolutionTextBox.Text;
        problem.Version = VersionTextBox.Text;
        _uaVersionsContext.Problems.Update(problem);
        await _uaVersionsContext.SaveChangesAsync();
        await SendEmailAsync(_email, SolutionTextBox.Text);

        MessageBox.Show("Отправлено!");
        
    }

    public static async Task SendEmailAsync(string email, string text)
    {
        string login = "suuupportserpkoll@mail.ru";
        string password = "t8ZrEHC4Hx7yeVxQ6tfc";

        using var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress("Support", login));
        emailMessage.To.Add(new MailboxAddress("User", email));
        emailMessage.Subject = "Bug";
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
        {
            Text = text
        };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.mail.ru", 465, true);
            await client.AuthenticateAsync(login, password);
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
    }
}