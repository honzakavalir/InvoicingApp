using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.Interfaces
{
    /// <summary>
    /// Rozhraní pro entity service
    /// - obecná definice entity service
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityService<T> where T : IEntity
    {
        /// <summary>
        /// Vytvoří novou entitu a uloží ji do souboru.
        /// </summary>
        /// <param name="entity">Entita, která má být vytvořena</param>
        /// <returns>Nově vytvořená entita</returns>
        T Create(T entity);

        /// <summary>
        /// Aktualizuje existující entitu podle ID.
        /// </summary>
        /// <param name="updatedEntity">Aktualizovaná entita</param>
        /// <returns>Aktualizovaná entita</returns>
        T Update(T updatedEntity);

        /// <summary>
        /// Smaže entitu podle jejího ID.
        /// </summary>
        /// <param name="id">ID entity, kterou chceme smazat</param>
        void Delete(int id);

        /// <summary>
        /// Načte entitu podle jejího ID.
        /// </summary>
        /// <param name="id">ID entity, kterou chceme načíst</param>
        /// <returns>Načtená entita</returns>
        T GetById(int id);

        /// <summary>
        /// Načte všechny entity.
        /// </summary>
        /// <returns>List všech entit</returns>
        List<T> GetAll();
    }
}
