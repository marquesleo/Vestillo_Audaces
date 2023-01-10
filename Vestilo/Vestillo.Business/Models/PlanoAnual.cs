using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("PlanoAnual", "Plano Anual")]
    public class PlanoAnual
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int EmpresaId { get; set; }
        [Contador("PlanoAnual")]
        [RegistroUnico]
        public string Referencia { get; set; }
        public string Usuario { get; set; }
        public string Descricao { get; set; }
        [DataAtual]
        public DateTime Data { get; set; }
        public string Obs { get; set; }
        public int AnoBase { get; set; }
        public string Semana { get; set; }
        public int HoraBase { get; set; }

        [NaoMapeado]
        public List<PlanoAnualDetalhesView> planoDetalhes { get; set; }
    }
}
