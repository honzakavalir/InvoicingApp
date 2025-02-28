using InvoicingApp.Entities;
using InvoicingApp.Services;

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
            var invoiceItem1 = new InvoiceItem("Produkt A", 100m, 2, 0, vatStandard);
            var invoiceItem2 = new InvoiceItem("Produkt B", 50m, 3, 0, vatStandard);

            // Vytvoření testové faktury pro klienta 1
            var invoice1 = new Invoice("Faktura001", client1, DateTime.Now, DateTime.Now.AddDays(30));

            // Vytvoření testové faktury pro klienta 2
            var invoice2 = new Invoice("Faktura003", client1, DateTime.Now, DateTime.Now.AddDays(30));

            // Vytvoření service
            var invoiceService = new InvoiceService();
            var invoiceItemService = new InvoiceItemService();
            invoiceItemService.RemoveFromInvoice(1, 1);


        }
    }
}
