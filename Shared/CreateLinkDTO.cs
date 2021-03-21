using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MiniLink.Shared
{
    public class CreateLinkDTO
    {
        
        [Required, Url, StringLength(2000)]
        public string URL { get; set; }
    }
}
