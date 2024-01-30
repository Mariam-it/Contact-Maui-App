using Microsoft.Maui.ApplicationModel.Communication;
using MyMauiApp.ViewModels;
namespace MyMauiApp.Pages;

public partial class ContactDetailPage : ContentPage
{
    private readonly ContactViewModel _viewModel;
    public ContactDetailPage(ContactViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
    /// <summary>
    /// Sätter kontaktobjektet som DataContext för sidan.
    /// </summary>
    /// <param name="contact">Kontaktobjektet som ska visas.</param>
    public void SetContact(Shared.Models.Contact contact)
    {
        if (BindingContext is ContactViewModel viewModel)
        {
            viewModel.CurrentContact = contact;
        }
    }

    /// <summary>
    /// Hanterar händelsen när knappen "Uppdatera kontakt" klickas.
    /// Uppdaterar kontaktinformationen och hanterar eventuella fel.
    /// </summary>
    private async void OnUpdateContactClicked(object sender, EventArgs e)
    {
        if (_viewModel.CurrentContact != null)
        {
            _viewModel.UpdateContact(_viewModel.CurrentContact);
            await DisplayAlert("Uppdatering", "Kontakten har uppdaterats.", "OK");
        }
        else
        {
            await DisplayAlert("Fel", "Ingen kontakt att uppdatera", "OK");
        }

    }


}