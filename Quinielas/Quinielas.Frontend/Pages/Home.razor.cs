using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Quinielas.Shared.Resources;

namespace Quinielas.Frontend.Pages
{
    public partial class Home
    {
        [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null;
    }
}