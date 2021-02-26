using System.Collections.Generic;

namespace DTO.Ferramentas
{
    public class Constantes
    {
        /// <summary>
        /// Negócio
        /// </summary>
        public const string NOME_APLICACAO = "Teste Invillia";

        /// <summary>
        /// Código hash para gerar o link de troca de senha
        /// </summary>
        public const string CODIGO_HASH_LINK_TROCAR_SENHA = "s$yjfH8q";

        /// <summary>
        /// Código hash para gerar o link de troca de senha
        /// </summary>
        public const string CODIGO_HASH_SIGNALR_EMPRESA = "dU&$h8H4";

        /// <summary>
        /// Senha inicial para quando criado um usuário novo
        /// </summary>
        public const string SENHA_INICIAL = "abcd1234";

        /// <summary>
        /// Email utilizado para camada de testes
        /// </summary>
        public static string EMAIL_TESTE = "email@email.com";


        #region MENSAGEM

        /// <summary>
        /// Tipo de alerta SUCESSO
        /// </summary>
        public const string TIPO_MENSAGEM_SUCESSO = "success";

        /// <summary>
        /// Tipo de alerta INFORMAÇÃO
        /// </summary>
        public const string TIPO_MENSAGEM_INFO = "info";

        /// <summary>
        /// Tipo de alerta ALERTA
        /// </summary>
        public const string TIPO_MENSAGEM_ALERTA = "warning";

        /// <summary>
        /// Tipo de alerta DANGER
        /// </summary>
        public const string TIPO_MENSAGEM_ERRO = "danger";

        #endregion

        #region ROLES

        /// <summary>
        /// Administrador do Sistema (todas Permissões)
        /// </summary>
        public const string ROLE_ADMINISTRADOR = "Administrador";

        /// <summary>
        /// Tela de Logs
        /// </summary>
        public const string ROLE_USUARIO = "Usuario";

        /// <summary>
        /// Tela de Produto
        /// </summary>
        public const string ROLE_JOGO = "Jogo";

        #endregion

        #region TEMPO

        /// <summary>
        /// Expirar Login
        /// </summary>
        public const int TEMPO_DIA_EXPIRAR_LOGIN = 7;

        #endregion

        #region FILTRO

        /// <summary>
        /// Todos
        /// </summary>
        public const int FILTRO_TODOS = -1;

        /// <summary>
        /// Selecione
        /// </summary>
        public const int FILTRO_SELECIONE = 0;

        /// <summary>
        /// Sim
        /// </summary>
        public const int FILTRO_SIM = 1;

        /// <summary>
        /// Não
        /// </summary>
        public const int FILTRO_NAO = 0;

        #endregion

        #region ICONES

        /// <summary>
        /// Envelope
        /// </summary>
        public const string ICONE_ENVELOPE = "fa fa-envelope-open";

        /// <summary>
        /// Lapis
        /// </summary>
        public const string ICONE_LAPIS = "fa fa-pencil-alt";

        /// <summary>
        /// Chave
        /// </summary>
        public const string ICONE_CHAVE = "fa fa-key";

        /// <summary>
        /// Editar
        /// </summary>
        public const string ICONE_EDITAR = "fa fa-edit";

        /// <summary>
        /// Excluir
        /// </summary>
        public const string ICONE_EXCLUIR = "fa fa-trash";

        /// <summary>
        /// Impressora
        /// </summary>
        public const string ICONE_IMPRESSORA = "fa fa-print";

        /// <summary>
        /// PDF
        /// </summary>
        public const string ICONE_PDF = "fa fa-file-pdf";

        /// <summary>
        /// Excel
        /// </summary>
        public const string ICONE_EXCEL = "fa fa-file-excel";

        /// <summary>
        /// Olho
        /// </summary>
        public const string ICONE_OLHO = "fa fa-eye";

        /// <summary>
        /// Arquivo Imagem
        /// </summary>
        public const string ICONE_ARQUIVO_IMAGEM = "fa fa-file-image";

        /// <summary>
        /// Lista
        /// </summary>
        public const string ICONE_LISTA = "fa fa-list";

        /// <summary>
        /// Telefone
        /// </summary>
        public const string ICONE_TELEFONE = "fa fa-phone-alt";

        /// <summary>
        /// Celular
        /// </summary>
        public const string ICONE_CELULAR = "fa fa-mobile-alt";

        /// <summary>
        /// + (MAIS)
        /// </summary>
        public const string ICONE_MAIS = "fa fa-plus";

        /// <summary>
        /// Ordenar
        /// </summary>
        public const string ICONE_ORDENAR = "fa fa-sort";

        /// <summary>
        /// Calendario
        /// </summary>
        public const string ICONE_CALENDARIO = "fa fa-calendar";

        /// <summary>
        /// Link
        /// </summary>
        public const string ICONE_LINK = "fa fa-link";

        /// <summary>
        /// Barcode
        /// </summary>
        public const string ICONE_BARCODE = "fa fa-barcode";

        /// <summary>
        /// Check Positivo
        /// </summary>
        public const string ICONE_CHECK_POSITIVO = "fa fa-check";

        /// <summary>
        /// Check Positivo
        /// </summary>
        public const string ICONE_CHECK_NEGATIVO = "fa fa-times";

        /// <summary>
        /// CPF
        /// </summary>
        public const string ICONE_CPF = "fa fa-id-card";

        /// <summary>
        /// Pessoa
        /// </summary>
        public const string ICONE_PESSOA = "fa fa-user-alt";


        /// <summary>
        /// Roles
        /// </summary>
        public const string ICONE_PESSOA_ROLES = "fa fa-user-tag";

        #endregion

        #region IMAGEM

        /// <summary>
        /// ./images/no-image.jpg
        /// </summary>
        public const string IMAGEM_SEM_IMAGEM = "./images/no-image.jpg";

        /// <summary>
        /// ./images/no-user.jpg
        /// </summary>
        public const string IMAGEM_SEM_USUARIO = "./images/no-user.jpg";

        /// <summary>
        /// Tamanho da imagem de Thumbnail
        /// </summary>
        public const int TAMANHO_IMAGEM_THUMBNAIL = 70;

        /// <summary>
        /// Tamanho máximo da imagem original
        /// </summary>
        public const int TAMANHO_IMAGEM_ORIGINAL = 480;

        #endregion

        #region PLATAFORMAS DE JOGOS

        /// <summary>
        /// Nomenclatura sem plataforma
        /// </summary>
        public const string SEM_PLATAFORMA = "Sem plataforma";

        /// <summary>
        /// ID do sem plataforma
        /// </summary>
        public const int SEM_PLATAFORMA_ID = 1;

        public static readonly List<Plataforma> PLATAFORMAS = new List<Plataforma> {
            new Plataforma(){ Id = SEM_PLATAFORMA_ID, Nome = SEM_PLATAFORMA },
            new Plataforma(){ Id = 2, Nome = "NES/Famicom" },
            new Plataforma(){ Id = 3, Nome = "MSX" },
            new Plataforma(){ Id = 4, Nome = "Casio PV-1000" },
            new Plataforma(){ Id = 5, Nome = "Atari 7800" },
            new Plataforma(){ Id = 6, Nome = "Action Max" },
            new Plataforma(){ Id = 7, Nome = "Master System" },
            new Plataforma(){ Id = 8, Nome = "Dynavision" },
            new Plataforma(){ Id = 9, Nome = "Game Boy" },
            new Plataforma(){ Id = 10, Nome = "Game Gear" },
            new Plataforma(){ Id = 11, Nome = "Commodore 64GS" },
            new Plataforma(){ Id = 12, Nome = "TurboGrafx-16" },
            new Plataforma(){ Id = 13, Nome = "Mega Drive/Genesis" },
            new Plataforma(){ Id = 14, Nome = "Atari Lynx" },
            new Plataforma(){ Id = 15, Nome = "TurboExpress" },
            new Plataforma(){ Id = 16, Nome = "Neo-Geo" },
            new Plataforma(){ Id = 17, Nome = "Super Nintendo/Super Famicom" },
            new Plataforma(){ Id = 18, Nome = "Commodore CDTV" },
            new Plataforma(){ Id = 19, Nome = "Sega CD" },
            new Plataforma(){ Id = 20, Nome = "CD-i" },
            new Plataforma(){ Id = 21, Nome = "Supervision" },
            new Plataforma(){ Id = 22, Nome = "Mega Duck" },
            new Plataforma(){ Id = 23, Nome = "Sega 32X" },
            new Plataforma(){ Id = 24, Nome = "Neo-Geo CD" },
            new Plataforma(){ Id = 25, Nome = "Super Game Boy" },
            new Plataforma(){ Id = 26, Nome = "Satellaview" },
            new Plataforma(){ Id = 27, Nome = "3DO" },
            new Plataforma(){ Id = 28, Nome = "Amiga CD32" },
            new Plataforma(){ Id = 29, Nome = "FM Towns Marty" },
            new Plataforma(){ Id = 30, Nome = "Pioneer LaserActive" },
            new Plataforma(){ Id = 31, Nome = "Atari Jaguar" },
            new Plataforma(){ Id = 32, Nome = "PC-FX" },
            new Plataforma(){ Id = 33, Nome = "Playdia" },
            new Plataforma(){ Id = 34, Nome = "Sega Saturn" },
            new Plataforma(){ Id = 35, Nome = "PlayStation" },
            new Plataforma(){ Id = 36, Nome = "Virtual Boy" },
            new Plataforma(){ Id = 37, Nome = "Casio Loopy" },
            new Plataforma(){ Id = 38, Nome = "R-Zone" },
            new Plataforma(){ Id = 39, Nome = "Atari Jaguar CD Pro" },
            new Plataforma(){ Id = 40, Nome = "Apple Pippin" },
            new Plataforma(){ Id = 41, Nome = "Nintendo 64" },
            new Plataforma(){ Id = 42, Nome = "Game.com" },
            new Plataforma(){ Id = 43, Nome = "Neo Geo Pocket" },
            new Plataforma(){ Id = 44, Nome = "Game Boy Color" },
            new Plataforma(){ Id = 45, Nome = "PocketStation" },
            new Plataforma(){ Id = 46, Nome = "Nintendo 64DD" },
            new Plataforma(){ Id = 47, Nome = "Dreamcast" },
            new Plataforma(){ Id = 48, Nome = "Neo Geo Pocket Color" },
            new Plataforma(){ Id = 49, Nome = "WonderSwan/WonderSwan Color" },
            new Plataforma(){ Id = 50, Nome = "PlayStation 2" },
            new Plataforma(){ Id = 51, Nome = "Pokémon mini" },
            new Plataforma(){ Id = 52, Nome = "Game Boy Advance" },
            new Plataforma(){ Id = 53, Nome = "GP32" },
            new Plataforma(){ Id = 54, Nome = "Xbox" },
            new Plataforma(){ Id = 55, Nome = "Nintendo GameCube" },
            new Plataforma(){ Id = 56, Nome = "SwanCrystal" },
            new Plataforma(){ Id = 57, Nome = "GameKing" },
            new Plataforma(){ Id = 58, Nome = "N-Gage" },
            new Plataforma(){ Id = 59, Nome = "PSX" },
            new Plataforma(){ Id = 60, Nome = "iQue Player" },
            new Plataforma(){ Id = 61, Nome = "Tapwave Zodiac" },
            new Plataforma(){ Id = 62, Nome = "Atari Flashback" },
            new Plataforma(){ Id = 63, Nome = "Nintendo DS" },
            new Plataforma(){ Id = 64, Nome = "PSP" },
            new Plataforma(){ Id = 65, Nome = "Gizmondo" },
            new Plataforma(){ Id = 66, Nome = "GP2X" },
            new Plataforma(){ Id = 67, Nome = "Xbox 360" },
            new Plataforma(){ Id = 68, Nome = "HyperScan" },
            new Plataforma(){ Id = 69, Nome = "PlayStation 3" },
            new Plataforma(){ Id = 70, Nome = "Wii" },
            new Plataforma(){ Id = 71, Nome = "Vii" },
            new Plataforma(){ Id = 72, Nome = "N-Gage 2.0" },
            new Plataforma(){ Id = 73, Nome = "Nintendo DSi" },
            new Plataforma(){ Id = 74, Nome = "GP2X Wiz" },
            new Plataforma(){ Id = 75, Nome = "Dingoo" },
            new Plataforma(){ Id = 76, Nome = "Zeebo" },
            new Plataforma(){ Id = 77, Nome = "OnLive" },
            new Plataforma(){ Id = 78, Nome = "Pandora" },
            new Plataforma(){ Id = 79, Nome = "Nintendo 3DS" },
            new Plataforma(){ Id = 80, Nome = "PlayStation Vita" },
            new Plataforma(){ Id = 81, Nome = "Nintendo Wii U" },
            new Plataforma(){ Id = 82, Nome = "Neo Geo X" },
            new Plataforma(){ Id = 83, Nome = "Ouya" },
            new Plataforma(){ Id = 84, Nome = "Shield Portable" },
            new Plataforma(){ Id = 85, Nome = "PlayStation 4" },
            new Plataforma(){ Id = 86, Nome = "Xbox One" },
            new Plataforma(){ Id = 87, Nome = "Nintendo Switch" },
            new Plataforma(){ Id = 88, Nome = "Google Stadia" },
            new Plataforma(){ Id = 89, Nome = "Xbox Series X/S" },
            new Plataforma(){ Id = 90, Nome = "PlayStation 5" },
            new Plataforma(){ Id = 91, Nome = "Atari VCS" },
            new Plataforma(){ Id = 92, Nome = "Intellivision Amico" },
            new Plataforma(){ Id = 93, Nome = "Mad Box" },
            new Plataforma(){ Id = 94, Nome = "Outros" }
        };

        #endregion
    }
}