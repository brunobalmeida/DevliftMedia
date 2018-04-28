using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Models.ViewModels
{
    public class UserListViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsClient { get; set; }
        public bool IsDeveloper { get; set; }

    }
}
