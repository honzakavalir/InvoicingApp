﻿using System;
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
        /// - metoda byla částečně zpracována pomocí AI
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

                // Pokud input není prázdný nebo je nepovinný
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

            // Vrátím input od uživatele
            return input;
        }

        /// <summary>
        /// Metoda přečte celočíselný vstup od uživatele a vrátí ho.
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
                // Zobrazí výzvu uživateli
                Console.Write(prompt);

                // Načte vstup
                input = Console.ReadLine()?.Trim();

                // Pokud je vstup prázdný
                if (string.IsNullOrEmpty(input))
                {
                    if (!required) return 0;
                    Console.WriteLine("Tato hodnota je povinná, prosím zadejte ji znovu.");
                    continue;
                }

                // Pokud uživatel nezadal celé číslo
                if (!int.TryParse(input, out number))
                {
                    Console.WriteLine("Zadejte platné celé číslo.");
                    continue;
                }

                // Pokud je zadáno minimum a číslo to nesplňuje
                if (min.HasValue && number < min.Value)
                {
                    Console.WriteLine($"Zadané číslo musí být alespoň {min.Value}.");
                    continue;
                }

                // Pokud je zadáno maximum a číslo to nesplňuje
                if (max.HasValue && number > max.Value)
                {
                    Console.WriteLine($"Zadané číslo nesmí být větší než {max.Value}.");
                    continue;
                }

                break;
            } while (true);

            // Vrátím input od uživatele
            return number;
        }

        /// <summary>
        /// Metoda načte číselný vstup od uživatele a vrátí ho.
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
                // Zobrazí výzvu uživateli
                Console.Write(prompt);

                // Načte vstup
                input = Console.ReadLine()?.Trim();

                // Pokud je vstup prázdný
                if (string.IsNullOrEmpty(input))
                {
                    if (!required) return 0;
                    Console.WriteLine("Tato hodnota je povinná, prosím zadejte ji znovu.");
                    continue;
                }

                // Pokud uživatel nezadal validní číslo
                if (!decimal.TryParse(input, out number))
                {
                    Console.WriteLine("Zadejte platné číslo (desetinné oddělujte čárkou nebo tečkou podle systému).");
                    continue;
                }

                // Pokud je zadáno minimum a číslo to nesplňuje
                if (min.HasValue && number < min.Value)
                {
                    Console.WriteLine($"Zadané číslo musí být alespoň {min.Value}.");
                    continue;
                }

                // Pokud je zadáno maximum a číslo to nesplňuje
                if (max.HasValue && number > max.Value)
                {
                    Console.WriteLine($"Zadané číslo nesmí být větší než {max.Value}.");
                    continue;
                }

                break;
            } while (true);

            // Vrátím input od uživatele
            return number;
        }

        /// <summary>
        /// Metoda, která aktualizuje vlastnost objektu na základě vstupu od uživatele
        /// - metoda byla částečně zpracována pomocí AI
        /// </summary>
        /// <param name="prompt">Text vstupu</param>
        /// <param name="property">Vlastnost k aktualizaci</param>
        /// <param name="regexPattern">Volitelný regulární výraz pro validaci vstupu</param>
        /// <param name="regexErrorMsg">Zpráva, která se zobrazí při neplatném vstupu</param>
        protected void UpdateProperty(string prompt, Action<string> setProperty, string regexPattern = "", string regexErrorMsg = "")
        {
            // Získám vstup od uživatele
            string value = ReadInput(prompt, false, regexPattern, regexErrorMsg);

            // Pokud vstup není prázdný
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
