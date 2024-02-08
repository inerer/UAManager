using System.Reflection;
using MailKit.Net.Smtp;
using MimeKit;

namespace UAM.Core.EmailException;

public class ExceptionSender
{
    public static async Task SendEmailAsync(string message, string email)
    {
        string info = $"Версия ос: {Assembly.GetExecutingAssembly().GetName().Version}\n" +
                      $"Ядер процессора: {Environment.ProcessorCount}\n" +
                      $"64 битная система: {Environment.Is64BitProcess}\n";

        var gcMemoryInfo = GC.GetGCMemoryInfo();
        long installedMemory = gcMemoryInfo.TotalAvailableMemoryBytes;
        // it will give the size of memory in MB
        var physicalMemory = (double)installedMemory / 1048576.0;
        info += $"Ram: {physicalMemory}MB\n";

        string sendTo = "suuupportserpkoll@mail.ru";

        string login = "testemailsup@mail.ru";
        string password = "sMkApyqicjTf7fEhpReF";

        using var emailMessage = new MimeMessage();

        if (email != null)
        {
            message = email + "\n" + message;
        }

        emailMessage.From.Add(new MailboxAddress("BugReporter", login));
        emailMessage.To.Add(new MailboxAddress("Support", sendTo));
        emailMessage.Subject = "Bug";
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
        {
            Text = message + "\n" + info
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