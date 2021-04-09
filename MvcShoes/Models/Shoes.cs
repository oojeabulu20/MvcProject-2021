using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MvcShoes.Models
{
    public class Shoes
    {   
        public int ShoesId { get; set; }
        public string ShoeName { get; set; }
        public string Description { get; set; }
        public string ShoeImage { get; set; }
        public decimal Price { get; set; }
    }
}
