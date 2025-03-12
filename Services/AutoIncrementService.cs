using InvoicingApp.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.Services
{
    /// <summary>
    /// Třída, která se stará o práci s ID entit
    /// - generování ID
    /// - ukládání posledních ID do souboru
    /// </summary>
    public class AutoIncrementService
    {
        // Soubor na uchovávání posledních ID entit
        private static readonly string LastIdsFile = "LastIds.json";

        // Vlastnost pro uložení posledních ID
        private static LastIds _lastIds;

        /// <summary>
        /// Konstruktor služby, načte poslední ID entit
        /// </summary>
        static AutoIncrementService()
        {
            LoadLastIds();
        }

        /// <summary>
        /// Třída pro uchování posledních ID pro každou entitu
        /// </summary>
        private class LastIds
        {
            public int ClientId { get; set; }
            public int InvoiceId { get; set; }
        }

        /// <summary>
        /// Načtení posledních ID
        /// </summary>
        private static void LoadLastIds()
        {
            if (File.Exists(LastIdsFile))
            {
                var json = File.ReadAllText(LastIdsFile);
                _lastIds = JsonConvert.DeserializeObject<LastIds>(json);
            }
            else
            {
                // Pokud soubor neexistuje, vytvoříme nový a inicializujeme ID na 0
                _lastIds = new LastIds { ClientId = 0, InvoiceId = 0 };
                SaveLastIds();
            }
        }

        /// <summary>
        /// Uložení posledních ID
        /// </summary>
        private static void SaveLastIds()
        {
            var json = JsonConvert.SerializeObject(_lastIds, Formatting.Indented);
            File.WriteAllText(LastIdsFile, json);
        }

        /// <summary>
        /// Generování nového ID pro konkrétní entitu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Nové ID entity</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static int GenerateId<T>()
        {
            switch (typeof(T).Name)
            {
                case nameof(Client):
                    _lastIds.ClientId++;
                    SaveLastIds();
                    return _lastIds.ClientId;

                case nameof(Invoice):
                    _lastIds.InvoiceId++;
                    SaveLastIds();
                    return _lastIds.InvoiceId;

                default:
                    throw new InvalidOperationException("Unknown entity type");
            }
        }

    }
}
