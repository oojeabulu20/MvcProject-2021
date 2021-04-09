using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcShoes.Models;

namespace MvcShoes.Data
{
        public class MvcShoesContext : DbContext
    
        {
            public MvcShoesContext(DbContextOptions<MvcShoesContext> options)
                : base(options)
            {
            }

            public DbSet<Shoes> Shoes { get; set; }
        }
    
}
