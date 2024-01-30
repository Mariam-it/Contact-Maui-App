using MyMauiApp.Pages;
using Microsoft.Maui.Controls;

namespace MyMauiApp
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Konstruktor för MAUI-applikationen.
        /// </summary>
        /// <param name="serviceProvider">En tjänstekontainer som används för att hantera beroenden.</param>
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;

            // Hämtar ContactPage från tjänstekontainern
            var contactPage = _serviceProvider.GetRequiredService<ContactPage>();

            // Konfigurerar huvudsidan som en NavigationPage med ContactPage som innehåll
            MainPage = new NavigationPage(contactPage);
        }
    }
}


