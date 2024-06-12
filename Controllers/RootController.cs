using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace BankCoreApi.Controllers
{   
    [Route("api")]
    [ApiController]
    public class RootController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public RootController(IConfiguration configuration)
        {
            _configuration = configuration; 
        }

        [HttpGet]
        public IActionResult Index()
        {
            var htmlContent = @"
            <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Welcome to Bank Core API</title>
                </head>
                <body>
                    <h1>Welcome to Bank Core API!</h1>
                    <p>To test the API in Postman, download the API collections <a target='_blank' href='/api/download-collections'>here</a>.</p>
                </body>
                </html>
            ";
            return Content(htmlContent, "text/html", Encoding.UTF8);
        }

        [HttpGet("download-collections")]
        public IActionResult DownloadCollections()
        {
            var fileName = "postman.postman_collection.json";
            var path = _configuration["ApiCollectionPath"] ?? string.Empty;
            var filePath = Path.Combine(path, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found"); // Handle file not found scenario
            }

            return new FileContentResult(System.IO.File.ReadAllBytes(filePath), "application/json")
            {
                FileDownloadName = fileName
            };
        }
    }
    
}