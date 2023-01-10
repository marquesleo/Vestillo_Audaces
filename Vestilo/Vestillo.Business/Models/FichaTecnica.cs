using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;

namespace Vestillo.Business.Models
{
    [Tabela("FichaTecnica", "Ficha Técnica De Operação")]
    public class FichaTecnica
    {
        public FichaTecnica()
        {
            Operacoes = new List<FichaTecnicaOperacao>();
        }

        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int EmpresaId { get; set; }
        public int ProdutoId { get; set; }
        public bool UtilizaQuebra { get; set; }
        public decimal TempoTotal { get; set; }
        public decimal CustoFaccao { get; set; }
        public bool Ativo { get; set; }
        public string Observacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int? UserId { get; set; }
        public string QuebraManual { get; set; }

        [NaoMapeado]
        public List<FichaTecnicaOperacao> Operacoes { get; set; }
    }

    public class FichaTecnicaView
    {
        public int Id { get; set; }
        public string ProdutoReferencia { get; set; }
        public string ProdutoDescricao { get; set; }
        public int GrupoProdutoId { get; set; }
        public string GrupoProdutoDescricao { get; set; }
        public string Colecao { get; set; }
        public string Segmento { get; set; }
        public decimal TempoTotal { get; set; }
        public bool UtilizaQuebra { get; set; }
        public bool Ativo { get; set; }
        public int ProdutoId { get; set; }
        public decimal CustoFaccao { get; set; }
        public decimal ValorFaccao { get; set; }
        public string Observacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int? UserId { get; set; }
        public string NomeUsuario { get; set; }
        public List<FichaTecnicaOperacaoView> Operacoes { get; set; }
        public string QuebraManual { get; set; }
        public string AtivoDescricao
        {
            get
            {
                if (this.Ativo)
                    return "Sim";
                else
                    return "Não";
            }
        }
    }
}
