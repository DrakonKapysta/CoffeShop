using Microsoft.AspNetCore.Mvc;
using СoffeShop.Views.Shared.Components;

namespace СoffeShop.Components
{
    public class NavbarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new NavbarViewModel();
            return View(model);
        }
    }
}
