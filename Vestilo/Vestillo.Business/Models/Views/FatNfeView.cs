using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class FatNfeView
    {
       
        public int Id { get; set; }        
        public int IdEmpresa { get; set; }
        public int Tipo { get; set; }        
        public string Referencia { get; set; }
        public string refpedidocliente { get; set; }        
        public string DescricaoTipo { get; set; }
        public int IdColaborador { get; set; }
        public string RefColaborador { get; set; }
        public string NomeColaborador { get; set; }
        public string RazaoColaborador { get; set; }
        public string UfColaborador { get; set; }
        public string refvendedor { get; set; }
        public string NomeVendedor { get; set; }
        public string refvendedor2 { get; set; }
        public string nomevendedor2 { get; set; }
        public string RefTabela { get; set; }
        public string DescTabela { get; set; }
        public string reftransportadora { get; set; }
        public string nometransportadora { get; set; }
        public int IdPedido { get; set; }
        public string RefPedido { get; set; }
        public int Serie { get; set; }
        public string Numero { get; set; }
        public DateTime? DataInclusao { get; set; } 
        public DateTime? DataEmissao { get; set; }
        public DateTime? DataSaida { get; set; }
        public DateTime? DataPrograma { get; set; }
        public DateTime? Horaemissao { get; set; }
        public decimal Frete { get; set; }
        public decimal Seguro { get; set; }
        public decimal Despesa { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValDesconto { get; set; }
        public int TipoFrete { get; set; }
        public string Placaveiculo { get; set; }
        public string Qtdvolumes { get; set; }
        public string Especie { get; set; }
        public string Marca { get; set; }
        public decimal Pesobruto { get; set; }
        public decimal Pesoliquido { get; set; }
        public string Observacao { get; set; }
        public int StatusNota { get; set; }
        public string DescStatusNota { get; set; }
        public string Xmlassinado { get; set; }
        public string Idnotaassinada { get; set; }
        public string Recibonota { get; set; }
        public string Datarecibo { get; set; }
        public string Statusxml { get; set; }
        public string Recebidasefaz { get; set; }
        public string DescRecebidasefaz { get; set; } 
        public string Protocolosefaz { get; set; }
        public decimal Descontoitem { get; set; }
        public decimal Baseicms { get; set; }
        public decimal Valoricms { get; set; }
        public decimal Totalnota { get; set; }
        public decimal TotalSemFrete { get; set; }
        public decimal Valortribestado { get; set; }
        public decimal Valorpartemissor { get; set; }
        public decimal Valorpartdest { get; set; }
        public decimal Valorfcp { get; set; }
        public decimal basefcpestado { get; set; } // Novo 4.0
        public decimal Valorfcpestado { get; set; } // Novo 4.0
        public decimal ValorDinheiro { get; set; }
        public decimal ValorCartaoCredito { get; set; }
        public decimal ValorCartaoDebito { get; set; }
        public decimal ValorCheque { get; set; }
        public string IdsNfe { get; set; }
        public int TotalmenteDevolvida { get; set; }  //0=> não 1=> sim        
        public int PossuiDevolucao { get; set; }  //0=> não 1=> sim
        public int? IdAntigo { get; set; }
        public int ValidadoParaEmissao { get; set; }
        public string NumPedido { get; set; }
        public decimal ComissaoVendedor { get; set; }
        public decimal ComissaoVendedor2 { get; set; }
        public string IdsCreditos { get; set; }
        public int Denegada { get; set; }
        public string DescDenegada { get; set; }
        public decimal totalitens { get; set; }
        public decimal QtddevolvidaTotal { get; set; }
        public decimal ValorIpi { get; set; }
        public int EmpresaTrocada { get; set; }
        public string DescOutraEmpresa { get; set; }
        public string NomeEmpresaTrocada { get; set; }
    }
}
