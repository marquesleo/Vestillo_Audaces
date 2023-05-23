using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Core.Models
{
    public class AcompanhamentoCatalogoView
    {

        public bool ExibirCotacao { get; set; }

        public decimal PercentualEstoqueIdeal { get; set; }

        [GridColumn("Entrega")]
        public string Entrega { get; set; }

        [GridColumn("Coleção")]
        public string Colecao { get; set; }

        [GridColumn("Ref")]
        public string Referencia { get; set; }

        [GridColumn("Cor")]
        public string Cor { get; set; }

        [GridColumn("Tamanho")]
        public string Tamanho { get; set; }

        [GridColumn("Preco")]
        public decimal Preco { get; set; }

        [GridColumn("Plano de Produção")]
        public decimal PlanoProducao { get; set; }

        [GridColumn("Empenho")]
        public decimal Empenho { get; set; }

        [GridColumn("Fabricado")]
        public decimal Fabricado { get; set; }

        //[GridColumn("Entrada de Troca")]
        //public decimal? EntradaDeTroca { get; set; }

        [GridColumn("Devolução")]
        public decimal Devolucao { get; set; }

        [GridColumn("Mostruários")]
        public decimal Mostruarios { get; set; }

        [GridColumn("Remessa")]
        public decimal Remessa { get; set; }

        [GridColumn("Marketing")]
        public decimal Marketing { get; set; }

        [GridColumn("Total Saída sem Vendas")]
        public decimal TotalSaidaSemVendas { get; set; }

        [GridColumn("Nota Fiscal de Saída")]
        public decimal NotaFiscalSaida { get; set; }

        [GridColumn("Especial")]
        public decimal Especial { get; set; }

        [GridColumn("DCI")]
        public decimal DCI { get; set; }

        [GridColumn("Cabo Frio")]
        public decimal CaboFrio { get; set; }

        [GridColumn("Amo Muito")]
        public decimal AmoMuito { get; set; }

        [GridColumn("Saída para Troca")]
        public decimal SaidaParaTroca { get; set; }

        [GridColumn("Total Saída Vendas")]
        public decimal TotalSaidaVendas 
        { 
                get
                    {
                        decimal valor = NotaFiscalSaida + Especial + DCI + CaboFrio
                            + AmoMuito + SaidaParaTroca;

                        if (valor > 0)
                            return valor;

                        return 0;
                    }
        }

        [GridColumn("Faturado")]
        public decimal Faturado { get; set; }

        [GridColumn("Cotação")]
        public decimal Cotacao { get; set; }

        [GridColumn("Pedido")]
        public decimal Pedido { get; set; }
        
        [GridColumn("Desejo Cliente")]
        public decimal DesejoCliente 
        {
            get
            {
                decimal valor = TotalSaidaVendas + Pedido;
                if (ExibirCotacao)
                    valor += Cotacao;

                if (valor > 0)
                    return valor;

                return 0;
            }
        }

        [GridColumn("Saldo Atual")]
        public decimal SaldoAtual { get; set; }

        [GridColumn("Não Atendido")]
        public decimal NaoAtendido { get; set; }
        
        [GridColumn("OP")]
        public decimal OP { get; set; }
        

        [GridColumn("Falta")]
        public decimal Falta 
        {
            get
            {
                //ESTIMATIVA DESEJO ESTOQUE  - (((DESEJO CLIENTE)*100)/90)*10% [Sendo o 90 a diferença DE 100 (-) % ideal de estoque inserido no início do relatório] e [Sendo o 10% o %ideal inserido no início do relatório]						
                decimal valor = (OP - (Faturado + Pedido));
                if (ExibirCotacao)
                    valor -= Cotacao;
                return valor;
            }
        }

        [GridColumn("Estimativa Desejo Estoque")]
        public decimal EstimativaDesejoEstoque 
         {
            get
            {
                //ESTIMATIVA DESEJO ESTOQUE  - (((DESEJO CLIENTE)*100)/90)*10% [Sendo o 90 a diferença DE 100 (-) % ideal de estoque inserido no início do relatório] e [Sendo o 10% o %ideal inserido no início do relatório]						
                decimal valor = ((DesejoCliente * 100) / (100 - PercentualEstoqueIdeal)) * (PercentualEstoqueIdeal / 100);

                if (valor > 0)
                    return Math.Ceiling(valor);

                return 0;
            }
        }

        [GridColumn("Analise para recompra")]
        public decimal AnaliseParaRecompra
        {
            get
            {
                var valor = EstoqueRealEstimado;
                if (valor < 0)
                    return - valor;

                return 0;
            }
        }

        [GridColumn("Fabricar")]
        public decimal Fabricar
        {
            get
            {
                //decimal resto = (AnaliseParaRecompra.GetValueOrDefault() % 20);
                int inteiro = Convert.ToInt32(AnaliseParaRecompra / 20);
                decimal valor = 20 * Decimal.Truncate(inteiro);

                //if (resto >= 10)
                //    valor += 20;
                


                if (valor > 0)
                    return valor;

                return 0;
            }
        }

        [GridColumn("Estoque Real Estimado")]
        public decimal EstoqueRealEstimado
        {
            get
            {
                decimal valor = OP - EstimativaDesejoEstoque - DesejoCliente - TotalSaidaSemVendas;
                return valor;
            }
        }

        [GridColumn("Estoque Indesejado")]
        public decimal EstoqueIndesejado
        {
            get
            {
                decimal valor = EstoqueRealEstimado > 0 ? EstoqueRealEstimado : 0;
                return valor;
            }

        }

        [GridColumn("Projeção de Venda")]
        public decimal ProjecaoVenda
        {
            get
            {
                decimal valor = DesejoCliente + (DesejoCliente * (PercentualEstoqueIdeal / 100));

                if (valor > 0)
                    return valor;

                return 0;
            }
        }

        [GridColumn("Assertividade Produtiva")]
        public decimal AssertividadeProdutiva
        {
            get
            {
                decimal valor = PlanoProducao - OP;

                if (valor > 0)
                    return valor;

                return 0;
            }
        } 
    }
}
