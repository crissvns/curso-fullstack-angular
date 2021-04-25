using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext _context;

        public ProAgilRepository(ProAgilContext context) => this._context = context;

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        #region  Evento
        
        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(c => c.Lotes).Include(c => c.RedesSociais);

            if (includePalestrantes)
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(p => p.Palestrante);

            return await query.AsNoTracking().OrderByDescending(c => c.DataEvento).ToArrayAsync();
        }

        public async Task<Evento> GetAllEventoAsyncById(int EventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(c => c.Lotes).Include(c => c.RedesSociais);

            if (includePalestrantes)
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(p => p.Palestrante);

            return await query.OrderByDescending(c => c.DataEvento).AsNoTracking().Where(c => c.Id.Equals(EventoId)).FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(string Tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(c => c.Lotes).Include(c => c.RedesSociais);

            if (includePalestrantes)
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(p => p.Palestrante);

            return await query.OrderByDescending(c => c.DataEvento).AsNoTracking().Where(c => c.Tema.ToLower().Contains(Tema.ToLower())).ToArrayAsync();
        }

        #endregion

        #region Palestrante

        public async Task<Palestrante> GetAllPalestranteAsync(int PalestranteId,bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(c => c.RedesSociais);

            if (includeEventos)
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(p => p.Evento);

            return await query.OrderBy(c => c.Nome).AsNoTracking().Where(c => c.Id.Equals(PalestranteId)).FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestranteAsyncByName(string Nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(c => c.RedesSociais);

            if (includeEventos)
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(p => p.Evento);

            return await query.OrderBy(c => c.Nome).AsNoTracking().Where(c => c.Nome.ToLower().Contains(Nome.ToLower())).ToArrayAsync();
        }

        #endregion
    }
}