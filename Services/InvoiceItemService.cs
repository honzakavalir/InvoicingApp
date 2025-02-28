using InvoicingApp.Entities;
using InvoicingApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.Services
{
    public class InvoiceItemService : IEntityService<InvoiceItem>
    {
        public InvoiceService InvoiceService { get; set; }

        public InvoiceItemService()
        {
            InvoiceService = new InvoiceService();
        }

        public InvoiceItem Create(InvoiceItem item)
        {
            item.Id = AutoIncrementService.GenerateId<InvoiceItem>();

            JsonService.Create(item);
            return item;
        }

        public InvoiceItem Update(InvoiceItem updatedItem)
        {
            JsonService.Update(updatedItem);
            return updatedItem;
        }

        public void Delete(int id)
        {
            JsonService.Delete<InvoiceItem>(id);
        }

        public InvoiceItem GetById(int id)
        {
            return JsonService.GetById<InvoiceItem>(id);
        }

        public List<InvoiceItem> GetAll()
        {
            List<InvoiceItem> items = new List<InvoiceItem>();

            List<Invoice> invoices = InvoiceService.GetAll();
            foreach (Invoice invoice in invoices) 
            {
                foreach (InvoiceItem item in invoice.InvoiceItems)
                {
                    items.Add(item);
                }
            }

            return items;
        }

        public List<InvoiceItem> GetAllForInvoice(int invoiceId)
        {
            Invoice invoice = InvoiceService.GetById(invoiceId);
            return invoice.InvoiceItems;
        }

        public void AddToInvoice(int invoiceId, int itemId)
        {
            InvoiceItem item = GetById(itemId);
            Invoice invoice = InvoiceService.GetById(invoiceId);
            invoice.InvoiceItems.Add(item);
            InvoiceService.Update(invoice);
        }

        public void RemoveFromInvoice(int invoiceId, int itemId)
        {
            InvoiceItem item = GetById(itemId);
            Invoice invoice = InvoiceService.GetById(invoiceId);
            invoice.InvoiceItems.Remove(item);
            InvoiceService.Update(invoice);
        }
    }
}
