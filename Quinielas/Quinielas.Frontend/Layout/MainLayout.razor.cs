using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Quinielas.Shared.Resources;

namespace Quinielas.Frontend.Layout
{
    public partial class MainLayout
    {
        [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null;
    }
}