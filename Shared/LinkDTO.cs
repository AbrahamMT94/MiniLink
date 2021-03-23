using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

/// <summary>
/// A series of dto to be use between layers
/// </summary>
namespace MiniLink.Shared
{
    
   
    public class LinkDTO : CreateLinkDTO
    {     
        public Guid Id { get; set; }
    }

    public class LinkWithCountDTO:LinkDTO
    {
        public int VisitCount { get; set; }

    }

    public class CreateLinkDTO
    {

        [Required, Url, StringLength(2000)]
        public string URL { get; set; }
    }
   
}
