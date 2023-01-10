using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Business.Models
{
    [Tabela("TotaisCaixas", "Totais do Caixa")]
    public class TotaisCaixas
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int idempresa { get; set; }
        public int idcaixa { get; set; }
        public int? Idcolaborador { get; set; }
        public DateTime datamovimento { get; set; }
        /// <summary>
        /// 1 Credito ; 2 Debito ; 
        /// </summary>
        public int tipo { get; set; }      
        public decimal dinheiro { get; set; } 
        public decimal cheque { get; set; } 
        public decimal cartaocredito { get; set; } 
        public decimal cartaodebito { get; set; } 
        public decimal outros { get; set; }
        public decimal PixDep { get; set; }
        public string operadoracredito { get; set; }
        public string operadoradebito { get; set; }
        public string observacao { get; set; }  
        public int? idNfce { get; set; }
        public int? idNfe { get; set; }

    }

    public class TotaisCaixasView
    {
        public int Id { get; set; }
        public int tipo { get; set; }
        [GridColumn("Referência do Caixa")]
        public string ReferenciaCaixa { get; set; }
        [GridColumn("Descrição do Caixa")]
        public string DescricaoCaixa { get; set; }
        public int idcaixa { get; set; }
        public int? Idcolaborador { get; set; }
        [GridColumn("Data do Movimento")]
        public DateTime datamovimento { get; set; }
        [GridColumn("Nome Colaborador")]
        public string NomeColaborador { get; set; }
        [GridColumn("Tipo Movimento")]
        public string Descricaotipo
        {
            get
            {
                if (tipo == 1)
                    return "Crédito";
                else if (tipo == 2)
                    return "Débito";
                else
                    return "";
            }
        }
        [GridColumn("Entrada em Dinheiro")]
        public decimal dinheiro { get; set; }

        [GridColumn("Saída em Dinheiro")]
        public decimal dinheiroDebito { get; set; }

        [GridColumn("Cheque")]
        public decimal cheque { get; set; }

        [GridColumn("Cartão de Crédito")]
        public decimal cartaocredito { get; set; }

         [GridColumn("Cartão de Débito")]
        public decimal cartaodebito { get; set; }

        [GridColumn("Outros")]
        public decimal outros { get; set; }
        [GridColumn("Pix/Depósito")]
        public decimal PixDep { get; set; }

        [GridColumn("Saldo")]
        public decimal Saldo { get; set; }

        [GridColumn("Operadora do Crédito")]
        public string operadoracredito { get; set; }
        [GridColumn("Operadora do Débito")]
        public string operadoradebito { get; set; }
        [GridColumn("Observação")]
        public string observacao { get; set; }        

    }

                                                                                
                  
                   
                   
                                                   
                  







}
