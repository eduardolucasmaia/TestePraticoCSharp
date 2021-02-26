using AutoMapper;
using Dominio.Entities.DataModels;
using Dominio.Interfaces;
using Infra.Interfaces;
using DTO.DTO;
using DTO.Ferramentas;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio
{
    public class JogoDominio : IJogoDominio
    {
        #region Construtor

        private readonly IJogoInfra _jogoInfra;
        private readonly ILogErroDominio _logErroDominio;
        private readonly IMapper _mapper;

        public JogoDominio(IJogoInfra jogoInfra, ILogErroDominio logErroDominio, IMapper mapper)
        {
            _logErroDominio = logErroDominio;
            _jogoInfra = jogoInfra;
            _mapper = mapper;
        }

        #endregion

        #region CONSULTAS

        public async Task<List<JogoDTO>> BuscarJogosDisponiveis(int id)
        {
            try
            {
                var mapper = RemoverReferenciaCircularUsuario();
                return mapper.Map<List<JogoDTO>>(await _jogoInfra.BuscarJogosDisponiveis(id));
            }
            catch (Exception ex)
            {
                await _logErroDominio.SalvarLog(ex);
                throw;
            }
        }

        public async Task<List<JogoDTO>> BuscarJogos()
        {
            try
            {
                var mapper = RemoverReferenciaCircularUsuario();
                return mapper.Map<List<JogoDTO>>(await _jogoInfra.BuscarJogos());
            }
            catch (Exception ex)
            {
                await _logErroDominio.SalvarLog(ex);
                throw;
            }
        }

        public async Task<List<JogoDTO>> BuscarJogoPorNome(string nome)
        {
            try
            {
                nome = nome ?? string.Empty;
                var mapper = RemoverReferenciaCircularUsuario();
                return mapper.Map<List<JogoDTO>>(await _jogoInfra.BuscarJogoPorNome(nome.Trim()));
            }
            catch (Exception ex)
            {
                await _logErroDominio.SalvarLog(ex);
                throw;
            }
        }

        public async Task<JogoDTO> BuscarJogoPorId(int id)
        {
            try
            {
                return _mapper.Map<JogoDTO>(await _jogoInfra.BuscarJogoPorId(id));
            }
            catch (Exception ex)
            {
                await _logErroDominio.SalvarLog(ex);
                throw;
            }
        }

        private IMapper RemoverReferenciaCircularUsuario()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Jogo, JogoDTO>()
                .ForMember(x => x.FotoBase64, opt => opt.Ignore());

                cfg.CreateMap<Usuario, UsuarioDTO>()
                .ForMember(x => x.Jogo, opt => opt.Ignore())
                .ForMember(x => x.FotoBase64, opt => opt.Ignore())
                .ForMember(x => x.MiniaturaBase64, opt => opt.Ignore())
                .ForMember(x => x.Senha, opt => opt.Ignore())
                .ForMember(x => x.InverseIdCriadoUsuarioNavigation, opt => opt.Ignore());
            });
            return config.CreateMapper();
        }

        #endregion

        #region AÇÕES

        public async Task<bool> SalvarJogo(JogoDTO jogo)
        {
            try
            {
                var jogoRetorno = await _jogoInfra.BuscarJogoPorId(jogo.Id);

                if (jogoRetorno != null)
                {
                    jogoRetorno.DataAlteracao = DateTime.Now;
                    jogoRetorno.FotoBase64 = string.IsNullOrEmpty(jogo.FotoBase64) ? null : Imagem.RedimencionarImagem(Constantes.TAMANHO_IMAGEM_ORIGINAL, jogo.FotoBase64);
                    jogoRetorno.MiniaturaBase64 = string.IsNullOrEmpty(jogo.FotoBase64) ? null : Imagem.RedimencionarImagem(Constantes.TAMANHO_IMAGEM_THUMBNAIL, jogo.FotoBase64);
                    jogoRetorno.Excluido = false;
                    jogoRetorno.Descricao = jogo.Descricao?.Trim();
                    jogoRetorno.Nome = jogo.Nome.Trim();
                    jogoRetorno.Observacao = jogo.Observacao?.Trim();
                    jogoRetorno.IdUsuario = jogo.IdUsuario == null ? null : (jogo.IdUsuario.Value == 0 ? null : jogo.IdUsuario);
                    jogoRetorno.Plataforma = jogo.Plataforma > Constantes.PLATAFORMAS.Count ? Constantes.SEM_PLATAFORMA_ID : jogo.Plataforma;
                }
                else
                {
                    jogoRetorno = new Jogo()
                    {
                        Id = 0,
                        DataCadastro = DateTime.Now,
                        DataAlteracao = new Nullable<DateTime>(),
                        FotoBase64 = string.IsNullOrEmpty(jogo.FotoBase64) ? null : Imagem.RedimencionarImagem(Constantes.TAMANHO_IMAGEM_ORIGINAL, jogo.FotoBase64),
                        MiniaturaBase64 = string.IsNullOrEmpty(jogo.FotoBase64) ? null : Imagem.RedimencionarImagem(Constantes.TAMANHO_IMAGEM_THUMBNAIL, jogo.FotoBase64),
                        Excluido = false,
                        Descricao = jogo.Descricao?.Trim(),
                        Nome = jogo.Nome.Trim(),
                        Observacao = jogo.Observacao?.Trim(),
                        IdUsuario = jogo.IdUsuario == null ? null : (jogo.IdUsuario.Value == 0 ? null : jogo.IdUsuario),
                        Plataforma = jogo.Plataforma > Constantes.PLATAFORMAS.Count ? Constantes.SEM_PLATAFORMA_ID : jogo.Plataforma
                    };
                }
                return await _jogoInfra.SalvarJogo(jogoRetorno);
            }
            catch (Exception ex)
            {
                await _logErroDominio.SalvarLog(ex);
                throw;
            }
        }

        public async Task<bool> ExcluirJogo(int id)
        {
            try
            {
                return await _jogoInfra.ExcluirJogo(id);
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