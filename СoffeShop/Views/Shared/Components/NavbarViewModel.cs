namespace СoffeShop.Views.Shared.Components
{
    public class NavbarViewModel
    {
        public List<string> Keys { get; }

        public Dictionary<string, string[]> DropdownKeys { get; }
        public NavbarViewModel()
        {
            Keys = new List<string>()
            {
                "Home", "About","Service","Menu","Pages","Contact"
            };
            DropdownKeys = new Dictionary<string, string[]>
            {
                { "Pages", new[]{ "Reservation", "Testimonial", "Upload" } }
            };
        }
    }
}
