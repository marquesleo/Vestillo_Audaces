using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public enum enumTipoColaborador
    {
        Cliente,
        Fornecedor,
        Vendedor
    }

    public enum enumStatusPedidoVenda
    {
        Incluido = 1,
        Faturado_Parcial = 2,
        Faturado_Total = 3,
        Finalizado = 4,
        Bloqueado = 5,
        Liberado = 6,
        Conferencia = 7,
        Conferencia_Parcial = 8,
        Aguardando_Liberacao = 9,
        Bloqueado_Financeiro = 10,
        Credito_Liberado = 11
    }

    public enum enumStatusLiberacaoPedidoVenda
    {
        Atendido = 1,
        Atendido_Parcial = 2,
        Sem_Estoque = 3,
        Atendido_Producao = 4,
        Producao = 5
    }

    public enum enumStatusPedidoCompra
    {
        Incluido = 1,
        Faturado_Parcial = 2,
        Faturado_Total = 3,
        Finalizado = 4
    }

    public enum enumStatusContasReceber
    {
        Aberto = 1,
        Baixa_Parcial = 2,
        Baixa_Total = 3,
        Negociado = 4
    }

    public enum enumStatusContasPagar
    {
        Aberto = 1,
        Baixa_Parcial = 2,
        Baixa_Total = 3
    }

    public enum enumStatusComissao
    {
        Aberto = 1,
        Liberado = 2        
    }

    public enum enumStatusCreditoCliente
    {
        Aberto = 1,
        Quitado = 2
    }

    public enum enumTipoMovBanco
    {
        Credito = 1,
        Debito = 2,
        Transferencia = 3
    }

    public enum enumTitulo
    {
        ContasAReceber = 1,
        ContasAPagar = 2
    }

    public enum enumTipoDocumento
    {
        Indefinido = 0,
        Dinheiro = 1,
        Cheque = 2,
        Duplicata = 3, 
        Nota_Credito_Cliente =  4,
        Nota_Credito_Fornecedor = 5,
        Nota_Debito_Cliente = 6,
        Nota_Debito_Fornecedor = 7,
        Nota_Fiscal = 8,
        Vale = 9,
        Cartão_de_Credito = 10,
        Cartão_de_Debito = 11,
        Cartorio = 12,
        Deposito = 99,
        Pix = 100
    }

    public enum enumStatusItemOrdemProducao
    {
        Novo = 0,
        Liberado = 1,
        Pacote = 2
    }

    public enum enumStatusPacotesProducao
    {
        Aberto = 0,
        Producao = 3,
        Concluido = 5,
        Finalizado = 6
    }


    public enum enumStatusOrdemProducao
    {
        Novo = 0,
        Aberto = 1,
        Producao_Parcial = 2,
        Em_producao = 3,
        Atendido_Parcial = 4,
        Atendido = 5,
        Finalizado = 6,
        Em_Corte = 8,
        Apenas_Liberado = 9,
        Liberado_Parcial = 10,
        Enviado_Corte = 11
    }

    public enum enumTipoMovimentoCaixa
    {
        Credito = 1,
        Debito = 2 
    }


    public enum enumControlaCaixa
    {
        Abrir = 1,
        Fechar = 2
    }

    public enum enumSuprimentoSangria
    {
        Suprimento = 1,
        Sangria = 2
    }

    public enum enumImprimirVisualizarDanfe
    {
        Visualizar = 1,
        Imprimir = 2
    }

    public enum enumPastaDoXml
    {
        nfe = 1,
        distr = 2
    }

    public enum enumTipoDocEletronico
    {
        nfe = 1,
        nfce = 2
    }

public enum TiposPagamento
{
    Outros = 0,
    Dinheiro = 1,
    Cheque = 2,
    CartaoCredito = 10,
    CartaoDebito = 11,
    Deposito = 99,
    Pix = 100,
    Transferencia = 101,
    Cashback = 102
}

