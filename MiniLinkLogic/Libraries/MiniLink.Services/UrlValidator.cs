using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MiniLinkLogic.Libraries.MiniLink.Services
{
    public static class UrlValidator
    {
        public static bool ValidHttpURL(string s)
        {
            if (!Regex.IsMatch(s, @"^https?:\/\/", RegexOptions.IgnoreCase))
                s = "http://" + s;

            Uri resultUri;

            if (Uri.TryCreate(s, UriKind.Absolute, out resultUri))
                return (resultUri.Scheme == Uri.UriSchemeHttp ||
                        resultUri.Scheme == Uri.UriSchemeHttps);

            return false;
        }
    }
}
