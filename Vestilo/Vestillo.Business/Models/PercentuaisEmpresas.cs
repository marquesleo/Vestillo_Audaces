using Vestillo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("PercentuaisEmpresas", "Percentuais da Empresa")]
    public class PercentuaisEmpresas
    {
        [Vestillo.Chave]
        public int Id {get;set;}
        [Vestillo.FiltroEmpresa]
        public int EmpresaId{get;set;}       
        public decimal eficienciaMan {get;set;}
        public decimal aproveitamentoMan {get;set;}
        public decimal presencaMan {get;set;}
        public decimal eficienciaAut {get;set;}
        public decimal aproveitamentoAut {get;set;}
        public decimal presencaAut {get;set;}
        public decimal despfixfuncionario {get;set;}
        public decimal despfixvarprevista {get;set;}
        public decimal despfixvarrealizada {get;set;}
        public bool calcperauto { get; set; }
        public bool fixarcustosfunc { get; set; }
        public bool fixarcustosprev { get; set; }
        public bool fixarcustosreal { get; set; }
        public int DiasUteis { get; set; }   
    }

    public class PercentuaisEmpresasView
    {        
        public int Id { get; set; }
        public string NomeEmpresa { get; set; }       
        public decimal eficienciaMan { get; set; }
        public decimal aproveitamentoMan { get; set; }
        public decimal presencaMan { get; set; }
        public decimal eficienciaAut { get; set; }
        public decimal aproveitamentoAut { get; set; }
        public decimal presencaAut { get; set; }
        public decimal despfixfuncionario { get; set; }
        public decimal despfixvarprevista { get; set; }
        public decimal despfixvarrealizada { get; set; }
        public bool calcperauto { get; set; }
        public bool fixarcustosfunc { get; set; }
        public bool fixarcustosprev { get; set; }
        public bool fixarcustosreal { get; set; }
        public int DiasUteis { get; set; } 
    }
}
