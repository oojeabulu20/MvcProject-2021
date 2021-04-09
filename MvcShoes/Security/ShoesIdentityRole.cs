using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MvcShoes.Security
{
    public class ShoesIdentityRole : IdentityRole
    {
        public string RoleDescription { get; set; }
    }
}
