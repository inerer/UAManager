using System.Windows;
using System.Windows.Controls;
using MailKit.Net.Smtp;
using MimeKit;

namespace WorkerApp.View;

public partial class SolutionUserControl : UserControl
{
    private string email;
    public SolutionUserControl(string email)
    {
        InitializeComponent();
        this.email = email;
    }

    private async void SendButton_OnClick(object sender, RoutedEventArgs e)
    {
        await SendEmailAsync(email, SolutionTextBox.Content.ToString());

        MessageBox.Show("Отправлено!");
        
    }

    public static async Task SendEmailAsync(string email, string text)
    {
        string login = "suuupportserpkoll@mail.ru";
        string password = "fgDKK5h9QUdKqgqvejr8";

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