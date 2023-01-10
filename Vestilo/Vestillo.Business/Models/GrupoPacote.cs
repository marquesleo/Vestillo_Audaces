using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("GrupoPacote", "Grupo de Pacote de Produção")]
    public class GrupoPacote
    {
        [Vestillo.Chave]
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        [Vestillo.FiltroEmpresa]
        public int EmpresaId { get; set; }
        [Vestillo.DataAtual]
        public DateTime Data { get; set; }
        public String Usuario { get; set; }

        [Vestillo.NaoMapeado]
        public List<PacoteProducao> Pacotes { get; set; }
    }
}
