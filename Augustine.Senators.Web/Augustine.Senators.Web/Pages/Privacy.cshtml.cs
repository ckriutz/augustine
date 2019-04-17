using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Augustine.Senators.Web.Pages
{
    [Authorize]
    public class PrivacyModel : PageModel
    {
        public async void OnGet()
        {
            await WriteOutIdentityInformation();
        }

        private async Task WriteOutIdentityInformation()
        {
            //get the saved identity token
            var identityToken = await HttpContext
                .GetTokenAsync(OpenIdConnectParameterNames.IdToken);

            Debug.WriteLine($"Identity Token: {identityToken}");

            foreach(var claim in User.Claims)
            {
                Debug.WriteLine($"Claim Type:{claim.Type}, Claim Value:{claim.Value}");
            }
        }
    }
}