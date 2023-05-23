using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{
    [Tabela("Atividades")]
    public class Atividade : IModel
    {

        public enum EnumStatusAtividade
        {
            Incluido = 1,
            Concluido = 2
        }

        public enum EnumTipoAtividadeCliente
        {
            Indefinido = 0,
            Relacionamento = 1,
            Cobranca = 2
        }
        
        [Chave]
        public int Id { get; set; }
        [Contador("Atividade")]
        public string Referencia { get; set; }
        public bool AlertarUsuario { get; set; }
        [FiltroEmpresa]
        public int? EmpresaId { get; set; }
        public int TipoAtividadeId { get; set; }
        public int? ClienteId { get; set; }
        public int? VendedorId { get; set; }
        public string Observacao { get; set; }
        [DataAtual]
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtividade { get; set; }
        public DateTime? DataAlerta { get; set; }
        public DateTime? DataExibicaoAlerta { get; set; }
        public int UsuarioCriacaoAtividadeId { get; set; }
        public int? UsuarioAlertaAtividadeId { get; set; }
        public EnumStatusAtividade StatusAtividade { get; set; }
        public EnumTipoAtividadeCliente TipoAtividadeCliente { get; set; }
        public string RefTitulo { get; set; }
        public decimal ValorTitulo { get; set; }
        public int IdTitulo { get; set; }
    }
}
