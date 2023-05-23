
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Core.Models
{
    
    public class ListagemAtividadesView
    {

        [GridColumn("Ref. Cliente")]
        public string RefCliente { get; set; }

        [GridColumn("Nome Cliente")]
        public string NomeCliente { get; set; }
        
        [GridColumn("Débito Cliente")]
        public decimal DebitoAntigo { get; set; }

         
        [GridColumn("Tipo Atividade")]
        public string DescricaoTipoAtividade { get; set; }

        [GridColumn("Data Criação Atividade")]
        public DateTime? DataCriacaoAtividade { get; set; }

        [GridColumn("Status Atividade")]
        public string StatusAtividade { get; set; }

        [GridColumn("Criador da Atividade")]
        public string UsuarioCriacaoAtividade { get; set; }

        [GridColumn("Usuario da Atividade")]
        public string UsuarioAlertaAtividade { get; set; }

        [GridColumn("Data do Alerta")]
        public DateTime? DataAlerta { get; set; }

        [GridColumn("Status do Alerta")]
        public string StatusAlerta { get; set; }

        [GridColumn("Obs do Alerta")]
        public string ObsAlerta { get; set; }

        [GridColumn("Ref Titulo")]
        public string NumTitulo { get; set; }

        [GridColumn("Parcela")]
        public string Parcela { get; set; }

        [GridColumn("Prefixo")]
        public string Prefixo { get; set; }

        [GridColumn("Nota Fiscal")]
        public string NotaFiscal { get; set; }

        [GridColumn("Data Emissão")]
        public DateTime? DataEmissao { get; set; }

        [GridColumn("Data Vencimento")]
        public DateTime? dataVencimento { get; set; }

        [GridColumn("Data DataPagamento")]
        public DateTime? DataPagamento { get; set; }

        [GridColumn("Valor")]
        public decimal ValorParcela { get; set; }

        [GridColumn("Desconto")]
        public decimal Desconto { get; set; }

        [GridColumn("Juros")]
        public decimal Juros { get; set; }

        [GridColumn("Valor Pago")]
        public decimal ValorPago { get; set; }

        [GridColumn("Saldo")]
        public decimal Saldo { get; set; }

        [GridColumn("Obs do Titulo")]
        public string ObsTitulo { get; set; }

        public int IdCliente { get; set; }





    }
}
