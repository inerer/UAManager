using System.Windows;
using System.Windows.Controls;
using MailKit.Net.Smtp;
using MimeKit;
using UAM.Core.Enteties;

namespace WorkerApp.Pages;

public partial class ProblemEditPage : Page
{
	private List<Problem> works;
	private Problem? currentWork;
	private UaVersionsContext _uaVersionContext = new();
    public ProblemEditPage()
    {
        InitializeComponent();
        works = _uaVersionContext.Problems.Where(c => c.WorkerId == null).ToList();

        if (works.Count == 0)
        {
	        NothingShowPanel.Visibility = Visibility.Visible;
	        TaskPanel.Visibility = Visibility.Collapsed;
        }
        else
        {
	        currentWork = works.FirstOrDefault();
	        ShowWork(currentWork!);
        }
    }

   
	

	private async void StartTimer()
	{
		var startTime = new DateTime(2000,1,1,1,5,0);

		TimerTextBlock.Content = $"Осталось времени: {startTime:mm:ss}";

		while (true)
		{
			await Task.Delay(1000);

			startTime = startTime.AddSeconds(-1);

			if (startTime is {Minute: 0, Second: 0})
			{
				MessageBox.Show("Истекло время на решение, данные присвоены автоматически");

				currentWork.StatusId = 3;
				currentWork.StartTime = DateTime.UtcNow;

				_uaVersionContext.Problems.Update(currentWork);
				await _uaVersionContext.SaveChangesAsync();

				if (currentWork.Email != null)
				{
					await SendEmailAsync(currentWork.Email, "Назначен исполнитель");
				}

				works.RemoveAt(0);

				if (works.Count == 0)
				{
					NothingShowPanel.Visibility = Visibility.Visible;
					TaskPanel.Visibility = Visibility.Collapsed;
					return;
				}

				currentWork = works.FirstOrDefault();
				ShowWork(currentWork!);
			}

			TimerTextBlock.Content = $"Осталось времени: {startTime:mm:ss}";
		}
	}

	private void ShowWork(Problem problem)
	{
		PriorityComboBox.ItemsSource = _uaVersionContext.Priorities.ToList();

		WorkerComboBox.ItemsSource = _uaVersionContext.Workers.ToList();

		PriorityComboBox.SelectedIndex = 0;

		WorkerComboBox.SelectedIndex = 0;
		currentWork.WorkerId = 1;

		TaskTextBlock.Content = problem.ProblemText;

		if (problem.Email != null)
		{
			EmailTextBlock.Content = $"Почта клиента: {problem.Email}";
		}

		StartTimer();
	}

	private void PriorityComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		currentWork.PriorityId = (PriorityComboBox.SelectedValue as Priority).Id;
	}

	private void WorkerComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		currentWork.WorkerId = (WorkerComboBox.SelectedValue as Worker).Id;
	}

	private async void SaveButton_OnClick(object sender, RoutedEventArgs e)
	{
		currentWork.StatusId = 3;
		currentWork.StartTime = DateTime.UtcNow;

		_uaVersionContext.Problems.Update(currentWork);
		await _uaVersionContext.SaveChangesAsync();

		works.RemoveAt(0);

		MessageBox.Show("Сохранено!");

		if (works.Count == 0)
		{
			NothingShowPanel.Visibility = Visibility.Visible;
			TaskPanel.Visibility = Visibility.Collapsed;
			return;
		}

		if (currentWork.Email != null)
		{
			await SendEmailAsync(currentWork.Email, "Назначен исполнитель");
		}

		currentWork = works.FirstOrDefault();
		ShowWork(currentWork!);
	}

	private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
	{
		works = _uaVersionContext.Problems.Where(c => c.WorkerId == null).ToList();

		if (works.Count == 0)
		{
			NothingShowPanel.Visibility = Visibility.Visible;
			TaskPanel.Visibility = Visibility.Collapsed;
		}
		else
		{
			NothingShowPanel.Visibility = Visibility.Collapsed;
			TaskPanel.Visibility = Visibility.Visible;
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