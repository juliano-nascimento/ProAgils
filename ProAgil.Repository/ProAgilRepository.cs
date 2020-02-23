using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        public readonly ProAgilContext _Context;

        public ProAgilRepository(ProAgilContext Context)
        {
            _Context = Context;
        }
        public void Add<T>(T entity) where T : class
        {
            _Context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _Context.Update(entity);
        }       

        public void Delete<T>(T entity) where T : class
        {
            _Context.Remove(entity);
        }
          public async Task<bool> SaveChangesAsync()
        {
            return (await _Context.SaveChangesAsync()) > 0;
        }

        public async Task<Evento[]> GetAllEventoAsync(bool pIncludePalestrante = false)
        {
            IQueryable<Evento> query = _Context.Eventos
                                        .Include(c => c.Lotes)
                                        .Include(c => c.RedesSociais);

            if(pIncludePalestrante){
                query = query
                        .Include(pe => pe.PalestranteEventos)
                        .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(c => c.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(string pTema, bool pIncludePalestrante)
        {
            IQueryable<Evento> query = _Context.Eventos
                                        .Include(c => c.Lotes)
                                        .Include(c => c.RedesSociais);

            if(pIncludePalestrante){
                query = query
                        .Include(pe => pe.PalestranteEventos)
                        .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(c => c.DataEvento)
                            .Where(c => c.Tema.ToLower().Contains(pTema.ToLower()));

            return await query.ToArrayAsync();
        }
        public async Task<Evento> GetEventoAsyncById(int pIdEvento, bool pIncludePalestrante)
        {
            IQueryable<Evento> query = _Context.Eventos
                                        .Include(c => c.Lotes)
                                        .Include(c => c.RedesSociais);

            if(pIncludePalestrante){
                query = query
                        .Include(pe => pe.PalestranteEventos)
                        .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(c => c.DataEvento)
                            .Where(c => c.Id == pIdEvento);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante> GetAllPalestranteAsync(int pIdPalestrante, bool pIncludeEventos = false)
        {
            IQueryable<Palestrante> query = _Context.Palestrantes                                        
                                        .Include(c => c.RedesSociais);

            if(pIncludeEventos){
                query = query
                        .Include(pe => pe.PalestranteEventos)
                        .ThenInclude(e => e.Evento);
            }

            query = query.OrderBy(p => p.Nome)
                            .Where(p => p.Id == pIdPalestrante);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestranteAsyncByName(string pNome, bool pIncludeEventos)
        {
             IQueryable<Palestrante> query = _Context.Palestrantes                                        
                                        .Include(c => c.RedesSociais);

            if(pIncludeEventos){
                query = query
                        .Include(pe => pe.PalestranteEventos)
                        .ThenInclude(e => e.Evento);
            }

            query = query.Where(p => p.Nome.ToLower().Contains(pNome.ToLower()));

            return await query.ToArrayAsync();
        }

        

       
    }
}