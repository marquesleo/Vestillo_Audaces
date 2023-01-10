
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("Caixas", "Caixas")]
    public class Caixas
    {
        [Vestillo.Chave]
        public int Id { get; set; }     
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }   
        [Vestillo.RegistroUnico]
        public String referencia { get; set; }       
        public String descricao { get; set; }    
        public DateTime? dataultabertura { get; set; }
        public DateTime? dataultfechamento { get; set; }       
        public bool Ativo { get; set; }
        public decimal saldo { get; set; }
    }

}

