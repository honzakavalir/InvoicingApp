using InvoicingApp.Entities;
using InvoicingApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.Services
{
    /// <summary>
    /// Service pro faktury
    /// </summary>
    public class InvoiceService : IEntityService<Invoice>
    {
        /*
         * Metody pro práci s fakturou
         */

        /// <summary>
        /// Vytvoří novou fakturu podle předaného objektu -> uloží do souboru -> vygeneruje unikátní ID
        /// </summary>
        /// <param name="invoice">Faktura</param>
        /// <returns>Uložená faktura</returns>
        public Invoice Create(Invoice invoice)
        {
            invoice.Id = AutoIncrementService.GenerateId<Invoice>();

            JsonService.Create(invoice);
            return invoice;
        }

        /// <summary>
        /// Aktualizuje fakturu podle předaného objektu -> uloží změny do souboru 
        /// </summary>
        /// <param name="updatedInvoice">Faktura</param>
        /// <returns>Aktualizovaná faktura</returns>
        public Invoice Update(Invoice updatedInvoice)
        {
            JsonService.Update(updatedInvoice);
            return updatedInvoice;
        }

        /// <summary>
        /// Smaže fakturu podle předaného ID -> smaže ze souboru
        /// </summary>
        /// <param name="id">ID faktury</param>
        public void Delete(int id)
        {
            JsonService.Delete<Invoice>(id);
        }

        /// <summary>
        /// Vrátí fakturu podle předaného ID -> vyhledá v souboru
        /// </summary>
        /// <param name="id">ID faktury</param>
        /// <returns>Vyhledaná faktura</returns>
        public Invoice GetById(int id)
        {
            return JsonService.GetById<Invoice>(id);
        }

        /// <summary>
        /// Vrátí všechny faktury -> vrátí obsah souboru
        /// </summary>
        /// <returns>List faktur</returns>
        public List<Invoice> GetAll()
        {
            return JsonService.GetAll<Invoice>();
        }

        /// <summary>
        /// Vrátí všechny faktury daného klienta
        /// </summary>
        /// <param name="client">Klient</param>
        /// <returns></returns>
        public List<Invoice> GetAllByClient(Client client)
        {
            List<Invoice> allInvoices = GetAll();
            return allInvoices.Where(invoice => invoice.Client.Id == client.Id).ToList();
        }
        
        /*
         * Metody pro práci s položkami faktury
         */

        /// <summary>
        /// Přidá položku do faktury
        /// </summary>
        /// <param name="invoice">Faktura</param>
        /// <param name="item">Položka pro přidání</param>
        public void AddItem(Invoice invoice, InvoiceItem item)
        {
            item.Id = invoice.InvoiceItems.Count + 1;
            invoice.InvoiceItems.Add(item);
            Update(invoice);
        }

        /// <summary>
        /// Smaže položku faktury
        /// </summary>
        /// <param name="invoice">Faktura</param>
        /// <param name="item">Položka faktury</param>
        public void DeleteItem(Invoice invoice, InvoiceItem item)
        {
            invoice.InvoiceItems.Remove(item);
            Update(invoice);
        }
    }
}
