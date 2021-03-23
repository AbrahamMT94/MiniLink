using MiniLink.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Text;


namespace MiniLinkLogic.Libraries.MiniLink.Core.Domain
{
    public class LinkEntry : BaseEntity
    {
        public LinkEntry(string urL, string ipAdress, DateTime dateAdded)
        {
            URL = urL;
            IpAdress = ipAdress;
            DateAdded = dateAdded;
        }

        public LinkEntry()
        {
           
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

    }
}
