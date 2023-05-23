using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Core.Models
{
    public class ClienteCarteiraView
    {
        public int Id { get; set; }
        
        [GridColumn("Razão Social")]
        public string RazaoSocial {get;set;}

        [GridColumn("Tipo Cliente")]
        public string TipoCliente { get; set; }

        [GridColumn("Vendedor 1")]
        public string Vendedor1 { get; set; }

        [GridColumn("E-Mail")]
        public string email { get; set; }

        [GridColumn("Vendedor 2")]
        public string Vendedor2 { get; set; }

        [GridColumn("Data Cadastro")]
        public DateTime DataCadastro { get; set; }

        [GridColumn("Rota")]
        public string Rota { get; set; }

        [GridColumn("UF")]
        public string UF { get; set; }

        [GridColumn("Cidade")]
        public string Cidade { get; set; }

        [GridColumn("Bairro")]
        public string Bairro { get; set; }

        [GridColumn("Primeira compra")]
        public DateTime? DataPrimeiraCompra { get; set; }

        [GridColumn("Última compra")]
        public DateTime? UltimaCompra { get; set; }        

        [GridColumn("Valor Últ. Compra")]
        public decimal? ValorUltimaCompra { get; set; }

        [GridColumn("Valor Acumulado Ano Anterior")]
        public decimal? ValorAcumuladoAnoPassado { get; set; }

        [GridColumn("Valor Acumulado Peças Ano Anterior")]
        public decimal? AcumuladoPecasAnoPassado { get; set; }
        
        
        
        [GridColumn("Valor Acumulado Ano Vigente")]
        public decimal? ValorAcumuladoAno { get; set; }

        [GridColumn("Valor Acumulado Peças Ano Vigente")]
        public decimal? AcumuladoPecasAno { get; set; }

        [GridColumn("Qtd Faturamentos")]
        public int QtdFaturamento { get; set; }

        [GridColumn("Limite de Compra")]
        public decimal LimiteCompra { get; set; }

        [GridColumn("Dt. Última atividade Inserida")]
        public DateTime? DataUltimaAtividadeInserida { get; set; }

        [GridColumn("Dt. Último Agendamento")]
        public DateTime? DataUltimoAgendamento { get; set; }
    }
}
