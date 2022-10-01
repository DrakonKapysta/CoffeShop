using CoffeShopServices.Emailer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShopServices.Emailer
{
    public interface IEmailSender
    {
        void SendEmail(EmailModel email);
    }
}
