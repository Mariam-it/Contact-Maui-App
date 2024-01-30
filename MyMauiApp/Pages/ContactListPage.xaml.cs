
using MyMauiApp.ViewModels;

namespace MyMauiApp.Pages
{
    public partial class ContactListPage : ContentPage
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ContactsViewModel _viewModel;

        public ContactListPage(ContactsViewModel viewModel, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _serviceProvider = serviceProvider;
        }
        /// <summary>
        /// Metod som k�rs n�r sidan visas.
        /// Laddar kontakter fr�n ViewModel och visar dem p� sidan.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadContacts(); 
            BindingContext = _viewModel;
            if (_viewModel.Contacts.Count == 0)
            {
                DisplayAlert("Inga kontakter", "Det finns inga kontakter att visa.", "OK");
            }
        }
        /// <summary>
        /// Hanterar h�ndelsen n�r ett kontaktobjekt trycks p�.
        /// �ppnar ContactDetailPage f�r den valda kontakten.
        /// </summary>
        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Shared.Models.Contact selectedContact)
            {
                var contactDetailPage = _serviceProvider.GetRequiredService<ContactDetailPage>();
                contactDetailPage.SetContact(selectedContact);
                await Navigation.PushAsync(contactDetailPage);
            }
        }

    }
}
