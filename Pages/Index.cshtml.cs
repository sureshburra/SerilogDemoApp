using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SerilogDemoApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        _logger.LogInformation("Index page loaded");

        try
        {
            for (int i = 0; i < 100; i++)
            {
                if (i == 56)
                {
                    throw new Exception("This is our demo exception");
                }
                else
                {
                    _logger.LogInformation("Processing item {iVariable}", i);
                    
                }
            }


        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the request");
        }
    }
}
