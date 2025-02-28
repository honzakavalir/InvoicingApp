using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.Interfaces
{
    /// <summary>
    /// Rozhraní pro entitu 
    /// - obecná definice entity
    /// - každá entita v programu musí realizovat toto rozhraní
    /// </summary>
    public interface IEntity
    {
        int Id { get; set; }
    }
}
