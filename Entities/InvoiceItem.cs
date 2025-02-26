﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.Entities
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int Amount { get; set; }
        public int Discount { get; set; }
        public Vat Vat { get; set; }

        public InvoiceItem(int id, string name, decimal unitPrice, int amount, int discount, Vat vat)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
            Amount = amount;
            Discount = discount;
            Vat = vat;
        }
    }
}
