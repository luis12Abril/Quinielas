using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Quinielas.Frontend.Repositories;
using Quinielas.Shared.Entites;
using Quinielas.Shared.Resources;

namespace Quinielas.Frontend.Pages.Countries
{
    public partial class CountriesIndex
    {
        [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null;
        [Inject] private IRepository Repository { get; set; } = null!;

        private List<Country>? Countries { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var responseHttp = await Repository.GetAsync<List<Country>>("api/countries");
            Countries = responseHttp.Response;

        }
    }
}