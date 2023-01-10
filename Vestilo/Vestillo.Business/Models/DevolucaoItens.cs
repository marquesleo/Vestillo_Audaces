
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("DevolucaoItens", "DevolucaoItens")]
    public class DevolucaoItens
    {
        [Chave]
        public int Id { get; set; }
        public int IdDevolucao { get; set; }        
        public int iditem { get; set; }
        public int? idcor { get; set; }
        public int? idtamanho { get; set; }
        public decimal Quantidade { get; set; }       
        public decimal Preco { get; set; }
        public decimal total { get; set; }
        
    }


    public class DevolucaoItensView
    {
    
        public int Id { get; set; }
        public int IdDevolucao { get; set; }
        public int Key { get; set; }
        public int AlmoxarifadoId { get; set; }
        public int ProdutoId { get; set; }
        public string ProdutoReferencia { get; set; }
        public string ProdutoDescricao { get; set; }
        public int? TamanhoId { get; set; }
        public string TamanhoAbreviatura { get; set; }
        public int? CorId { get; set; }
        public string CorAbreviatura { get; set; }
        public decimal Saldo { get; set; }
        public decimal Qtd { get; set; }
        public decimal Preco { get; set; }
        public decimal total { get; set; }

    }
}