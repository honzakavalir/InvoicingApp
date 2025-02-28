using InvoicingApp.Entities;
using InvoicingApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.Services
{
    public class InvoiceService : IEntityService<Invoice>
    {
        public Invoice Create(Invoice invoice)
        {
            invoice.Id = AutoIncrementService.GenerateId<Invoice>();

            JsonService.Create(invoice);
            return invoice;
        }

        public Invoice Update(Invoice updatedInvoice)
        {
            JsonService.Update(updatedInvoice);
            return updatedInvoice;
        }

        public void Delete(int id)
        {
            JsonService.Delete<Invoice>(id);
        }

        public Invoice GetById(int id)
        {
            return JsonService.GetById<Invoice>(id);
        }

        public List<Invoice> GetAll()
        {
            return JsonService.GetAll<Invoice>();
        }     
    }
}
