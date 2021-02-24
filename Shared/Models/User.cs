using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERacuniNovi.Shared.Models
{
    public class User : IdentityUser
    {
        public string firstName { get; set; }
        public string lastName { get; set; }

        public User() { }
        public User(string first, string last)
        {
            firstName = first;
            lastName = last;
        }
    }
}
