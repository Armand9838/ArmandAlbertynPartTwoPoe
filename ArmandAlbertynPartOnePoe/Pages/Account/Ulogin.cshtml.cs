using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ArmandAlbertynPartOnePoe.Pages.Account
{
    public class UloginModel : PageModel
    {

        public Credentials credentials { get; set; }

        public void OnGet()
        {
        }

        public class Credentials
        {
            [Required]
            [Display(Name = "User Name")]
            public string UserName { get; set; }

            [Required]
            public string Password { get; set; }
        }
    }
}
