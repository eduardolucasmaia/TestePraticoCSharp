using System;
using System.Runtime.Serialization;

namespace DTO.Ferramentas
{
    [DataContract]
    [Serializable]
    public class RetornoGenerico<T>
    {
        /// <summary>
        /// Mensagem a exibir
        /// </summary>
        [DataMember]
        public string Mensagem { get; set; }

        ///// <summary>
        ///// Tipo Mensagem
        ///// {1 = Success} {2 = Info} {3 = Warning} {4 = Error} {5 = Danger}
        ///// </summary>
        [DataMember]
        public string TipoMensagem { get; set; }

        /// <summary>
        /// Para comparar se ocorreu algum erro
        /// </summary>
        [DataMember]
        public bool Sucesso { get; set; }

        /// <summary>
        /// Retorno do tipo Generico
        /// </summary>
        [DataMember]
        public T Retorno { get; set; }

        /// <summary>
        /// Exception ocorrida
        /// </summary>
        [DataMember]
        public Exception Exception { get; set; }
    }
}
