using CurrieTechnologies.Razor.SweetAlert2;
using Quinielas.Frontend.Repositories;
using Quinielas.Shared.Resources;
using Quinielas.Shared.Entites;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;


namespace Quinielas.Frontend.Pages.Countries
{
    public partial class CountryCreate
    {
        private CountryForm? countryForm;
        private Country country = new();

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/countries", country);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync(Localizer["Error"], message);
                return;
            }

            Return();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: Localizer["RegistroGuardado"]);
        }

        private void Return()
        {
            countryForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/countries");
        }
    }

}