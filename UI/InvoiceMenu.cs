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
                    EditInvoice();
                    break;
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    DeleteInvoice();
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
            Console.Clear();
            Console.WriteLine("=== Všechny faktury ===");

            List<Invoice> invoices = InvoiceService.GetAll();

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
            Console.WriteLine("=== Všechny faktury ===");
            List<Invoice> invoices = InvoiceService.GetAll();

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
        /// - regulární výrazy byly vygenerovány pomocí AI
        /// </summary>
        private void CreateInvoice()
        {
            Console.Clear();
            List<Client> clients = ClientService.GetAll();
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
            Console.WriteLine();
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
                    invoice.InvoiceNumber = ReadInput("Zadejte číslo faktury: ", true, @"^[A-Za-zÁČĎÉĚÍŇÓŘŠŤÚŮÝŽáčďéěíňóřšťúůýž0-9 ]+$", "Číslo faktury může obsahovat pouze písmena a čísla.");

                    DateTime? issueDate;
                    do
                    {
                        issueDate = ConvertToDateTime(ReadInput("Zadejte datum vystavení (dd.MM.yyyy): "));

                        if (issueDate.HasValue)
                        {
                            if (issueDate.Value < new DateTime(2015, 1, 1))
                            {
                                Console.WriteLine("Datum nesmí být starší než 01.01.2015. Zadejte prosím novější datum.");
                                continue;
                            }

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
                            if (dueDate.Value <= invoice.IssueDate)
                            {
                                Console.WriteLine("Datum splatnosti musí být alespoň o den později než datum vystavení.");
                                continue;
                            }

                            invoice.DueDate = dueDate.Value;
                            break;
                        }
                    } while (true);

                    InvoiceService.Create(invoice);

                    AddInvoiceItem(invoice);
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
        /// - regulární výrazy byly vygenerovány pomocí AI
        /// </summary>
        /// <param name="invoice">Faktura</param>
        private void AddInvoiceItem(Invoice invoice)
        {
            Console.WriteLine();

            do
            {
                Console.WriteLine("=== Přidat položku faktury ===");
                InvoiceItem item = new InvoiceItem();

                item.Name = ReadInput("Název: ", true, @"^[A-Za-zÁČĎÉĚÍŇÓŘŠŤÚŮÝŽáčďéěíňóřšťúůýž0-9 ]+$", "Název položky může obsahovat pouze písmena a čísla.");
                item.Amount = ReadIntegerInput("Počet: ");
                item.UnitPrice = ReadDecimalInput("Jednotková cena (Kč): ");
                item.Discount = ReadIntegerInput("Sleva (%): ", true, 0, 100);

                Console.WriteLine("Chcete aplikovat snížené DPH? (Stiskněte klávesu A pro potvrzení nebo jakoukoliv jinou klávesu pro aplikování standardní sazby 21 %.)");
                ConsoleKeyInfo reducedVat = Console.ReadKey();
                Console.WriteLine();

                if (reducedVat.Key == ConsoleKey.A)
                {
                    Vat vat = VatService.FindBySlug("reduced");
                    item.Vat = vat;
                    Console.WriteLine("Byla aplikována sazba DPH: " + vat.Name);
                }
                else
                {
                    Vat vat = VatService.FindBySlug("basic");
                    item.Vat = vat;
                    Console.WriteLine("Byla aplikována sazba DPH: " + vat.Name);
                }

                InvoiceService.AddItem(invoice, item);
                Console.WriteLine("Položka byla přidána.");
                Console.WriteLine();

                Console.WriteLine("Chcete přidat další položku? (Stiskněte klávesu A pro přidání další položky nebo jakoukoliv jinou klávesu pro ukončení přidávání položek.)");
                ConsoleKeyInfo addAnother = Console.ReadKey();
                Console.WriteLine();

                if (addAnother.Key != ConsoleKey.A)
                    break;

                Console.WriteLine();

            } while (true);

            RenderInvoiceDetail(invoice);
        }

        /// <summary>
        /// Upravení faktury
        /// - regulární výrazy byly vygenerovány pomocí AI
        /// </summary>
        private void EditInvoice()
        {
            Console.Clear();
            Console.WriteLine("=== Všechny faktury ===");
            List<Invoice> invoices = InvoiceService.GetAll();

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
            Console.WriteLine("=== Úprava faktury ===");
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
                    Console.WriteLine("\n=== Detail faktury ===");
                    Console.WriteLine(invoice.ToString());
                    Console.WriteLine();
                    RenderInvoiceItems(invoice);

                    Console.WriteLine();
                    Console.WriteLine("Pokud danou vlastnost nechcete přepsat, odentrujte řádek bez zadání nové hodnoty.");

                    UpdateProperty("Zadejte číslo faktury: ", newValue => invoice.InvoiceNumber = newValue, @"^[A-Za-zÁČĎÉĚÍŇÓŘŠŤÚŮÝŽáčďéěíňóřšťúůýž0-9 ]+$", "Číslo faktury může obsahovat pouze písmena a čísla.");

                    DateTime? issueDate;
                    do
                    {
                        string issueDateString = ReadInput("Zadejte datum vystavení (dd.MM.yyyy): ", false);

                        if (string.IsNullOrEmpty(issueDateString))
                        {
                            break;
                        }

                        issueDate = ConvertToDateTime(issueDateString);

                        if (issueDate.HasValue)
                        {
                            if (issueDate.Value < new DateTime(2015, 1, 1))
                            {
                                Console.WriteLine("Datum nesmí být starší než 01.01.2015. Zadejte prosím novější datum.");
                                continue;
                            }

                            invoice.IssueDate = issueDate.Value;
                            break;
                        }
                    } while (true);

                    DateTime? dueDate;
                    do
                    {
                        string dueDateString = ReadInput("Zadejte datum splatnosti (dd.MM.yyyy): ", false);

                        if (string.IsNullOrEmpty(dueDateString)) 
                        {
                            break;
                        }

                        dueDate = ConvertToDateTime(dueDateString);

                        if (dueDate.HasValue)
                        {
                            if (dueDate.Value <= invoice.IssueDate)
                            {
                                Console.WriteLine("Datum splatnosti musí být alespoň o den později než datum vystavení.");
                                continue;
                            }

                            invoice.DueDate = dueDate.Value;
                            break;
                        }
                    } while (true);

                    Console.WriteLine();
                    Console.WriteLine("Chcete změnit klienta? (Stiskněte klávesu A pro potvrzení nebo jakoukoliv jinou pro odmítnutí): ");
                    ConsoleKeyInfo confirmationChangeClient = Console.ReadKey();

                    if (confirmationChangeClient.Key == ConsoleKey.A)
                    {
                        Console.WriteLine();
                        List<Client> clients = ClientService.GetAll();
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
                        Console.WriteLine();
                        Console.Write("Zadejte ID klienta: ");
                        string inputClient = Console.ReadLine();

                        if (int.TryParse(inputClient, out int clientId))
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
                                Console.WriteLine("Opravdu chcete změnit klienta? (Stiskněte klávesu A pro potvrzení nebo jakoukoliv jinou pro odmítnutí): ");
                                ConsoleKeyInfo confirmationNewClient = Console.ReadKey();

                                if (confirmationNewClient.Key == ConsoleKey.A)
                                {
                                    invoice.Client = client;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Neplatné ID klienta.");
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine("Chcete upravit položky faktury? (Stiskněte klávesu A pro potvrzení nebo jakoukoliv jinou pro odmítnutí): ");
                    ConsoleKeyInfo confirmationChangeItems = Console.ReadKey();

                    if (confirmationChangeItems.Key == ConsoleKey.A)
                    {
                        ConsoleKey anotherEdit;
                        do
                        {
                            Console.WriteLine();
                            Console.Write("Zadejte ID položky: ");
                            string inputItem = Console.ReadLine();

                            if (int.TryParse(inputItem, out int itemId))
                            {
                                InvoiceItem? item = invoice.InvoiceItems.FirstOrDefault(item => item.Id == itemId);
                                if (item == null)
                                {
                                    Console.WriteLine("Položka s tímto ID neexistuje.");
                                    Pause();
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine(item.ToString());
                                    Console.WriteLine();
                                    Console.WriteLine("Pokud danou vlastnost nechcete přepsat, odentrujte řádek bez zadání nové hodnoty.");

                                    string name = ReadInput("Název: ", false, @"^[A-Za-zÁČĎÉĚÍŇÓŘŠŤÚŮÝŽáčďéěíňóřšťúůýž0-9 ]+$", "Název položky může obsahovat pouze písmena a čísla.");
                                    if (!string.IsNullOrEmpty(name))
                                    {
                                        item.Name = name;
                                    }

                                    int amount = ReadIntegerInput("Počet: ", false, 1);
                                    if (amount != 0)
                                    {
                                        item.Amount = amount;
                                    }

                                    decimal unitPrice = ReadDecimalInput("Jednotková cena (Kč): ", false);
                                    if (unitPrice != 0)
                                    {
                                        item.UnitPrice = unitPrice;
                                    }

                                    int discount = ReadIntegerInput("Sleva (%): ", false, 0, 100);
                                    if (item.Discount != discount)
                                    {
                                        item.Discount = discount;
                                    }

                                    Console.WriteLine("Chcete aplikovat snížené DPH? (Stiskněte klávesu A pro potvrzení nebo jakoukoliv jinou klávesu pro aplikování standardní sazby 21 %.)");
                                    ConsoleKeyInfo reducedVat = Console.ReadKey();
                                    Console.WriteLine();

                                    if (reducedVat.Key == ConsoleKey.A)
                                    {
                                        Vat vat = VatService.FindBySlug("reduced");
                                        item.Vat = vat;
                                        Console.WriteLine("Byla aplikována sazba DPH: " + vat.Name);
                                    }
                                    else
                                    {
                                        Vat vat = VatService.FindBySlug("basic");
                                        item.Vat = vat;
                                        Console.WriteLine("Byla aplikována sazba DPH: " + vat.Name);
                                    }

                                    Console.WriteLine();
                                    Console.WriteLine("Položka byla úspěšně upravena.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Neplatné ID položky.");
                            }

                            Console.WriteLine();
                            Console.Write("Chcete upravit další položku? (Stiskněte klávesu A pro potvrzení nebo jakoukoliv jinou pro odmítnutí): ");
                            anotherEdit = Console.ReadKey().Key;
                            Console.WriteLine();

                        } while (anotherEdit == ConsoleKey.A);
                    }


                    InvoiceService.Update(invoice);
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
        /// Smazání faktury
        /// </summary>
        private void DeleteInvoice()
        {
            Console.Clear();
            Console.WriteLine("=== Všechny faktury ===");
            List<Invoice> invoices = InvoiceService.GetAll();

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
            Console.WriteLine("=== Smazání faktury ===");
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
                    Console.WriteLine("\n=== Detail faktury ===");
                    Console.WriteLine(invoice.ToString());

                    Console.Write("\nOpravdu chcete smazat tuto fakturu? (Stiskněte klávesu A pro potvrzení nebo jakoukoliv jinou pro odmítnutí): ");
                    ConsoleKeyInfo confirmation = Console.ReadKey();

                    if (confirmation.Key == ConsoleKey.A)
                    {
                        InvoiceService.Delete(invoiceId);
                        Console.WriteLine();
                        Console.WriteLine("Faktura byla úspěšně smazána.");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Smazání faktury bylo zrušeno.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Neplatné ID faktury.");
            }

            Pause();
        }
    }
}
