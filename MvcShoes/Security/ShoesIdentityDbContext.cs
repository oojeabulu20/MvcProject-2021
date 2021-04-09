using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MvcShoes.Security
{
    public class ShoesIdentityDbContext : IdentityDbContext<ShoesIdentityUser, ShoesIdentityRole, string>
    {
        public ShoesIdentityDbContext
            (DbContextOptions<ShoesIdentityDbContext> options)
            : base(options)
        {
        }
    }

}
