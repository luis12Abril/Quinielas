using CurrieTechnologies.Razor.SweetAlert2;
using Quinielas.Frontend.Repositories;
using Quinielas.Shared.Resources;
using Quinielas.Shared.Entites;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;


namespace Quinielas.Frontend.Pages.Countries
{
    public partial class CountryEdit
    {
        private Country? country;
        private CountryForm? countryForm;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

        [Parameter] public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var responseHttp = await Repository.GetAsync<Country>($"api/countries/{Id}");

            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("countries");
                }
                else
                {
                    var messageError = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync(Localizer["Error"], messageError, SweetAlertIcon.Error);
                }
            }
            else
            {
                country = responseHttp.Response;
            }
        }

        private async Task EditAsync()
        {
            var responseHttp = await Repository.PutAsync("api/countries", country);

            if (responseHttp.Error)
            {
                var mensajeError = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync(Localizer["Error"], mensajeError, SweetAlertIcon.Error);
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
            NavigationManager.NavigateTo("countries");
        }
    }

}