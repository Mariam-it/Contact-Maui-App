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
            builder.Services.AddSingleton<ContactService>();
            builder.Services.AddSingleton<ContactListPage>();
            builder.Services.AddSingleton<ContactDetailPage>();
            builder.Services.AddSingleton<IFile, FileService>();

            return builder.Build();
        }
    }
}

//using MyMauiApp.Pages;
//using MyMauiApp.ViewModels;
//using Shared.Models;
//using Shared.Services;

//namespace MyMauiApp
//{
//    public static class MauiProgram
//    {
//        public static MauiApp CreateMauiApp()
//        {
//            var builder = MauiApp.CreateBuilder();
//            builder
//                .UseMauiApp<App>()
//                .ConfigureFonts(fonts =>
//                {
//                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
//                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
//                });
//            builder.Services.AddTransient<ContactsViewModel>();

//            builder.Services.AddSingleton<ContactAddPage>();
//            builder.Services.AddSingleton<ContactPage>();
//            builder.Services.AddSingleton<ContactService>();
//            builder.Services.AddSingleton<ContactListPage>();
//            builder.Services.AddSingleton<ContactDetailPage>();
//            builder.Services.AddSingleton<IFile, FileService>();



//            return builder.Build();
//        }
//    }
//}
