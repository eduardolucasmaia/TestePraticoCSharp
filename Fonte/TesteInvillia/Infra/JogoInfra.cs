using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities.DataModels;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra
{
    public class JogoInfra : IJogoInfra
    {
        #region CONSULTAS

        public async Task<List<Jogo>> BuscarJogosDisponiveis(int id)
        {
            using (var db = new TesteInvilliaContext())
            {
                return await db.Jogo.Where(x => (x.IdUsuario == null || x.IdUsuario == id) && !x.Excluido)
                    .Include(x => x.IdUsuarioNavigation)
                    .AsNoTracking()
                    .ToListAsync();
            }
        }

        public async Task<List<Jogo>> BuscarJogos()
        {
            using (var db = new TesteInvilliaContext())
            {
                return await db.Jogo.Where(x => !x.Excluido)
                    .Include(x => x.IdUsuarioNavigation)
                    .AsNoTracking()
                    .ToListAsync();
            }
        }

        public async Task<List<Jogo>> BuscarJogosIn(List<int> ids, int id)
        {
            using (var db = new TesteInvilliaContext())
            {
                return await db.Jogo.Where(x => (x.IdUsuario == null || x.IdUsuario == id) && ids.Contains(x.Id) && !x.Excluido)
                    .AsNoTracking()
                    .ToListAsync();
            }
        }

        public async Task<List<Jogo>> BuscarJogoPorNome(string nome)
        {
            using (var db = new TesteInvilliaContext())
            {
                return await db.Jogo
                    .Where(x => x.Nome.Contains(nome) && !x.Excluido)
                    .Include(x => x.IdUsuarioNavigation)
                    .AsNoTracking()
                    .ToListAsync();
            }
        }

        public async Task<Jogo> BuscarJogoPorId(int id)
        {
            using (var db = new TesteInvilliaContext())
            {
                return await db.Jogo
                    .Where(x => x.Id == id && !x.Excluido)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
            }
        }

        #endregion

        #region AÇÕES

        public async Task<bool> SalvarJogo(Jogo jogo)
        {
            using (var db = new TesteInvilliaContext())
            {
                if (jogo.Id != 0)
                {
                    db.Jogo.Update(jogo);
                }
                else
                {
                    await db.Jogo.AddAsync(jogo);
                }
                await db.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> ExcluirJogo(int id)
        {
            using (var db = new TesteInvilliaContext())
            {
                var model = await db.Jogo.Where(x => x.Id == id && !x.Excluido).FirstOrDefaultAsync();
                if (model != null)
                {
                    model.Excluido = true;
                    model.DataAlteracao = DateTime.Now;
                    model.IdUsuario = null;
                    db.Jogo.Update(model);
                    await db.SaveChangesAsync();
                }
                return true;
            }
        }

        #endregion
    }
}
