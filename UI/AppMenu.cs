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
    public class AppMenu
    {
        // Proměnná, která řídí jestli aplikace běží nebo ne
        private bool IsAppMenuRunning { get; set; } = true;

        // Menu pro faktury
        private InvoiceMenu InvoiceMenu { get; set; } = new InvoiceMenu();

        /// <summary>
        /// Metoda pro spuštění hlavního menu
        /// </summary>
        public void Run()
        {
            // Dokud běží aplikace vykresluj hlavní menu a očekávej vstup od uživatele
            while (IsAppMenuRunning)
            {
                Render();
                HandleUserInput();
            }
        }

        /// <summary>
        /// Vykreslení hlavního menu
        /// </summary>
        private void Render()
        {
            Console.Clear();
            Console.WriteLine("=== Fakturační program ===");
            Console.WriteLine("1. Faktury");
            Console.WriteLine("2. Klienti");
            Console.WriteLine("3. Konec");
            Console.Write("Vyberte možnost: ");
        }

        /// <summary>
        /// Očekávání vstupu uživatele
        /// </summary>
        private void HandleUserInput()
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
                    Pause();
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

        /// <summary>
        /// Ukončení hlavního menu = ukončení programu
        /// </summary>
        private void Exit()
        {
            Console.WriteLine("\nUkončuji program...");
            IsAppMenuRunning = false;
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
