using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("PlanoContas", "Plano de Contas")]
    public class PlanoContas 
    {
        public PlanoContas()
        {
            Ativo = true;
        }

        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int EmpresaId { get; set; }
        [OrderByColumn]
        [RegistroUnico]
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int? PlanoContasPaiId { get; set; }
        public bool Ativo { get; set; }
        public bool Padrao { get; set; }
        [NaoMapeado]
        public List<NaturezaFinanceira> Naturezas { get; set; }
    }
}
