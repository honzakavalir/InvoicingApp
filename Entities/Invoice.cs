using InvoicingApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.Entities
{
    public class Invoice : IEntity
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public Client? Client { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

        public Invoice()
        {
            Id = 0;
            InvoiceNumber = string.Empty;
            Client = null;
            IssueDate = DateTime.Now;
            DueDate = DateTime.Now.AddDays(14);
        }

        public decimal TotalPrice()
        {
            return InvoiceItems.Sum(item => item.TotalPrice());
        }

        public decimal TotalDiscount() 
        {
            return InvoiceItems.Sum(item => item.DiscountAmount());
        }

        public decimal TotalPriceAfterDiscount()
        {
            return InvoiceItems.Sum(item => item.TotalPriceAfterDiscount());
        }

        public decimal VatAmount()
        {
            return InvoiceItems.Sum(item => item.VatAmount());
        }

        public decimal TotalPriceWithVat()
        {
            return InvoiceItems.Sum(item => item.TotalPriceWithVat());
        }

        public override string ToString()
        {
            return $"ID faktury: {Id}\n" +
                   $"Číslo faktury: {InvoiceNumber}\n" +
                   $"Datum vystavení: {IssueDate.ToString("dd.MM.yyyy")}\n" +
                   $"Datum splatnosti: {DueDate.ToString("dd.MM.yyyy")}\n" +
                   $"Zákazník: {Client?.Name}\n" +
                   $"Celková částka bez DPH: {TotalPriceAfterDiscount().ToString("0.00")} Kč\n" +
                   $"Celková částka včetně DPH: {TotalPriceWithVat().ToString("0.00")} Kč";
        }
    }
}
