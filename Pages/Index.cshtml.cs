using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace PuppeteerDocker.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly LaunchOptions _options = new LaunchOptions
        {
            Headless = true,
            ExecutablePath = "/usr/bin/google-chrome",
            Args = new[] {"--enable-logging", "--v=1", "--no-sandbox"}
        };

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            
        }

        
        public async Task<IActionResult> OnPostAsync()
        {
            using var browser = await Puppeteer.LaunchAsync(_options);
            using var page = await browser.NewPageAsync();

            var data = new byte[0];
            await page.GoToAsync("http://www.google.com", new NavigationOptions { WaitUntil = new[] { WaitUntilNavigation.Networkidle0 } });
            data = await page.PdfDataAsync(new PdfOptions { Format = PaperFormat.A4 });

            return File(data, "application/pdf", "file.pdf");
        }
    
    }
}
