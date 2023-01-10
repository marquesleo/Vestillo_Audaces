using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("OrdemProducao", "Ordem de Produção")]
    public class OrdemProducao
    {
        [Vestillo.Chave]
        public int Id { get; set; }
        public int AlmoxarifadoId { get; set; }
        public int? IdColaborador { get; set; }
        public int? IdColecao { get; set; }
        [Vestillo.FiltroEmpresa]
        public int EmpresaId { get; set; }
        [Vestillo.DataAtual]
        public DateTime DataEmissao { get; set; }
        [Vestillo.DataAtual]
        public DateTime DataEntrada { get; set; }
        [Vestillo.Contador("OrdemProducao")]
        [Vestillo.OrderByColumn]
        [RegistroUnico]
        public string Referencia { get; set; }
        public int Status { get; set; }
        public DateTime DataPrevisaoLiberacao { get; set; }
        public DateTime DataPrevisaoFinalizacao { get; set; }
        public DateTime? DataPrevisaoCorte { get; set; }
        public DateTime? Liberacao { get; set; }
        public DateTime? Corte { get; set; }
        public DateTime? Finalizacao { get; set; }
        public string Observacao { get; set; }
        public bool SomenteOperacoes { get; set; }
        public int Semana { get; set; }
        public string observacaomateriais { get; set; }
        [Vestillo.NaoMapeado]
        public List<ItemOrdemProducaoView> Itens { get; set; }

        [Vestillo.NaoMapeado]
        public List<OrdemProducaoMaterialView> ListaOrdemProducaoMaterial { get; set; }

        
    }
}
