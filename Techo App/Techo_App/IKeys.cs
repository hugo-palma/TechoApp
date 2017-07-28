using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techo_App
{
    public interface IKeys
    {
        string ClientId { get; }
        string ClientSecret { get; }
        string Scope { get; }
        string AuthorizeUrl { get; }
        string RedirectUrl { get; }
        string RequestTokenUrl { get; }
        string AccessTokenUrl { get; }
        string ConsumerSecret { get; }
        string ConsumerKey { get; }
        string CallbackUrl { get; }
    }
    public class FacebookKeys : IKeys
    {

        public string ClientId { get; } = "1857233457861970";

        public string AuthorizeUrl { get; } = "https://m.facebook.com/dialog/oauth/";

        public string RedirectUrl { get; } = "https://www.facebook.com/connect/login_success.html";

        public string AccessTokenUrl { get; } = "";

        public string ClientSecret { get; }

        public string ConsumerKey { get; }

        public string ConsumerSecret { get; }

        public string RequestTokenUrl { get; }

        public string Scope { get; }

        public string CallbackUrl { get; }
    }

    public class TwitterKeys : IKeys
    {

        public string ConsumerKey { get; } = "muAuYTBzDyDFxOqMOoU5xZwVd";

        public string ConsumerSecret => "OsLKdx04aywLDXoXlwEqeJOwtqd58ELomfBQAaip2EhbccBBAt";

        public string AccessTokenUrl { get; } = "https://api.twitter.com/oauth/access_token";

        public string AuthorizeUrl { get; } = "https://api.twitter.com/oauth/authorize";

        public string RedirectUrl { get; } = "https://mobile.twitter.com/home";

        public string RequestTokenUrl => "https://api.twitter.com/oauth/request_token";

        public string CallbackUrl => "https://mobile.twitter.com/home";

        public string ClientId { get; } = "";

        public string ClientSecret { get; } = "";

        public string Scope { get; } = "";


    }
}
