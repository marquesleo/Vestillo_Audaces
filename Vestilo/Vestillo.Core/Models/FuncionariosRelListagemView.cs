
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Core.Models
{

    public class FuncionariosRelListagemView
    {             
        [GridColumn("Ref. Funcionário")]
        public string Referencia { get; set; }
        [GridColumn("Nome")]
        public string Nome { get; set; }
        [GridColumn("Cpf")]
        public string CPF { get; set; }
        [GridColumn("Cargo")]
        public string Cargo { get; set; }
        [GridColumn("Nascimento")]
        public DateTime? DataNascimento { get; set; }
        [GridColumn("Admissão")]
        public DateTime? DataAdmissao { get; set; }
        [GridColumn("Demissão")]
        public DateTime? DataDemissao { get; set; }
        [GridColumn("DDD")]
        public string DDD { get; set; }
        [GridColumn("Telefone")]
        public string Telefone { get; set; }
        [GridColumn("RG")]
        public string RG { get; set; }
        [GridColumn("Ativo")]
        public bool Ativo { get; set; }
        [GridColumn("Obs")]
        public string Obs { get; set; }


        public int Id { get; set; }
        
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public int? EstadoId { get; set; }
        public int CalendarioId { get; set; }       
        public string Mes { get; set; }
        public byte[] Foto { get; set; }
    }
}
