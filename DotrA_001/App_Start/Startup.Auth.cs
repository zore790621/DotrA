using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;

namespace DotrA_001
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                LoginPath = new PathString("/Account/Index"),
                SlidingExpiration = true
            });
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "279531613191-m9h76om107qs2gulplb6864pqta8vedh.apps.googleusercontent.com",
                ClientSecret = "5yNKLIINoaBGk5HiGuSImG8-",
                CallbackPath = new PathString("/GoogleLoginCallback")
            });
        }
    }
}
