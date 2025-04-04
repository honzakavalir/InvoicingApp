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
    /// Menu pro faktury
    /// </summary>
    public class InvoiceMenu : BaseMenu
    {

        /// <summary>
        /// Service pro práci s fakturami
        /// </summary>
        private InvoiceService InvoiceService { get; set; } = new InvoiceService();

        /// <summary>
        /// Service pro práci s klienty
        /// </summary>
        private ClientService ClientService { get; set; } = new ClientService();


        /// <summary>
        /// Vykreslení menu faktur
        /// </summary>
        protected override void Render()
        {
            Console.Clear();
            Console.WriteLine("=== Faktury ===");
            Console.WriteLine("1. Zobrazit všechny faktury");
            Console.WriteLine("2. Zobrazit fakturu");
            Console.WriteLine("3. Vytvořit fakturu");
            Console.WriteLine("4. Upravit fakturu");
            Console.WriteLine("5. Smazat fakturu");
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
                    OpenInvoiceList();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    OpenInvoiceDetail();
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    CreateInvoice();
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    Pause();
                    break;
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    Pause();
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
        /// Otevření seznamu všech faktur
        /// </summary>
        private void OpenInvoiceList() 
        {
            List<Invoice> invoices = InvoiceService.GetAll();

            Console.Clear();
            Console.WriteLine("=== Všechny faktury ===");

            if (invoices.Count == 0)
            {
                Console.WriteLine("Žádné faktury k zobrazení.");
            }
            else
            {
                foreach (Invoice invoice in invoices)
                {
                    Console.WriteLine(invoice.ToString());
                    Console.WriteLine("-----------------------------------");
                }
            }
            Pause();
        }

        /// <summary>
        /// Otevření detailu faktury
        /// </summary>
        private void OpenInvoiceDetail()
        {
            Console.Clear();
            Console.WriteLine("=== Detail faktury ===");
            Console.Write("Zadejte ID faktury: ");

            string input = Console.ReadLine();

            if (int.TryParse(input, out int invoiceId))
            {
                Invoice invoice = InvoiceService.GetById(invoiceId);

                if (invoice == null)
                {
                    Console.WriteLine("Faktura s tímto ID neexistuje.");
                }
                else
                {
                    RenderInvoiceDetail(invoice);
                }
            }
            else
            {
                Console.WriteLine("Neplatné ID faktury.");
            }

            Pause();
        }

        /// <summary>
        /// Vytvoření faktury
        /// </summary>
        private void CreateInvoice()
        {
            Console.Clear();
            Console.WriteLine("=== Vytvoření faktury ===");
            Console.Write("Zadejte ID klienta, kterému chcete vytvořit fakturu: ");
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
                    Console.WriteLine();
                    Console.WriteLine("=== Detail klienta ===");
                    Console.WriteLine(client.ToString());
                    Console.WriteLine();
                    Console.WriteLine("=== Zadejte údaje o faktuře ===");
                    Invoice invoice = new Invoice();
                    invoice.Client = client;
                    invoice.InvoiceNumber = ReadInput("Zadejte číslo faktury: ");

                    DateTime? issueDate;
                    do
                    {
                        issueDate = ConvertToDateTime(ReadInput("Zadejte datum vystavení (dd.MM.yyyy): "));

                        if (issueDate.HasValue)
                        {
                            invoice.IssueDate = issueDate.Value;
                            break;
                        }
                    } while (true);

                    DateTime? dueDate;
                    do
                    {
                        dueDate = ConvertToDateTime(ReadInput("Zadejte datum splatnosti (dd.MM.yyyy): "));

                        if (dueDate.HasValue)
                        {
                            invoice.DueDate = dueDate.Value;
                            break;
                        }
                    } while (true);

                    AddInvoiceItem(invoice);
                    Pause();
                }
            }
            else
            {
                Console.WriteLine("Neplatné ID klienta.");
            }
            Pause();
        }

        /// <summary>
        /// Vykreslení detailu faktury
        /// </summary>
        /// <param name="invoice">Faktura</param>
        private void RenderInvoiceDetail(Invoice invoice)
        {
            Console.Clear();
            Console.WriteLine("=== Detail faktury ===");
            Console.WriteLine(invoice.ToString());
            Console.WriteLine();
            RenderInvoiceItems(invoice);
        }

        /// <summary>
        /// Vykreslení položek faktury
        /// </summary>
        /// <param name="invoice">Faktura</param>
        private void RenderInvoiceItems(Invoice invoice)
        {
            Console.WriteLine("Položky faktury:");
            foreach (InvoiceItem item in invoice.InvoiceItems)
            {
                Console.WriteLine(item.ToString());
            }
        }

        /// <summary>
        /// Přidání položky do faktury
        /// </summary>
        /// <param name="invoice">Faktura</param>
        private void AddInvoiceItem(Invoice invoice)
        {
            Console.WriteLine();
            Console.WriteLine("=== Přidat položku faktury ===");
            InvoiceItem item = new InvoiceItem();
            item.Name = ReadInput("Název: ");
        }
    }
}
