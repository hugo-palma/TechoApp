using System;
using Techo_App;
using Xamarin.Forms;
using Xamarin.Auth;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(AuthorizePage), typeof(Techo_App.Droid.AuthorizePageRenderer))]
namespace Techo_App.Droid
{
    class AuthorizePageRenderer : PageRenderer 
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            var activity = this.Context as Android.App.Activity;
            OAuth2Authenticator auth2 = null;
            OAuth1Authenticator auth1 = null;
            IKeys keys = null;

            var authPage = Element as AuthorizePage;
            switch (authPage.Service)
            {
                case LoginServices.Facebook:
                    keys = new FacebookKeys();
                    auth2 = new OAuth2Authenticator(
                        clientId: keys.ClientId,
                        scope: keys.Scope,
                        authorizeUrl: new Uri(keys.AuthorizeUrl),
                        redirectUrl: new Uri(keys.RedirectUrl));
                    break;
                case LoginServices.Twitter:
                    keys = new TwitterKeys();
                    auth1 = new OAuth1Authenticator(
                            consumerKey: keys.ConsumerKey,
                            consumerSecret: keys.ConsumerSecret,
                            requestTokenUrl: new Uri(keys.RequestTokenUrl),
                            authorizeUrl: new Uri(keys.AuthorizeUrl),
                            accessTokenUrl: new Uri(keys.AccessTokenUrl),
                            callbackUrl: new Uri(keys.CallbackUrl));
                    break;
                default:
                    throw new Exception("Service " + authPage.Service + " not yet supported");
            }
            if (auth2 != null)
            {
                auth2.Completed += async (sender, eventArgs) =>
                {
                    if (eventArgs.IsAuthenticated)
                        AccountStore.Create().Save(eventArgs.Account, authPage.Service.ToString());
                    await authPage.Navigation.PopAsync();
                };
                activity.StartActivity(auth2.GetUI(activity));
            }

            if (auth1 != null)
            {
                auth1.Completed += async (sender, eventArgs) =>
                {
                    if (eventArgs.IsAuthenticated)
                    {
                        AccountStore.Create().Save(eventArgs.Account, authPage.Service.ToString());
                    }
                    await authPage.Navigation.PopAsync();
                };
                
                activity.StartActivity(auth1.GetUI(activity));
            }

        }
    }
}