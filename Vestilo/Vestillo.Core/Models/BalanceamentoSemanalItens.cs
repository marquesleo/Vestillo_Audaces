
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{
    [Tabela("BalanceamentoSemanalItens")]
    public class BalanceamentoSemanalItens : IModel
    {
        [Chave]
        public int Id { get; set; }
        public int SetorId { get; set; }
        public int BalanceamentoId { get; set; }        
        public decimal PessoasTrabalhando { get; set; }
        public decimal TempoNecessario { get; set; }
        public decimal JornadaSemanal { get; set; }
        public decimal ResultadoTempo { get; set; }       
        public decimal Aproveitamento { get; set; }      
        public decimal Eficiencia { get; set; }     
        public decimal Presenca { get; set; }       
        
    }

    public class BalanceamentoSemanalItensView: BalanceamentoSemanalItens
    {
        [NaoMapeado]
        public string Setor { get; set; }
        [NaoMapeado]
        public decimal CalculoSemanal
        {
            get
            {
                decimal JdSemanal = (PessoasTrabalhando * JornadaSemanal) * (Aproveitamento / 100) * (Eficiencia / 100) * (Presenca / 100);
                return decimal.Round(JdSemanal,4);
                
            }
        }

        [NaoMapeado]
        public decimal DiferencaTempo
        {

            get
            {
                decimal Resultado = CalculoSemanal - TempoNecessario;
                return decimal.Round(Resultado,4);

            }
        }
        /*
         * 
         *

        [NaoMapeado]
        public string StatusDesc
        {
            get
            {
                switch (Status)
                {
                    case 0:
                        return "Lib";
                    case 1:
                        return "Prod";
                    case 2:
                        return "Geral";
                    default:
                        return "Lib";
                }
            }
        }

        [NaoMapeado]
        public decimal? CapacidadeGeral { get; set; }



        [NaoMapeado]
        public decimal? Disponivel
        {
            get
            {
                if (Capacidade != null)
                    return Capacidade - Total;
                else
                    return -Total;
            }
        }
        [NaoMapeado]
        public decimal? Coeficiente
        {
            get
            {
                if (Status != 2)
                    return MinutoProducao * Eficiencia / 100;
                else if (Operadoras != 0)
                    return CapacidadeGeral / Operadoras;
                else
                    return 0;
            }
        }
        [NaoMapeado]
        public decimal? TotalNovos { get; set; }
        [NaoMapeado]
        public decimal? OperadorasNecessarias
        {
            get
            {
                if (Status == 2)
                    if (Coeficiente > 0)
                        return (Total + TotalNovos) / Coeficiente;
                    else
                        return 0;
                else
                    return 0;
            }
        }
        [NaoMapeado]
        public decimal? CapacidadeRestante
        {
            get
            {
                if (Status == 2)
                    return Disponivel - TotalNovos;
                else
                    return 0;
            }
        }
        [NaoMapeado]
        public decimal? Sobra
        {
            get
            {
                if (Status == 2)
                    if (Coeficiente > 0)
                        return CapacidadeRestante / Coeficiente;
                    else
                        return 0;
                else
                    return 0;
            }
        }
        */
    }
}

