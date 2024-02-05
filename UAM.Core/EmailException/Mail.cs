using System.ComponentModel.DataAnnotations;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MimeKit;

namespace UAM.Core.EmailException;

public class Mail
{
    public static async Task ReadEmails()
	{
		string login = "supprtinstallerapk220@mail.ru";
		string password = "vi8Lz1QjZxpZ0R9qzM21";

		using (var client = new ImapClient())
		{
			await client.ConnectAsync("imap.mail.ru", 993 , true);
			await client.AuthenticateAsync(login, password);

			var inbox = client.Inbox;
			await inbox.OpenAsync(FolderAccess.ReadWrite);
			var unread = inbox.FirstUnread;

			if (unread == 0)
			{
				Console.WriteLine("Сообщения не найдены");
				return;
			}

			var message = await inbox.GetMessageAsync(unread);

			if (message.Subject == "Bug")
			{
				string? email = null;

				var emailTry = message.TextBody.Split('\n')[0];
				if (new EmailAddressAttribute().IsValid(emailTry))
				{
					email = emailTry;
				}

				// await using FilesContext filesContext = new FilesContext();
				//
				// var work = new Work() {Text = message.TextBody, ClientEmail = email, StatusId = 1};
				// filesContext.Works.Add(work);
				// await filesContext.SaveChangesAsync();

				await inbox.AddFlagsAsync(unread, MessageFlags.Seen, true);

				if (email != null)
				{
					await SendEmailAsync(email, "Задача зарегистрирована");
				}

				Console.WriteLine(string.Concat(Enumerable.Repeat("-", 64)));
				Console.WriteLine("Получено сообщение");
				Console.WriteLine(message.Subject);
				Console.WriteLine(message.TextBody);
				Console.WriteLine(string.Concat(Enumerable.Repeat("-", 64)));
			}
			else
			{
				await inbox.AddFlagsAsync(unread, MessageFlags.Seen, true);
			}

			await client.DisconnectAsync(true);
		}
	}

	public static async Task SendEmailAsync(string email, string text)
	{
		string login = "supprtinstallerapk220@mail.ru";
		string password = "vi8Lz1QjZxpZ0R9qzM21";

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