using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class ConsultaRelListaMateriaisView
    {
        public int materiaPrimaId { get; set; }
        public int corId { get; set; }
        public int tamanhoId { get; set; }
        public string referencia { get; set; }
        public string descricao { get; set; }
        public string corAbreviatura { get; set; }
        public string tamAbreviatura { get; set; }
        public string uniAbreviatura { get; set; }
        public string grupoMaterial { get; set; }
        public int idGrupoMaterial { get; set; }
        public decimal quantidade { get; set; }
        public decimal qtdopliberada { get; set; }
        public decimal qtdopnaoliberada { get; set; }
        public decimal saldo { get; set; }
        public decimal estoquebaixado { get; set; }
        public decimal estoqueempenhado { get; set; }
        public decimal semana0 { get; set; }
        public decimal semana1 { get; set; }
        public decimal semana2 { get; set; }
        public decimal semana3 { get; set; }
        public decimal semana4 { get; set; }
        public decimal semana5 { get; set; }
        public decimal semana6 { get; set; }

        public decimal estoquefisico
        {
            get
            {
                return saldo + estoqueempenhado;
            }
        }

        public decimal saldo1
        {
            get
            {
                return estoquefisico - estoqueempenhado - qtdopliberada;
            }
        }
        public decimal saldo2
        {
            get
            {
                return saldo1 - qtdopnaoliberada;
            }
        }
        public decimal saldo3
        {
            get
            {
                return saldo2 + semana0 + semana1 + semana2 + semana3 + semana4 + semana5 + semana6;
            }
        }

        public decimal precoCusto { get; set; } 
        public decimal custo
        {
            get
            {
                return precoCusto * saldo3;
            }
        }

        public string descSemana1 { get; set; }
        public string descSemana2 { get; set; }
        public string descSemana3 { get; set; }
        public string descSemana4 { get; set; }
        public string descSemana5 { get; set; }
    }

    public class SemanasCompra{
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
