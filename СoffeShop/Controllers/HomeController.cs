using CoffeShopServices.Emailer;
using CoffeShopServices.Emailer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using СoffeShop.Models;

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}