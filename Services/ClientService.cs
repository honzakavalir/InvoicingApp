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
    /// Service pro klienty
    /// </summary>
    public class ClientService : IEntityService<Client>
    {
        /// <summary>
        /// Service pro práci s fakturami
        /// </summary>
        private InvoiceService InvoiceService { get; set; } = new InvoiceService();

        /// <summary>
        /// Vytvoření klienta
        /// </summary>
        /// <param name="client">Klient</param>
        /// <returns></returns>
        public Client Create (Client client)
        {
            client.Id = AutoIncrementService.GenerateId<Client>();

            JsonService.Create(client);
            return client;
        }

        /// <summary>
        /// Aktualizace klienta
        /// </summary>
        /// <param name="updatedClient">Klient</param>
        /// <returns></returns>
        public Client Update(Client updatedClient)
        {
            JsonService.Update(updatedClient);
            return updatedClient;
        }

        /// <summary>
        /// Smazání klienta a jeho faktur
        /// </summary>
        /// <param name="id">ID klienta</param>
        public void Delete(int id)
        {
            Client client = GetById(id);
            List<Invoice> invoices = InvoiceService.GetAllByClient(client);
            foreach (Invoice invoice in invoices)
            {
                InvoiceService.Delete(invoice.Id);
            }
            JsonService.Delete<Client>(id);
        }

        /// <summary>
        /// Získání klienta podle ID
        /// </summary>
        /// <param name="id">ID klienta</param>
        /// <returns></returns>
        public Client GetById(int id)
        {
            return JsonService.GetById<Client>(id);
        }

        /// <summary>
        /// Získání všech klientů
        /// </summary>
        /// <returns></returns>
        public List<Client> GetAll()
        {
            return JsonService.GetAll<Client>();
        }
    }
}
