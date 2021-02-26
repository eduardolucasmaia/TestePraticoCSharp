using AutoMapper;
using Dominio.Entities.DataModels;
using Dominio.Interfaces;
using Infra.Interfaces;
using DTO.DTO;
using DTO.Ferramentas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio
{
    public class UsuarioDominio : IUsuarioDominio
    {
        #region Construtor

        private readonly IUsuarioInfra _usuarioInfra;
        private readonly ILogErroDominio _logErroDominio;
        private readonly IJogoInfra _jogoInfra;
        private readonly IMapper _mapper;
        public UsuarioDominio(IUsuarioInfra usuarioInfra, IJogoInfra jogoInfra, ILogErroDominio logErroDominio, IMapper mapper)
        {
            _logErroDominio = logErroDominio;
            _jogoInfra = jogoInfra;
            _usuarioInfra = usuarioInfra;
            _mapper = mapper;
        }

        #endregion

        #region CONSULTA

        public async Task<UsuarioDTO> AutenticarUsuario(string nomeUsuario, string senha)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Role, RoleDTO>()
                    .ForMember(x => x.VinculoUsuarioRole, opt => opt.Ignore());
                    cfg.CreateMap<Usuario, UsuarioDTO>()
                    .ForMember(x => x.InverseIdCriadoUsuarioNavigation, opt => opt.Ignore());
                    cfg.CreateMap<VinculoUsuarioRole, VinculoUsuarioRoleDTO>()
                    .ForMember(x => x.IdUsuarioNavigation, opt => opt.Ignore());
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<UsuarioDTO>(await _usuarioInfra.AutenticarUsuario(nomeUsuario, senha));
            }
            catch (Exception ex)
            {
                await _logErroDominio.SalvarLog(ex);
                throw;
            }
        }

        public async Task<List<UsuarioDTO>> BuscarUsuarios(int id)
        {
            try
            {
                var mapper = RemoverReferenciaCircularJogo();
                return mapper.Map<List<UsuarioDTO>>(await _usuarioInfra.BuscarUsuarios(id));
            }
            catch (Exception ex)
            {
                await _logErroDominio.SalvarLog(ex);
                throw;
            }
        }

        public async Task<UsuarioDTO> BuscarUsuarioPorId(int id)
        {
            try
            {
                var mapper = RemoverReferenciaCircularJogo();
                return mapper.Map<UsuarioDTO>(await _usuarioInfra.BuscarUsuarioPorId(id));
            }
            catch (Exception ex)
            {
                await _logErroDominio.SalvarLog(ex);
                throw;
            }
        }

        public async Task<UsuarioDTO> BuscarUsuarioPorEmail(string email)
        {
            try
            {
                return _mapper.Map<UsuarioDTO>(await _usuarioInfra.BuscarUsuarioPorEmail(email));
            }
            catch (Exception ex)
            {
                await _logErroDominio.SalvarLog(ex);
                throw;
            }
        }

        public async Task<string> BuscarFotoPerfilUsuarioAtual(int id)
        {
            try
            {
                return await _usuarioInfra.BuscarFotoPerfilUsuarioAtual(id);
            }
            catch (Exception ex)
            {
                await _logErroDominio.SalvarLog(ex);
                throw;
            }
        }

        public async Task<UsuarioDTO> BuscarUsuarioAtual(int id)
        {
            try
            {
                return _mapper.Map<UsuarioDTO>(await _usuarioInfra.BuscarUsuarioPorId(id));
            }
            catch (Exception ex)
            {
                await _logErroDominio.SalvarLog(ex);
                throw;
            }
        }

        private IMapper RemoverReferenciaCircularJogo()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Jogo, JogoDTO>()
                .ForMember(x => x.FotoBase64, opt => opt.Ignore())
                .ForMember(x => x.MiniaturaBase64, opt => opt.Ignore())
                .ForMember(x => x.IdUsuarioNavigation, opt => opt.Ignore());
                cfg.CreateMap<Usuario, UsuarioDTO>()
                .ForMember(x => x.Senha, opt => opt.Ignore())
                .ForMember(x => x.InverseIdCriadoUsuarioNavigation, opt => opt.Ignore());
                cfg.CreateMap<Role, RoleDTO>()
                .ForMember(x => x.VinculoUsuarioRole, opt => opt.Ignore());
                cfg.CreateMap<VinculoUsuarioRole, VinculoUsuarioRoleDTO>()
                .ForMember(x => x.IdUsuarioNavigation, opt => opt.Ignore());
            });
            return config.CreateMapper();
        }

        #endregion

        #region AÇÕES

        public async Task<bool> SalvarUsuario(UsuarioDTO usuario)
        {
            try
            {
                var usuarioRetorno = await _usuarioInfra.BuscarUsuarioPorId(usuario.Id);

                List<Jogo> listaJogos = null;

                if (usuario.JogosSelecionados != null && usuario.JogosSelecionados.Count > 0)
                {
                    listaJogos = await _jogoInfra.BuscarJogosIn(usuario.JogosSelecionados, usuario.Id);
                }

                if (usuarioRetorno != null)
                {
                    usuarioRetorno.DataAlteracao = DateTime.Now;
                    usuarioRetorno.Excluido = false;
                    usuarioRetorno.Telefone = usuario.Telefone?.Trim();
                    usuarioRetorno.Nome = usuario.Nome.Trim();
                    usuarioRetorno.Cpf = usuario.Cpf?.Trim();
                    usuarioRetorno.Email = usuario.Email.Trim();
                    usuarioRetorno.Observacao = usuario.Observacao?.Trim();
                    usuarioRetorno.FotoBase64 = string.IsNullOrEmpty(usuario.FotoBase64) ? null : Imagem.RedimencionarImagem(Constantes.TAMANHO_IMAGEM_ORIGINAL, usuario.FotoBase64);
                    usuarioRetorno.MiniaturaBase64 = string.IsNullOrEmpty(usuario.FotoBase64) ? null : Imagem.RedimencionarImagem(Constantes.TAMANHO_IMAGEM_THUMBNAIL, usuario.FotoBase64);

                    //Usuário Possui jogo
                    if (usuarioRetorno.Jogo != null && usuarioRetorno.Jogo.Count > 0)
                    {
                        //Limpa a lista de jogos dele
                        usuarioRetorno.Jogo.ToList().ForEach(c => { c.IdUsuario = null; c.IdUsuarioNavigation = null; });

                        //Lista veio preechida
                        if (listaJogos != null && listaJogos.Count > 0)
                        {
                            //Passa todas para o id do usuário
                            listaJogos.ForEach(c => c.IdUsuario = usuarioRetorno.Id);

                            //Percorre a iista
                            foreach (var loop in listaJogos)
                            {
                                //verifica se não está na lista
                                if (!usuarioRetorno.Jogo.ToList().Exists(x => x.Id == loop.Id))
                                {
                                    //Se não estiver adiciona a coleção
                                    usuarioRetorno.Jogo.Add(loop);
                                }
                                else
                                {
                                    //Se estiver na lista devolve para o id dele.
                                    usuarioRetorno.Jogo.Where(x => x.Id == loop.Id).Select(x => { x.IdUsuario = usuario.Id; return x; }).ToList();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (listaJogos != null && listaJogos.Count > 0)
                        {
                            listaJogos.ForEach(c => c.IdUsuario = usuarioRetorno.Id);
                            usuarioRetorno.Jogo = listaJogos;
                        }
                    }
                }
                else
                {
                    usuarioRetorno = new Usuario()
                    {
                        Id = 0,
                        DataCadastro = DateTime.Now,
                        DataAlteracao = new Nullable<DateTime>(),
                        Excluido = false,
                        IdCriadoUsuario = usuario.IdCriadoUsuario,
                        Telefone = usuario.Telefone?.Trim(),
                        Nome = usuario.Nome.Trim(),
                        Cpf = usuario.Cpf?.Trim(),
                        Email = usuario.Email.Trim(),
                        Observacao = usuario.Observacao?.Trim(),
                        Senha = Constantes.SENHA_INICIAL,
                        FotoBase64 = string.IsNullOrEmpty(usuario.FotoBase64) ? null : Imagem.RedimencionarImagem(Constantes.TAMANHO_IMAGEM_ORIGINAL, usuario.FotoBase64),
                        MiniaturaBase64 = string.IsNullOrEmpty(usuario.FotoBase64) ? null : Imagem.RedimencionarImagem(Constantes.TAMANHO_IMAGEM_THUMBNAIL, usuario.FotoBase64),
                        Jogo = listaJogos
                    };
                }

                return await _usuarioInfra.SalvarUsuario(usuarioRetorno);
            }
            catch (Exception ex)
            {
                await _logErroDominio.SalvarLog(ex);
                throw;
            }
        }

        public async Task<bool> AtualizarPerfil(UsuarioDTO model, int id)
        {
            try
            {
                var usuario = await _usuarioInfra.BuscarUsuarioPorId(id);
                if (usuario != null)
                {
                    usuario.Nome = string.IsNullOrEmpty(model.Nome) ? null : model.Nome.Trim();
                    usuario.Email = string.IsNullOrEmpty(model.Email) ? null : model.Email.Trim();
                    usuario.Cpf = string.IsNullOrEmpty(model.Cpf) ? null : model.Cpf.Trim();
                    usuario.Telefone = string.IsNullOrEmpty(model.Telefone) ? null : model.Telefone.Trim();
                    usuario.Observacao = string.IsNullOrEmpty(model.Observacao) ? null : model.Observacao.Trim();
                    usuario.FotoBase64 = string.IsNullOrEmpty(model.FotoBase64) ? null : Imagem.RedimencionarImagem(Constantes.TAMANHO_IMAGEM_ORIGINAL, model.FotoBase64);
                    usuario.MiniaturaBase64 = string.IsNullOrEmpty(model.FotoBase64) ? null : Imagem.RedimencionarImagem(Constantes.TAMANHO_IMAGEM_THUMBNAIL, model.FotoBase64);
                    usuario.DataAlteracao = DateTime.Now;
                    usuario.Excluido = false;

                    return await _usuarioInfra.SalvarUsuario(usuario);
                }
                return true;
            }
            catch (Exception ex)
            {
                await _logErroDominio.SalvarLog(ex);
                throw;
            }
        }

        public async Task<bool> ExcluirUsuario(int id)
        {
            try
            {
                return await _usuarioInfra.ExcluirUsuario(id);
            }
            catch (Exception ex)
            {
                await _logErroDominio.SalvarLog(ex);
                throw;
            }
        }

        public async Task<UsuarioDTO> TrocarSenha(string senhaAntiga, string novaSenha, int id)
        {
            try
            {
                return _mapper.Map<UsuarioDTO>(await _usuarioInfra.TrocarSenha(senhaAntiga, novaSenha, id));
            }
            catch (Exception ex)
            {
                await _logErroDominio.SalvarLog(ex);
                throw;
            }
        }

        #endregion
    }
}
