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
        public Client Client { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

        public Invoice(string invoiceNumber, Client client, DateTime issueDate, DateTime dueDate)
        {
            InvoiceNumber = invoiceNumber;
            Client = client;
            IssueDate = issueDate;
            DueDate = dueDate;
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
    }
}
