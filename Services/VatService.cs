using InvoicingApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.Services
{
    /// <summary>
    /// Service pro práci s DPH
    /// - je static, protože během celého běhu programu existuje pouze jednou
    /// </summary>
    public static class VatService
    {
        private static readonly string vatFileName = "Vat.json";

        /// <summary>
        /// Zkontroluje, jestli aplikace má k dispozici soubor Vat.json, kde jsou uloženy sazby DPH.
        /// Pokud ne, vytvoří ho.
        /// </summary>
        public static void InitializeVatFile()
        {
            // Pokud soubor neexistuje, vytvoříme ho a přidáme základní sazby
            if (!File.Exists(vatFileName))
            {
                // Vytvoření seznamu DPH sazeb
                List<Vat> vats = new List<Vat>
                {
                    new Vat(1, "21%", "basic", 21),
                    new Vat(2, "12%", "reduced", 12)
                };

                // Uložení základních sazeb do souboru
                foreach (Vat vat in vats)
                {
                    JsonService.Create<Vat>(vat);
                }
            }
        }

        /// <summary>
        /// Vrátí objekt DPH podle slugu
        /// </summary>
        /// <param name="slug">slug</param>
        /// <returns></returns>
        public static Vat FindBySlug(string slug)
        {
            List<Vat> vats = JsonService.GetAll<Vat>();
            Vat? searchedVat = vats.FirstOrDefault(vat => vat.Slug == slug);

            if (searchedVat == null)
            {
                return FindBySlug("basic");
            }

            return searchedVat;
        }
    }
}
