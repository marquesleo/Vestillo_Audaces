﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class GestaoOrdemCompra
    {

        public string MaterialReferencia { get; set; }
        public string MaterialDescricao { get; set; }
        public string TamanhoDescricao { get; set; }
        public string CorDescricao { get; set; }
        public string ArmazemDescricao { get; set; }
        public string UM { get; set; }
        public string SE { get; set; }        
        public string Nfe { get; set; }

        public int ArmazemId { get; set; }
        public int CorId { get; set; }
        public int TamanhoId { get; set; }
        public int MateriaPrimaId { get; set; }
        public int CorOriginalId { get; set; }
        public int TamanhoOriginalId { get; set; }
        public int MateriaPrimaOriginalId { get; set; }

        public decimal QuantidadeEmpenhada { get; set; }
        public decimal QuantidadeEmpenhadaTotal { get; set; }
        public decimal QuantidadeNecessaria { get; set; }
        public decimal QuantidadeNecessariaTotal { get; set; }
        public decimal QuantidadeBaixada { get; set; }
        public decimal CompraEfetiva { get; set; }
        public decimal CompraEfetivaPorOp { get; set; }
        public decimal CompraFaltante { get; set; }
        public decimal CompraFaltanteTotal { get; set; }
        public decimal NecessidadeCompraTotal { get; set; }
        public decimal SaldoDisponivel { get; set; }
        public decimal EstoqueTotal { get; set; }
        public decimal Preco { get; set; }
        public decimal NecessidadeCompraOp { get; set; }
    }
}
