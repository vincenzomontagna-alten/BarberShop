﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Surname {  get; set; }

        public ICollection<Prenotation> Prenotations { get; set; } = new List<Prenotation>();
    }
}
