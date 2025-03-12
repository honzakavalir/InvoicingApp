using InvoicingApp.Core;
using InvoicingApp.Entities;
using InvoicingApp.Services;

namespace InvoicingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppEngine engine = new AppEngine();
            engine.Start();
        }
    }
}
