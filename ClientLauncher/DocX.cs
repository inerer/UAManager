using System.Diagnostics;
using ClientLauncher.Enteties;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace ClientLauncher;

public class DocX
{
    public void GenerateProtocol(Ticket ticket)
    {
        string path = Environment.CurrentDirectory + $@"\Word\{ticket.ClientInfo.FIO} ticket";

        Xceed.Words.NET.DocX document = Xceed.Words.NET.DocX.Create(path);

        Paragraph paragraph = document.InsertParagraph();

        paragraph.AppendLine($"Билет на поезд №{ticket.Id}")
            .Font("Times New Roman")
            .FontSize(12)
            .Alignment = Alignment.center;

        Paragraph mainParagraph = document.InsertParagraph();

        mainParagraph.AppendLine(
                $"Клиент {ticket.ClientInfo.FIO} \nДата отправления {ticket.DateDeparture} \n Дата прибытия {ticket.DateArrival} \n Количество станций {ticket.CountStation}  \n Цена {ticket.Price} \n Сотрудник оформления {ticket.Worker.LastName}  ")
            .Font("Times New Roman")
            .FontSize(12)
            .Alignment = Alignment.left;
        document.Save();

        Process.Start(new ProcessStartInfo(Environment.CurrentDirectory + $@"\Word\{ticket.ClientInfo.FIO} ticket.docx")
            { UseShellExecute = true });
    }
}