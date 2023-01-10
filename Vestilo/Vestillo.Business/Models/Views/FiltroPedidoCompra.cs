using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FiltroPedidoCompra
    {
        public List<int> Pedidos { get; set; }
        public List<int> Produtos { get; set; }
        public List<int> Fornecedores { get; set; }

        public string DaEmissao { get; set; }
        public string AteEmissao { get; set; }
        public string DaPrev { get; set; }
        public string AtePrev { get; set; }
        public string Ordenar { get; set; }
        public string Agrupar { get; set; }
        public string Contato { get; set; }

        public int Exibir { get; set; }
        public int Relatorio { get; set; }

        public bool NaoAtendido { get; set; }
        public bool AtendidoParcial { get; set; }
        public bool Atendido { get; set; }
        public bool Finalizado { get; set; }

        public bool DadosEmpresa { get; set; }
        public bool Assinatura { get; set; }
        public bool AgruparItem { get; set; }
    }
}
