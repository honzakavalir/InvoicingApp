using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.UI
{
    /// <summary>
    /// Hlavní menu aplikace
    /// </summary>
    public class AppMenu : BaseMenu
    {

        /// <summary>
        /// Menu faktur
        /// </summary>
        private InvoiceMenu InvoiceMenu { get; set; } = new InvoiceMenu();

        /// <summary>
        /// Menu klientů
        /// </summary>
        private ClientMenu ClientMenu { get; set; } = new ClientMenu();

        /// <summary>
        /// Vykreslení hlavního menu
        /// </summary>
        protected override void Render()
        {
            Console.Clear();
            Console.WriteLine("=== Fakturační program ===");
            Console.WriteLine("1. Faktury");
            Console.WriteLine("2. Klienti");
            Console.WriteLine("3. Konec");
            Console.Write("Vyberte možnost: ");
        }

        /// <summary>
        /// Zpracování vstupu od uživatele
        /// </summary>
        protected override void HandleUserInput()
        {
            // Přečti stisknutou klávesu
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            Console.WriteLine();

            // Podle stisknuté klávesy vykonej akci
            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    InvoiceMenu.Run();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    ClientMenu.Run();
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    Exit();
                    break;
                default:
                    Console.WriteLine("\nNeplatná volba, zkuste to znovu.");
                    Pause();
                    break;
            }
        }
    }
}
