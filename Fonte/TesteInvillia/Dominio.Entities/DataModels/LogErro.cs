using System;
using System.Collections.Generic;

namespace Dominio.Entities.DataModels
{
    public partial class LogErro
    {
        public int Id { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string InnerException { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
