﻿using InvoicingApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.Entities
{

    /// <summary>
    /// Sazba DPH
    /// </summary>
    public class Vat : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Rate { get; set; }

        public Vat(int id, string name, string slug, int rate)
        {
            Id = id;
            Name = name;
            Slug = slug;
            Rate = rate;
        }
    }
}
