using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UAManagerWeb.Models;

namespace UAManagerWeb.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly UaVersionsContext _uaVersionsContext;

    [BindProperty] public string Login { get; set; } = null!;

    [BindProperty] public string Password { get; set; } = null!;

    public bool Logined { get; set; }

    public string? Message { get; set; }

    public List<Problem> Works { get; set; }

    public IndexModel(ILogger<IndexModel> logger, UaVersionsContext uaVersionsContext)
    {
        _logger = logger;
        _uaVersionsContext = uaVersionsContext;

        Works = _uaVersionsContext.Problems.Include(c => c.Status).Include(c => c.Priority).Include(c => c.Worker).ToList();
    }

    public async Task OnPostAsync()
    {
        try
        {
            var user = _uaVersionsContext.Workers.FirstOrDefault(c => c.FullName == Login && c.Password == Password);

            if (user == null)
            {
                Message = "Неверный логин или пароль";
                return;
            }

            Logined = true;
        }
        catch (Exception ex)
        {
            Console.Write(ex);
        }
    }
}