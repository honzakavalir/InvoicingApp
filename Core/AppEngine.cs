using InvoicingApp.Entities;
using InvoicingApp.Services;
using InvoicingApp.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.Core
{
    /// <summary>
    /// Třída, která spustí celou aplikaci
    /// </summary>
    public class AppEngine
    {
        public AppMenu AppMenu { get; set; } = new AppMenu();

        /// <summary>
        /// Spustí hlavní menu aplikace a inicializuje sazby DPH
        /// </summary>
        public void Start()
        {
            VatService.InitializeVatFile();
            AppMenu.Run();
        }

    }
}
