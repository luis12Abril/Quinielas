using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Quinielas.Shared.Resources;

namespace Quinielas.Frontend.Layout
{
    public partial class NavMenu
    {
        [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null;

        private bool collapseNavMenu = true;

        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
    }
}