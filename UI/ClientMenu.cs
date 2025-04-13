using InvoicingApp.Entities;
using InvoicingApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.UI
{
    /// <summary>
    /// Menu pro klienty
    /// </summary>
    public class ClientMenu : BaseMenu
    {
        /// <summary>
        /// Service pro práci s klienty
        /// </summary>
        private ClientService ClientService { get; set; } = new ClientService();

        /// <summary>
        /// Service pro práci s fakturami
        /// </summary>
        private InvoiceService InvoiceService { get; set; } = new InvoiceService();

        /// <summary>
        /// Vykreslení menu klientů
        /// </summary>
        protected override void Render()
        {
            Console.Clear();
            Console.WriteLine("=== Klienti ===");
            Console.WriteLine("1. Zobrazit všechny klienty");
            Console.WriteLine("2. Zobrazit klienta");
            Console.WriteLine("3. Vytvořit klienta");
            Console.WriteLine("4. Upravit klienta");
            Console.WriteLine("5. Smazat klienta");
            Console.WriteLine("6. Zpět do hlavního menu");
            Console.Write("Vyberte možnost: ");
        }

        /// <summary>
        /// Zpracování vstupu od uživatele
        /// </summary>
        protected override void HandleUserInput()
        {
            // Zachytím stisknutou klávesu
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            Console.WriteLine();

            // Provedu akci podle klávesy
            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    OpenClientList();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    OpenClientDetail();
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    CreateClient();
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    EditClient();
                    break;
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    DeleteClient();
                    break;
                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:
                    Exit();
                    break;
                default:
                    Console.WriteLine("\nNeplatná volba, zkuste to znovu.");
                    Pause();
                    break;
            }
        }

        /// <summary>
        /// Otevření seznamu všech klientů
        /// </summary>
        private void OpenClientList()
        {
            List<Client> clients = ClientService.GetAll();

            Console.Clear();
            Console.WriteLine("=== Klienti v systému ===");

            if (clients.Count == 0)
            {
                Console.WriteLine("V systému nejsou žádní klienti.");
            }
            else
            {
                foreach (Client client in clients)
                {
                    Console.WriteLine(client.ToString());
                    Console.WriteLine("-----------------------------------");
                }
            }
            Pause();
        }

        /// <summary>
        /// Otevření detailu klienta
        /// </summary>
        private void OpenClientDetail()
        {
            Console.Clear();
            Console.WriteLine("=== Detail klienta ===");
            Console.Write("Zadejte ID klienta: ");

            string input = Console.ReadLine();

            if (int.TryParse(input, out int clientId))
            {
                Client client = ClientService.GetById(clientId);

                if (client == null)
                {
                    Console.WriteLine("Klient s tímto ID neexistuje.");
                }
                else
                {
                    RenderClientDetail(client);
                }
            }
            else
            {
                Console.WriteLine("Neplatné ID faktury.");
            }

            Pause();
        }

        /// <summary>
        /// Vykreslení detailu klienta
        /// </summary>
        /// <param name="client">Klient</param>
        private void RenderClientDetail(Client client)
        {
            Console.Clear();
            Console.WriteLine("=== Detail klienta ===");
            Console.WriteLine(client.ToString());
            Console.WriteLine();

            List<Invoice> invoices = InvoiceService.GetAllByClient(client);
            Console.WriteLine("Faktury klienta:");
            if (invoices.Count == 0)
            {
                Console.WriteLine("Žádné faktury k zobrazení");
            } 
            else
            {
                foreach (Invoice invoice in invoices) 
                {
                    Console.WriteLine(invoice.ToString());
                    Console.WriteLine("-------------------");
                }
            }
        }

        /// <summary>
        /// Vytvoření klienta
        /// Reguálrní výrazy byly vygenerovány pomocí AI
        /// </summary>
        private void CreateClient()
        {
            Console.Clear();
            Console.WriteLine("=== Vytvoření klienta ===");
            Client client = new Client();

            client.Name = ReadInput("Zadejte název/jméno klienta: ", true , @"^[A-Za-zÁČĎÉĚÍŇÓŘŠŤÚŮÝŽáčďéěíňóřšťúůýž0-9 ]+$", "Název klienta může obsahovat pouze písmena a čísla.");
            client.Address = ReadInput("Zadejte ulici a č.p.: ", true, @"^[A-Za-zÁČĎÉĚÍŇÓŘŠŤÚŮÝŽáčďéěíňóřšťúůýž0-9 ]+$", "Ulice a č.p. může obsahovat pouze písmena a čísla.");
            client.City = ReadInput("Zadejte město: ", true, @"^[A-Za-zÁČĎÉĚÍŇÓŘŠŤÚŮÝŽáčďéěíňóřšťúůýž ]+$", "Město může obsahovat pouze písmena.");
            client.PostalCode = ReadInput("Zadejte PSČ: ", true, @"^\d{5}$", "Zadejte PSČ ve formátu 12345");
            client.Country = ReadInput("Zadejte zemi: ", true, @"^[A-Za-zÁČĎÉĚÍŇÓŘŠŤÚŮÝŽáčďéěíňóřšťúůýž ]+$", "Země může obsahovat pouze písmena.");
            client.Email = ReadInput("Zadejte emailovou adresu: ", true, @"^[\w\.-]+@[\w\.-]+\.\w{2,}$", "Zadejte validní emailovou adresu.");
            client.Phone = ReadInput("Zadejte telefonní číslo: ", true, @"^\+\d{3}\d{9}$", "Zadejte telefonní číslo ve formátu +420123456789.");
            client.IdentificationNumber = ReadInput("Zadejte IČO: ", false, @"^\d{8}$", "Zadejte IČO ve formátu 12345678.");
            client.VatNumber = ReadInput("Zadejte DIČ: ", false, @"^[A-Za-z]{2}\d{8}$", "Zadejte DIČ ve formátu CZ12345678.");

            ClientService.Create(client);

            RenderClientDetail(client);
            Pause();
        }

        /// <summary>
        /// Upravení klienta
        /// </summary>
        private void EditClient()
        {
            Console.Clear();
            Console.WriteLine("=== Úprava klienta ===");
            Console.Write("Zadejte ID klienta: ");

            string input = Console.ReadLine();

            if (int.TryParse(input, out int clientId))
            {
                Client client = ClientService.GetById(clientId);

                if (client == null)
                {
                    Console.WriteLine("Klient s tímto ID neexistuje.");
                }
                else
                {
                    Console.WriteLine("\n=== Detail klienta ===");
                    Console.WriteLine(client.ToString());

                    Console.WriteLine();
                    Console.WriteLine("Pokud danou vlastnost nechcete přepsat, odentrujte řádek bez zadání nové hodnoty.");

                    UpdateProperty("Zadejte jméno: ", newValue => client.Name = newValue);
                    UpdateProperty("Zadejte adresu: ", newValue => client.Address = newValue);
                    UpdateProperty("Zadejte město: ", newValue => client.City = newValue);
                    UpdateProperty("Zadejte PSČ: ", newValue => client.PostalCode = newValue);
                    UpdateProperty("Zadejte zemi: ", newValue => client.Country = newValue);
                    UpdateProperty("Zadejte emailovou adresu: ", newValue => client.Email = newValue);
                    UpdateProperty("Zadejte telefonní číslo: ", newValue => client.Phone = newValue);
                    UpdateProperty("Zadejte IČO: ", newValue => client.IdentificationNumber = newValue);
                    UpdateProperty("Zadejte DIČ: ", newValue => client.VatNumber = newValue);

                    ClientService.Update(client);
                    RenderClientDetail(client);
                }
            }
            else
            {
                Console.WriteLine("Neplatné ID klienta.");
            }

            Pause();
        }

        /// <summary>
        /// Smazání klienta
        /// </summary>
        private void DeleteClient()
        {
            Console.Clear();
            Console.WriteLine("=== Smazání klienta ===");
            Console.Write("Zadejte ID klienta: ");

            string input = Console.ReadLine();

            if (int.TryParse(input, out int clientId))
            {
                Client client = ClientService.GetById(clientId);

                if (client == null)
                {
                    Console.WriteLine("Klient s tímto ID neexistuje.");
                }
                else
                {
                    Console.WriteLine("\n=== Detail klienta ===");
                    Console.WriteLine(client.ToString());

                    Console.Write("\nOpravdu chcete smazat tohoto klienta? (A/N): ");
                    ConsoleKeyInfo confirmation = Console.ReadKey();

                    if (confirmation.Key == ConsoleKey.A)
                    {
                        ClientService.Delete(clientId);
                        Console.WriteLine();
                        Console.WriteLine("Klient byl úspěšně smazán.");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Smazání klienta bylo zrušeno.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Neplatné ID klienta.");
            }

            Pause();
        }
    }
}
