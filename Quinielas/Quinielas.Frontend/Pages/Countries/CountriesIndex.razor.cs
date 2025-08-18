using CurrieTechnologies.Razor.SweetAlert2;
using Quinielas.Frontend.Repositories;
using Quinielas.Shared.Resources;
using Quinielas.Shared.Entites;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;


namespace Quinielas.Frontend.Pages.Countries
{
    public partial class CountriesIndex
    {
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        private List<Country>? Countries { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            var responseHppt = await Repository.GetAsync<List<Country>>("api/countries");
            if (responseHppt.Error)
            {
                var message = await responseHppt.GetErrorMessageAsync();
                await SweetAlertService.FireAsync(Localizer["Error"], message, SweetAlertIcon.Error);
                return;
            }
            Countries = responseHppt.Response!;
        }

        private async Task DeleteAsync(Country country)
        {
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = Localizer["Confirmacion"],
                Text = string.Format(Localizer["ConfirmarBorrado"], Localizer["Pais"], country.Name),
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
                CancelButtonText = Localizer["Cancelar"]
            });

            var confirm = string.IsNullOrEmpty(result.Value);

            if (confirm)
            {
                return;
            }

            var responseHttp = await Repository.DeleteAsync($"api/countries/{country.id}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    var mensajeError = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync(Localizer["Error"], mensajeError, SweetAlertIcon.Error);
                }
                return;
            }

            await LoadAsync();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000,
                ConfirmButtonText = Localizer["Si"]
            });
            toast.FireAsync(icon: SweetAlertIcon.Success, message: Localizer["RegistroBorrado"]);
        }
    }

}