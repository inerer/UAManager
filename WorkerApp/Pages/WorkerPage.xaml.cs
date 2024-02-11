using System.Windows;
using ModernWpf.Controls;
using UAM.Core.Enteties;
using WorkerApp.View;
using Page = System.Windows.Controls.Page;

namespace WorkerApp.Pages;

public partial class WorkerPage : Page
{
    private readonly Worker _worker;
    private Problem? currentWork;
    private UaVersionsContext _uaVersionsContext = new UaVersionsContext();

    private DateTime timer;

    public WorkerPage(Worker worker)
    {
        _worker = worker;
        InitializeComponent();

        WorkerTextBlock.Content = worker.FullName;

        currentWork = _uaVersionsContext.Problems.OrderBy(c => c.PriorityId)
            .FirstOrDefault(c => c.WorkerId == worker.Id && c.EndTime == null);

        if (currentWork == null)
        {
            NothingShowStackPanel.Visibility = Visibility.Visible;
            TaskStackPanel.Visibility = Visibility.Collapsed;
        }
        else
        {
            ShowWork(currentWork);

            NothingShowStackPanel.Visibility = Visibility.Collapsed;
            TaskStackPanel.Visibility = Visibility.Visible;
        }
    }

    private void ShowWork(Problem work)
    {
        TaskTextBlock.Content = work.ProblemText;

        if (work.Email != null)
        {
            EmailTextBlock.Content = $"Почта клиента: {work.Email}";
        }

        StartTimer();
    }

    private async void StartTimer()
    {
        timer = new DateTime(2000, 1, 1, 1, 0, 0);

        TimerTextBlock.Content = $"Осталось времени: {timer:hh:mm:ss}";

        while (true)
        {
            await Task.Delay(1000);

            timer = timer.AddSeconds(-1);

            if (timer is { Minute: 0, Second: 0 })
            {
                MessageBox.Show("Истекло время на решение, данные присвоены автоматически");
                timer = new DateTime(2000, 1, 1, 1, 0, 0);
            }

            TimerTextBlock.Content = $"Осталось времени: {timer:hh:mm:ss}";
        }
    }

    private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
    {
        currentWork = _uaVersionsContext.Problems.FirstOrDefault(c => c.WorkerId == _worker.Id && c.EndTime == null);

        if (currentWork == null)
        {
            NothingShowStackPanel.Visibility = Visibility.Visible;
            TaskStackPanel.Visibility = Visibility.Collapsed;
        }
        else
        {
            ShowWork(currentWork);

            NothingShowStackPanel.Visibility = Visibility.Collapsed;
            TaskStackPanel.Visibility = Visibility.Visible;
        }
    }

    private void NotWorkButton_OnClick(object sender, RoutedEventArgs e)
    {
        var newWorker = _uaVersionsContext.Workers.FirstOrDefault(c => c.Id != _worker.Id);

        currentWork.WorkerId = newWorker.Id;
        _uaVersionsContext.Problems.Update(currentWork);
        _uaVersionsContext.SaveChanges();

        var nextWork = _uaVersionsContext.Problems.FirstOrDefault(c => c.WorkerId == _worker.Id && c.EndTime == null);

        if (nextWork == null)
        {
            NothingShowStackPanel.Visibility = Visibility.Visible;
            TaskStackPanel.Visibility = Visibility.Collapsed;
        }
        else
        {
            ShowWork(nextWork);

            NothingShowStackPanel.Visibility = Visibility.Collapsed;
            TaskStackPanel.Visibility = Visibility.Visible;
        }
    }

    private void MoreTime_OnClick(object sender, RoutedEventArgs e)
    {
        timer = timer.AddHours(1);
    }

    private async void SendSolButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (currentWork.Email == null)
        {
            MessageBox.Show("Почта клиента неизвестна, задача выполнена");

            currentWork.EndTime = DateTime.UtcNow;
            currentWork.StatusId = 4;

            _uaVersionsContext.Problems.Update(currentWork);
            _uaVersionsContext.SaveChanges();

            currentWork =
                _uaVersionsContext.Problems.FirstOrDefault(c => c.WorkerId == _worker.Id && c.EndTime == null);

            if (currentWork == null)
            {
                NothingShowStackPanel.Visibility = Visibility.Visible;
                TaskStackPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                ShowWork(currentWork);

                NothingShowStackPanel.Visibility = Visibility.Collapsed;
                TaskStackPanel.Visibility = Visibility.Visible;
            }
        }
        else
        {
            ContentDialog contentDialog = new ContentDialog
            {
                Title = "Подтверждение",
                Content = new SolutionUserControl(currentWork.Email),
                CloseButtonText = "Нет",
                PrimaryButtonText = "Да",
            };

            await contentDialog.ShowAsync();

            currentWork.EndTime = DateTime.UtcNow;
            currentWork.StatusId = 2;

            _uaVersionsContext.Problems.Update(currentWork);
            _uaVersionsContext.SaveChanges();

            currentWork =
                _uaVersionsContext.Problems.FirstOrDefault(c => c.WorkerId == _worker.Id && c.EndTime == null);

            if (currentWork == null)
            {
                NothingShowStackPanel.Visibility = Visibility.Visible;
                TaskStackPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                ShowWork(currentWork);

                NothingShowStackPanel.Visibility = Visibility.Collapsed;
                TaskStackPanel.Visibility = Visibility.Visible;
            }
        }
    }
}