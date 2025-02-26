using InvoicingApp.Entities;

namespace InvoicingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Vytvoření testových dat pro klienta
            var client1 = new Client(1, "Jan Novák", "Hlavní 123", "Praha", "10000", "Česká republika", "jan.novak@email.cz", "+420123456789", null, null);
            var client2 = new Client(2, "Petr Svoboda", "Vedlejší 456", "Brno", "60000", "Česká republika", "petr.svoboda@email.cz", "+420987654321", null, null);

            // Vytvoření testových dat pro VAT
            var vatStandard = new Vat(1, "Standard", "standard", 21);
            var vatReduced = new Vat(2, "Snížená", "reduced", 10);

            // Vytvoření testových položek pro faktury
            var invoiceItem1 = new InvoiceItem(1, "Produkt A", 100m, 2, 0, vatStandard);
            var invoiceItem2 = new InvoiceItem(2, "Produkt B", 50m, 3, 0, vatStandard);

            // Vytvoření testové faktury pro klienta 1
            var invoice1 = new Invoice(1, "Faktura001", client1, DateTime.Now, DateTime.Now.AddDays(30), new List<InvoiceItem> { invoiceItem1, invoiceItem2 });

            // Vytvoření testové faktury pro klienta 2
            var invoice2 = new Invoice(2, "Faktura002", client2, DateTime.Now, DateTime.Now.AddDays(30), new List<InvoiceItem> { invoiceItem1 });

            // Výpis testových faktur
            Console.WriteLine("Načtené faktury:");
            Console.WriteLine($"Faktura č.: {invoice1.InvoiceNumber} | Klient: {invoice1.Client.Name} | Celková cena bez DPH: {invoice1.TotalPriceAfterDiscount()} | Celková cena: {invoice1.TotalPriceWithVat():C}");
            Console.WriteLine($"Faktura č.: {invoice2.InvoiceNumber} | Klient: {invoice2.Client.Name} | Celková cena bez DPH: {invoice1.TotalPriceAfterDiscount()} | Celková cena: {invoice2.TotalPriceWithVat():C}");

            // Výpis testových klientů
            Console.WriteLine("\nNačtení klienti:");
            Console.WriteLine($"Klient: {client1.Name} | Email: {client1.Email} | Telefon: {client1.Phone}");
            Console.WriteLine($"Klient: {client2.Name} | Email: {client2.Email} | Telefon: {client2.Phone}");
        }
    }
}
