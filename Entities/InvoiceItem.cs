using InvoicingApp.Interfaces;
using InvoicingApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.Entities
{
    public class InvoiceItem : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int Amount { get; set; }
        public int Discount { get; set; }
        public Vat Vat { get; set; }

        public InvoiceItem() 
        { 
            Name = string.Empty;
            UnitPrice = 0;
            Amount = 0;
            Discount = 0;
            Vat = VatService.FindBySlug("basic");
        }

        public InvoiceItem(string name, decimal unitPrice, int amount, int discount, Vat vat)
        {
            Name = name;
            UnitPrice = unitPrice;
            Amount = amount;
            Discount = discount;
            Vat = vat;
        }

        public decimal TotalPrice()
        {
            return UnitPrice * Amount;
        }

        public decimal DiscountAmount()
        {
            return TotalPrice() * (Discount / 100m);
        }

        public decimal TotalPriceAfterDiscount()
        {
            return TotalPrice() - DiscountAmount();
        }

        public decimal VatAmount()
        {
            return TotalPriceAfterDiscount() * (Vat.Rate / 100m);
        }

        public decimal TotalPriceWithVat()
        {
            return TotalPriceAfterDiscount() + VatAmount();
        }

        public override string ToString()
        {
            return $"ID: {Id} | Název: {Name} | Počet: {Amount} ks | Jedn. cena: {UnitPrice.ToString("0.00")} Kč/ks | Sleva: {Discount} % | DPH: {Vat.Rate} % | Cena bez DPH: {TotalPriceAfterDiscount().ToString("0.00")} Kč | Cena včetně DPH: {TotalPriceWithVat().ToString("0.00")} Kč";
        }
    }
}
