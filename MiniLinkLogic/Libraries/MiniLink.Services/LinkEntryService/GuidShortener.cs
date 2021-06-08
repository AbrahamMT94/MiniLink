using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;

namespace MiniLinkLogic.Libraries.MiniLink.Services
{
    public static class GuidShortener
    {
        public static string EncodeGuid(Guid guid)
        {
            var encodedGuid = Base64UrlTextEncoder.Encode(guid.ToByteArray());
          
            return encodedGuid ;
        }

        public static Guid DecodeGuid(string encodedString)
        {
            return new Guid(Base64UrlTextEncoder.Decode(encodedString ));
        }
    }
}
