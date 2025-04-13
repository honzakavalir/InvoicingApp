using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        /// <param name="prompt">Zpráva zobrazená uživateli pro zadání vstupu.</param>
        /// <param name="required">Určuje, zda je vstup povinný. Pokud ano, uživatel musí něco zadat.</param>
        /// <param name="regexPattern">Nepovinný regulární výraz pro validaci vstupu.</param>
        /// <param name="regexErrorMsg">Nepovinná vlastní chybová zpráva zobrazená při neplatném vstupu podle regexu.</param>
        /// <returns></returns>
        protected string ReadInput(string prompt, bool required = true, string regexPattern = "", string regexErrorMsg = "")
        {
            string input;
            do
            {
                // Zobrazí výzvu uživateli
                Console.Write(prompt);

                // Načte vstup
                input = Console.ReadLine()?.Trim();

                if (!required || !string.IsNullOrEmpty(input))
                {
                    // Pokud input není prázdný a byl předán regulární výraz, ověří, zda odpovídá formátu
                    if (!string.IsNullOrEmpty(input) && !string.IsNullOrEmpty(regexPattern) && !Regex.IsMatch(input, regexPattern))
                    {
                        // Zobrazí buď výchozí, nebo vlastní chybovou zprávu
                        if (string.IsNullOrEmpty(regexErrorMsg))
                        {
                            Console.WriteLine("Zadaný formát je neplatný, prosím zadejte správně.");
                        }
                        else
                        {
                            Console.WriteLine(regexErrorMsg);
                        }

                        // Po neúspěšné validaci se opakuje zadání
                        continue;
                    } 
                    else
                    {
                        // Vstup je buď prázdný (a povolený), nebo splňuje validaci => ukončí cyklus
                        break;
                    }
                }

                Console.WriteLine("Tato hodnota je povinná, prosím zadejte ji znovu.");
            } while (true);

            return input;
        }

        /// <summary>
        /// Metoda přečte číselný vstup od uživatele a vrátí ho jako int
        /// </summary>
        /// <param name="prompt">Text vstupu</param>
        /// <param name="required">Je parametr povinný</param>
        /// <param name="min">Volitelná minimální hodnota (včetně).</param>
        /// <param name="max">Volitelná maximální hodnota (včetně).</param>
        /// <returns>Číslo zadané uživatelem</returns>
        protected int ReadIntegerInput(string prompt, bool required = true, int? min = null, int? max = null)
        {
            string input;
            int number;

            do
            {
                Console.Write(prompt);
                input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    if (!required) return 0;
                    Console.WriteLine("Tato hodnota je povinná, prosím zadejte ji znovu.");
                    continue;
                }

                if (!int.TryParse(input, out number))
                {
                    Console.WriteLine("Zadejte platné celé číslo.");
                    continue;
                }

                if (min.HasValue && number < min.Value)
                {
                    Console.WriteLine($"Zadané číslo musí být alespoň {min.Value}.");
                    continue;
                }

                if (max.HasValue && number > max.Value)
                {
                    Console.WriteLine($"Zadané číslo nesmí být větší než {max.Value}.");
                    continue;
                }

                break;
            } while (true);

            return number;
        }

        /// <summary>
        /// Metoda načte číselný vstup od uživatele a vrátí ho jako decimal
        /// </summary>
        /// <param name="prompt">Text vstupu</param>
        /// <param name="required">Je parametr povinný</param>
        /// <param name="min">Volitelná minimální hodnota (včetně).</param>
        /// <param name="max">Volitelná maximální hodnota (včetně).</param>
        /// <returns>Vrací desetinné číslo zadané uživatelem.</returns>
        protected decimal ReadDecimalInput(string prompt, bool required = true, decimal? min = null, decimal? max = null)
        {
            string input;
            decimal number;

            do
            {
                Console.Write(prompt);
                input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    if (!required) return 0;
                    Console.WriteLine("Tato hodnota je povinná, prosím zadejte ji znovu.");
                    continue;
                }

                if (!decimal.TryParse(input, out number))
                {
                    Console.WriteLine("Zadejte platné číslo (desetinné oddělujte čárkou nebo tečkou podle systému).");
                    continue;
                }

                if (min.HasValue && number < min.Value)
                {
                    Console.WriteLine($"Zadané číslo musí být alespoň {min.Value}.");
                    continue;
                }

                if (max.HasValue && number > max.Value)
                {
                    Console.WriteLine($"Zadané číslo nesmí být větší než {max.Value}.");
                    continue;
                }

                break;
            } while (true);

            return number;
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
