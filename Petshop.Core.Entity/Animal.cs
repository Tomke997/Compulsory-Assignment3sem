using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.Entity
{
    public class Animal
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime SoldDate { get; set; }
        public string Color { get; set; }
        public String PreviousOwner { get; set; }
        public double Price { get; set; }
    }
}
