using MiniLink.Shared.Utilities;
using MiniLinkLogic.Libraries.MiniLink.Services;
using System;
using System.Collections.Generic;
using System.Text;


namespace MiniLinkLogic.Libraries.MiniLink.Core.Domain
{
    public class LinkEntry : BaseEntity
    {
        public LinkEntry(string urL, string ipAdress, DateTime dateAdded) : base()
        {
            URL = urL;
            IpAdress = ipAdress;
            DateAdded = dateAdded;
            Base64Id = GuidShortener.EncodeGuid(Id);
        }

        public LinkEntry():base()
        {
            Base64Id = GuidShortener.EncodeGuid(Id);
        }
        /// <summary>
        /// URL to redirecto to
        /// </summary>
        public string URL { get; set; }


        /// <summary>
        /// Creators ip address
        /// </summary>
        public string IpAdress { get; set; }

        /// <summary>
        /// Number of visits
        /// </summary>
        public int Visits { get; set; }

        // date added
        public DateTime DateAdded { get; set; }

        public string Base64Id { get; set; }

    }
}
