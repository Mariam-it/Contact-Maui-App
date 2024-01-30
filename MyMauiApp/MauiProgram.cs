using MyMauiApp.Pages;
using MyMauiApp.ViewModels;
using Shared.Models;
using Shared.Services;

namespace MyMauiApp
{
    public static class MauiProgram
    {
        /// <summary>
        /// Skapar en ny MAUI-applikation.
        /// </summary>
        /// <returns>En instans av MauiApp.</returns>
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Lägger till dependency injection av ContactsViewModel
            builder.Services.AddTransient<ContactsViewModel>();

            // Lägger till singleton-tjänster
            builder.Services.AddSingleton<ContactAddPage>();
            builder.Services.AddSingleton<ContactPage>();
            builder.Services.AddSingleton<IContactService, ContactService>();
            builder.Services.AddTransient<ContactViewModel>();
            builder.Services.AddSingleton<ContactListPage>();
            builder.Services.AddSingleton<ContactDetailPage>();
            builder.Services.AddSingleton<IFile, FileService>();

            return builder.Build();
        }
    }
}