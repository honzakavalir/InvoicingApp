using InvoicingApp.Core;
using InvoicingApp.Entities;
using InvoicingApp.Services;

namespace InvoicingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Vytvořím instanci app engine a zavolám metodu Start
            AppEngine engine = new AppEngine();
            engine.Start();
        }
    }
}
