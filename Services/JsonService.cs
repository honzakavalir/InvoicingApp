using InvoicingApp.Entities;
using InvoicingApp.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.Services
{
    /// <summary>
    /// Třídá, která se stará o práci s JSON soubory
    /// - čtení a ukládání dat
    /// - je static, protože během celého běhu programu existuje pouze jednou
    /// </summary>
    public static class JsonService
    {
        /// <summary>
        /// Metoda, která vrátí název souboru, kam se ukládají data na základě datového typu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Název souboru</returns>
        private static string GetFileName<T>()
        {
            return $"{typeof(T).Name}.json";
        }

        /// <summary>
        /// Získá ID entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">Entita</param>
        /// <returns>ID entity</returns>
        /// <exception cref="InvalidOperationException"></exception>
        private static int GetEntityId<T>(T entity)
        {
            if (entity is Client client)
                return client.Id;
            if (entity is Invoice invoice)
                return invoice.Id;
            if (entity is InvoiceItem invoiceItem)
                return invoiceItem.Id;
            if (entity is Vat vat)
                return vat.Id;

            throw new InvalidOperationException("Unknown entity type");
        }

        /// <summary>
        /// Pomocná metoda pro seřazení seznamu entit podle ID.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">List entit</param>
        /// <returns></returns>
        private static List<T> SortEntitiesById<T>(List<T> entities) where T : IEntity
        {
            return entities.OrderBy(e => GetEntityId(e)).ToList();
        }

        /// <summary>
        /// Vrátí entitu na základě předaného ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">ID entity</param>
        /// <returns></returns>
        public static T GetById<T>(int id) where T : IEntity
        {
            try
            {
                // Získám název souboru
                string fileName = GetFileName<T>();

                // Načtu data
                List<T> entities = GetAll<T>();

                // Najdu entitu podle ID
                var entity = entities.FirstOrDefault(e => GetEntityId(e) == id);

                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při vykonávání metody GetById: {ex.Message}");
                throw;
            }

        }

        /// <summary>
        /// Načte data z JSON souboru
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>List obsahující data daného datového typu</returns>
        public static List<T> GetAll<T>()
        {
            try
            {
                // Získám název souboru
                string fileName = GetFileName<T>();

                if (File.Exists(fileName))
                {
                    // Pokud soubor existuje, přečtu obsah souboru
                    string json = File.ReadAllText(fileName);

                    // Obsah souboru převedu na objekty a vloží se do listu - pokud je obsah JSON souboru prázdný, vrátím prázdný list
                    return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
                }
                else
                {
                    // Pokud soubor neexistuje, vrátím prázdný list
                    return new List<T>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při vykonávání metody GetAll: {ex.Message}");
                return new List<T>();
            }
        }

        /// <summary>
        /// Přidá novou entitu do JSON souboru
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">Entita, která se má uložit</param>
        /// <exception cref="ArgumentException"></exception>
        public static void Create<T>(T entity) where T : IEntity
        {
            try
            {
                // Získám název souboru
                string fileName = GetFileName<T>();

                // Načtu data
                List<T> entities = GetAll<T>();

                // Přidám entitu
                entities.Add(entity);

                // Seřadím seznam entit vzestupně podle ID
                entities = SortEntitiesById(entities);

                // Serializace do JSON
                string json = JsonConvert.SerializeObject(entities, Formatting.Indented);

                // Uložím do souboru
                File.WriteAllText(fileName, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při vykonávání metody Create: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Uloží aktualizovanou verzi entity do JSON souboru
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void Update<T>(T entity) where T : IEntity
        {
            try
            {
                // Získám název souboru
                string fileName = GetFileName<T>();

                // Načtu data
                List<T> entities = GetAll<T>();

                // Najdu existující entitu podle ID
                var existingEntity = entities.FirstOrDefault(e => GetEntityId(e) == GetEntityId(entity));

                if (existingEntity != null)
                {
                    // Aktualizace existující entity
                    entities.Remove(existingEntity);  // Odstraním starou entitu
                    entities.Add(entity);  // Přidám novou verzi (aktualizovanou)

                    // Seřadím seznam entit vzestupně podle ID
                    entities = SortEntitiesById(entities);

                    // Serializace do JSON
                    string json = JsonConvert.SerializeObject(entities, Formatting.Indented);

                    // Uložím do souboru
                    File.WriteAllText(fileName, json);
                }
                else
                {
                    // Pokud entita neexistuje, vyvolám výjimku
                    throw new ArgumentException($"Entity with ID {GetEntityId(entity)} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při vykonávání metody Update: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Smaže entitu z JSON souboru podle předaného ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityId">ID entity ke smazání</param>
        /// <exception cref="ArgumentException"></exception>
        public static void Delete<T>(int entityId) where T : IEntity
        {
            try
            {
                // Získám název souboru
                string fileName = GetFileName<T>();

                // Načtu data
                List<T> entities = GetAll<T>();

                // Najdu entitu podle ID
                T entityToDelete = entities.FirstOrDefault(e => GetEntityId(e) == entityId);

                if (entityToDelete != null)
                {
                    // Odstraním entitu ze seznamu
                    entities.Remove(entityToDelete);

                    // Seřadím seznam entit vzestupně podle ID
                    entities = SortEntitiesById(entities);

                    // Serializace do JSON
                    string json = JsonConvert.SerializeObject(entities, Formatting.Indented);

                    // Zápis do souboru
                    File.WriteAllText(fileName, json);
                }
                else
                {
                    throw new ArgumentException($"Entity with ID {entityId} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při vykonávání metody Delete: {ex.Message}");
                throw;
            }
        }

    }
}
