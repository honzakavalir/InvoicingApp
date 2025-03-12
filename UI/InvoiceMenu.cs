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
    public class InvoiceMenu
    {
        // Proměnná pro sledování stavu menu faktur
        private bool IsInvoiceMenuRunning { get; set; }

        // Service pro práci s fakturami
        private InvoiceService InvoiceService { get; set; } = new InvoiceService();

        /// <summary>
        /// Spuštění menu faktur
        /// </summary>
        public void Run()
        {
            // Nastavím, že menu faktur běží a poté vykreslím a čekám na vstup od uživatele
            IsInvoiceMenuRunning = true;
            while (IsInvoiceMenuRunning)
            {
                Render();
                HandleUserInput();
            }
        }

        /// <summary>
        /// Vykreslení menu faktur
        /// </summary>
        private void Render()
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
        /// Čekám na vstup od uživatele
        /// </summary>
        private void HandleUserInput()
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
        /// Otevřu seznam všech faktur
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
                    Console.WriteLine($"ID faktury: {invoice.Id}");
                    Console.WriteLine($"Číslo faktury: {invoice.InvoiceNumber}");
                    Console.WriteLine($"Datum vystavení: {invoice.IssueDate.ToString("dd.MM.yyyy")}");
                    Console.WriteLine($"Datum splatnosti: {invoice.DueDate.ToString("dd.MM.yyyy")}");
                    Console.WriteLine($"Zákazník: {invoice.Client.Name}");
                    Console.WriteLine($"Celková částka bez DPH: {invoice.TotalPriceAfterDiscount().ToString("0.00")} Kč");
                    Console.WriteLine($"Celková částka včetně DPH: {invoice.TotalPriceWithVat().ToString("0.00")} Kč");
                    Console.WriteLine("-----------------------------------");
                }
            }
            Pause();
        }

        /// <summary>
        /// Otevřu detail faktury
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
        /// Vytvořím fakturu
        /// </summary>
        private void CreateInvoice()
        {
            Console.Clear();
            Console.WriteLine("=== Vytvoření faktury ===");
            Invoice invoice = new Invoice();
            Pause();
        }

        /// <summary>
        /// Vykreslím detail faktury
        /// </summary>
        /// <param name="invoice">Faktura</param>
        private void RenderInvoiceDetail(Invoice invoice)
        {
            Console.Clear();
            Console.WriteLine("=== Detail faktury ===");
            Console.WriteLine($"ID faktury: {invoice.Id}");
            Console.WriteLine($"Číslo faktury: {invoice.InvoiceNumber}");
            Console.WriteLine($"Datum vystavení: {invoice.IssueDate.ToString("dd.MM.yyyy")}");
            Console.WriteLine($"Datum splatnosti: {invoice.DueDate.ToString("dd.MM.yyyy")}");
            Console.WriteLine($"Zákazník: {invoice.Client.Name}");
            Console.WriteLine($"Celková částka bez DPH: {invoice.TotalPriceAfterDiscount()} Kč");
            Console.WriteLine($"Celková částka včetně DPH: {invoice.TotalPriceWithVat()} Kč");
            Console.WriteLine();
            RenderInvoiceItems(invoice);
        }

        /// <summary>
        /// Vykreslím položky faktury
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
        /// Zavře menu faktur
        /// </summary>
        private void Exit()
        {
            IsInvoiceMenuRunning = false;
        }

        /// <summary>
        /// Pozastavení
        /// </summary>
        private void Pause()
        {
            Console.WriteLine("\nStiskněte libovolnou klávesu pro návrat do menu...");
            Console.ReadKey();
        }
    }
}
