using InvoicingApp.Entities;
using InvoicingApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.Services
{
    public class ClientService : IEntityService<Client>
    {
        public Client Create (Client client)
        {
            client.Id = AutoIncrementService.GenerateId<Client>();

            JsonService.Create(client);
            return client;
        }

        public Client Update(Client updatedClient)
        {
            JsonService.Update(updatedClient);
            return updatedClient;
        }

        public void Delete(int id)
        {
            JsonService.Delete<Client>(id);
        }

        public Client GetById(int id)
        {
            return JsonService.GetById<Client>(id);
        }

        public List<Client> GetAll()
        {
            return JsonService.GetAll<Client>();
        }
    }
}
