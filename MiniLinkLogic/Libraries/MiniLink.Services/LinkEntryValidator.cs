using MiniLinkLogic.Libraries.MiniLink.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLinkLogic.Libraries.MiniLink.Services
{
    public static class LinkEntryValidator
    {

        public static void Validate(LinkEntry entry, IOperationResult operationResult)
        {
            if (entry.IpAdress.Length > 50)
                operationResult.AddError(LinkEntryErrors.longIpAddressMessage);

            if (!string.IsNullOrEmpty(entry.URL)) 
            {
                if (entry.URL.Length > 2000)
                    operationResult.AddError(LinkEntryErrors.longUrlErrorMessage);

                if (!UrlValidator.ValidHttpURL(entry.URL))
                {
                    operationResult.AddError(LinkEntryErrors.invalidUrl);
                }
            }
            else
            {
                operationResult.AddError(LinkEntryErrors.missingUrl);
            }
           
        }
    }

    public static class LinkEntryErrors
    {
        public const string longIpAddressMessage = "Invalid IP Address";
        public const string longUrlErrorMessage = "The provided URL is too long";
        public const string invalidUrl = "Invalid url";
        public const string missingUrl = "The URL can not be empty";
    }
}
