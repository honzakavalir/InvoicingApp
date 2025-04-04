using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.UI
{
    /// <summary>
    /// Základní třída pro všechny typy menu
    /// </summary>
    public abstract class BaseMenu
    {
        /// <summary>
        /// Proměnná, která sleduje stav menu
        /// </summary>
        protected bool IsMenuRunning { get; set; }

        /// <summary>
        /// Metoda, která vykreslí dané menu
        /// </summary>
        protected abstract void Render();

        /// <summary>
        /// Tato metoda zpracuje vstup od uživatele
        /// </summary>
        protected abstract void HandleUserInput();

        /// <summary>
        /// Spuštění menu
        /// </summary>
        public void Run()
        {
            // Nastavím, že menu běží a poté vykreslím a čekám na vstup od uživatele
            IsMenuRunning = true;
            while (IsMenuRunning)
            {
                Render();
                HandleUserInput();
            }
        }

        /// <summary>
        /// Pozastavení pro čekání na vstup
        /// </summary>
        protected void Pause()
        {
            Console.WriteLine("\nStiskněte libovolnou klávesu pro návrat...");
            Console.ReadKey();
        }

        /// <summary>
        /// Ukončí menu
        /// </summary>
        protected void Exit()
        {
            IsMenuRunning = false;
        }

        /// <summary>
        /// Metoda přečte vstup od uživatele a vrátí ho
        /// </summary>
        /// <param name="prompt">Text vstupu</param>
        /// <param name="required">Je parametr povinný</param>
        /// <returns></returns>
        protected string ReadInput(string prompt, bool required = true)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine()?.Trim();

                if (!required || !string.IsNullOrEmpty(input))
                {
                    break;
                }

                Console.WriteLine("Tato hodnota je povinná, prosím zadejte ji znovu.");
            } while (true);

            return input;
        }

        /// <summary>
        /// Metoda, která aktualizuje vlastnost objektu na základě vstupu od uživatele
        /// </summary>
        /// <param name="prompt">Text vstupu</param>
        /// <param name="property">Vlastnost k aktualizaci</param>
        protected void UpdateProperty(string prompt, Action<string> setProperty)
        {
            // Získám vstup od uživatele
            string value = ReadInput(prompt, false);

            // Pokud vstup není prázdný tak nastavím
            if (!string.IsNullOrEmpty(value))
            {
                // Zavolám předanou akci, která nastaví hodnotu do příslušné vlastnosti
                setProperty(value);
            }
        }

        /// <summary>
        /// Metoda, která převede date string na datum
        /// </summary>
        /// <param name="input"></param>
        /// <param name="format"></param>
        protected DateTime? ConvertToDateTime(string input, string format = "dd.MM.yyyy")
        {
            DateTime parsedDate;

            if (DateTime.TryParseExact(input, format, null, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return parsedDate; 
            }
            else
            {
                Console.WriteLine($"Neplatný formát data. Očekává se formát {format}.");
                return null;
            }
        }

    }
}
