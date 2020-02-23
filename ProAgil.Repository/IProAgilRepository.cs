using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        //Geral
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        Task<bool> SaveChangesAsync();

        Task<Evento[]> GetAllEventoAsyncByTema(string pTema, bool pIncludePalestrante);
        Task<Evento[]> GetAllEventoAsync(bool pIncludePalestrante);
        Task<Evento> GetEventoAsyncById(int pIdEvento, bool pIncludePalestrante);


        Task<Palestrante[]> GetAllPalestranteAsyncByName(string pNome, bool pIncludeEventos);
        Task<Palestrante[]> GetAllPalestranteAsync(int pIdPalestrante, bool pIncludeEventos);

    }
}