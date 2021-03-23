using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniLink.Server.Utilities
{
    public static class GuidShortener
    {
        public static string EncodeGuid(Guid guid)
        {
            var encodedGuid = Convert.ToBase64String(guid.ToByteArray());
            // for all strings we have a '==' at the end so we trim it and add it back in the decode function
            return encodedGuid.Substring(0, encodedGuid.Length-2) ;
        }

        public static Guid DecodeGuid(string encodedString)
        {
            return new Guid(Convert.FromBase64String(encodedString + "=="));
        }
    }
}
