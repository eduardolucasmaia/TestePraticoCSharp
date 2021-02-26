using Dominio.Entities.DataModels;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra
{
    public class UsuarioInfra : IUsuarioInfra
    {
        #region CONSULTAS

        public async Task<Usuario> AutenticarUsuario(string nomeUsuario, string senha)
        {
            using (var db = new TesteInvilliaContext())
            {
                return await db.Usuario
                    .Where(x => (x.Telefone.Equals(nomeUsuario) || x.Email.Equals(nomeUsuario)) && x.Senha.Equals(senha) && !x.Excluido)
                    .Include(a => a.VinculoUsuarioRole)
                    .ThenInclude(x => x.IdRoleNavigation)
                    .Select(x => new Usuario()
                    {
                        Id = x.Id,
                        Nome = x.Nome,
                        Email = x.Email,
                        VinculoUsuarioRole = x.VinculoUsuarioRole
                        .Where(y => y.IdUsuario == x.Id && !y.Excluido)
                        .Select(y => new VinculoUsuarioRole()
                        {
                            IdRoleNavigation = y.IdRoleNavigation
                        }).ToList()
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<Usuario> BuscarUsuarioPorId(int id)
        {
            using (var db = new TesteInvilliaContext())
            {
                return await db.Usuario
                    .Where(x => x.Id == id && !x.Excluido)
                    .Include(a => a.Jogo)
                    .Include(a => a.VinculoUsuarioRole)
                    .ThenInclude(x => x.IdRoleNavigation)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<Usuario> BuscarUsuarioPorEmail(string email)
        {
            using (var db = new TesteInvilliaContext())
            {
                return await db.Usuario
                    .Where(x => x.Email.Equals(email) && !x.Excluido)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<List<Usuario>> BuscarUsuarios(int id)
        {
            using (var db = new TesteInvilliaContext())
            {
                return await db.Usuario
                    .Where(x => x.Id != id && !x.Excluido)
                    .Include(x => x.Jogo)
                    .Include(x => x.VinculoUsuarioRole)
                    .ThenInclude(x => x.IdRoleNavigation)
                    .AsNoTracking()
                    .ToListAsync();
            }
        }

        public async Task<string> BuscarFotoPerfilUsuarioAtual(int id)
        {
            using (var db = new TesteInvilliaContext())
            {
                return await db.Usuario
                    .Where(x => x.Id == id && !x.Excluido)
                    .Select(x => x.FotoBase64)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
            }
        }

        #endregion

        #region AÇÕES

        public async Task<bool> SalvarUsuario(Usuario usuario)
        {
            using (var db = new TesteInvilliaContext())
            {
                using (var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (usuario.Id != 0)
                        {
                            var jogos = usuario.Jogo;
                            usuario.Jogo = null;

                            db.Usuario.Update(usuario);
                            await db.SaveChangesAsync();

                            if (jogos != null && jogos.Count > 0)
                            {
                                db.Jogo.UpdateRange(jogos);
                                await db.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            var jogos = usuario.Jogo;
                            usuario.Jogo = null;

                            await db.Usuario.AddAsync(usuario);
                            await db.SaveChangesAsync();

                            if (jogos != null && jogos.Count > 0)
                            {
                                jogos.ToList().ForEach(c => c.IdUsuario = usuario.Id);
                                db.Jogo.UpdateRange(jogos);
                                await db.SaveChangesAsync();
                            }
                        }

                        dbTransaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        dbTransaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task<bool> ExcluirUsuario(int id)
        {
            using (var db = new TesteInvilliaContext())
            {
                using (var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var model = await db.Usuario.Where(x => x.Id == id && !x.Excluido).Include(x => x.Jogo).AsNoTracking().FirstOrDefaultAsync();
                        if (model != null)
                        {
                            if (model.Jogo != null && model.Jogo.Count > 0)
                            {
                                model.Jogo.ToList().ForEach(c => { c.IdUsuario = null; c.IdUsuarioNavigation = null; });

                                db.Jogo.UpdateRange(model.Jogo);
                                await db.SaveChangesAsync();
                            }

                            model.Jogo = null;
                            model.Excluido = true;
                            model.DataAlteracao = DateTime.Now;

                            db.Usuario.Update(model);
                            await db.SaveChangesAsync();

                            dbTransaction.Commit();
                        }
                        return true;
                    }
                    catch (Exception)
                    {
                        dbTransaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task<Usuario> TrocarSenha(string senhaAntiga, string novaSenha, int id)
        {
            using (var db = new TesteInvilliaContext())
            {
                var model = await db.Usuario.Where(x => x.Id == id && x.Senha.Equals(senhaAntiga.Trim()) && !x.Excluido).FirstOrDefaultAsync();
                if (model != null)
                {
                    model.Senha = novaSenha.Trim();
                    model.DataAlteracao = DateTime.Now;
                    db.Usuario.Update(model);
                    await db.SaveChangesAsync();
                }
                return model;
            }
        }

        #endregion

    }
}