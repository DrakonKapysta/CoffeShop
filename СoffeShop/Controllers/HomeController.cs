using CoffeShopServices.Emailer;
using CoffeShopServices.Emailer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using СoffeShop.Models;
using Microsoft.AspNetCore.Localization;


namespace СoffeShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger, IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
        }
        [Route("Home")]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("About")]
        public IActionResult About()
        {
            return View();
        }
        [Route("Service")]
        public IActionResult Service()
        {
            return View();
        }
        [Route("Menu")]
        public IActionResult Menu()
        {
            return View();
        }
        [Route("Reservation")]
        public IActionResult Reservation()
        {
            return View();
        }
        [Route("Testimonial")]
        public IActionResult Testimonial()
        {
            return View();
        }
        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [Route("SendEmail")]
        public IActionResult SendEmail(EmailModel email)
        {
            try
            {
                _logger.LogInformation("Email is sending...");
                _emailSender.SendEmail(email);
                _logger.LogInformation("Email send");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Route("FileUpload")]
        public async Task<IActionResult> FileUpload(IFormFile file)
        {
            await UploadFile(file);
            TempData["msg"] = "File uploaded successfully";
            return View("FileUploadPage");
        }
        [Route("FileUpload")]
        public IActionResult FileUpload()
        {
            return View("FileUploadPage");
        }
        public async Task<bool> UploadFile(IFormFile file)
        {
            string path = "";
            bool iscopied = false;
            try
            {
                if (file.Length>0)
                {
                    string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files"));
                    using (var filestream = new FileStream(Path.Combine(path,filename), FileMode.Create))
                    {
                        await file.CopyToAsync(filestream);
                    }
                    iscopied = true;
                }
                else
                {
                    iscopied = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return iscopied;
        }
        [HttpPost]
        [Route("SetLanguage")]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), IsEssential=true });

            return LocalRedirect(returnUrl);


        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}