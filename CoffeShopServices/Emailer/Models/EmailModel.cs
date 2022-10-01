using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShopServices.Emailer.Models
{
    public class EmailModel
    {
        public string Receiver { get; set; } = null!;
        public string Message { get; set; } = "Welcome to the CoffeShop family!";
        public string Sender { get; set; } = "CoffeShopServiceTest@gmail.com";
        public string Theme { get; set; } = "For dear user";
    }
}
