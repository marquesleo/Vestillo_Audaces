using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Funcionarios","Funcionários")]
    public class Funcionario
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int? EmpresaId { get; set; }
        [OrderByColumn]
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        [Vestillo.Contador("Funcionario")]
        [RegistroUnico]
        public string Referencia { get; set; }
        public string TituloEleitorNumero { get; set; }
        public string TituloEleitorSecao { get; set; }
        public string TituloEleitorZona { get; set; }
        public DateTime? TituloEleitorDataEmissao { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public int? EstadoId { get; set; }
        public int? CargoId { get; set; }
        public int CalendarioId { get; set; }
        public string DDD { get; set; }
        public string Telefone { get; set; }
        public DateTime? DataNascimento { get; set; }
        public DateTime? DataAdmissao { get; set; }
        public DateTime? DataDemissao { get; set; }
        public string CarteiraTrabalhoNumero { get; set; }
        public string CarteiraTrabalhoSerie { get; set; }
        public bool Ativo { get; set; }
        public bool CalculaProducao { get; set; }
        public bool ExecutaOperacaoManual { get; set; }
        public byte[] Foto { get; set; }
        public string Obs { get; set; }
        public decimal? SalarioBase { get; set; }
        public int? MinutosDia { get; set; }
        public decimal? AliquotaFGTS { get; set; }
        public decimal? AliquotaINSS { get; set; }
        public decimal? DespesaTotal { get; set; }
        public decimal? Gratificacao { get; set; }
        public decimal? DecimoTerceiroProporcional { get; set; }
        public decimal? FeriasProporcional { get; set; }
        public decimal? AuxilioAlimentacao { get; set; }
        public decimal? AuxilioSaude { get; set; }
        public decimal? AuxilioTransporte { get; set; }
        public decimal? ValeAlimentacao { get; set; }
        public decimal? ValeTransporte { get; set; }
        public decimal? PlanoSaude { get; set; }
        public string RelogioDePonto { get; set; }
        public int UsaCupom { get; set; }
        [NaoMapeado]
        public String DescUsaCupom { get; set; }

        [NaoMapeado]
        public IEnumerable<FuncionarioDespesa> Despesas { get; set; }
        [NaoMapeado]
        public IEnumerable<FuncionarioMaquinaView> Maquinas { get; set; }
        [Vestillo.NaoMapeado]
        public List<Produtividade> Produtividades { get; set; }
        [Vestillo.NaoMapeado]
        public List<OcorrenciaFuncionarioView> Ocorrencias { get; set; }
        [Vestillo.NaoMapeado]
        public List<TempoFuncionario> Tempos { get; set; }
        [Vestillo.NaoMapeado]
        public List<OperacaoOperadoraView> Operacoes { get; set; }

        [Vestillo.NaoMapeado]
        public Premio Premio { get; set; }

        [Vestillo.NaoMapeado]
        public PremioPartida PremioPartida { get; set; }

        [Vestillo.NaoMapeado]
        public string RefNome { get { return "(" + Referencia + ") " + Nome; } }
    }

    public class FuncionarioView
    {
        public int Id { get; set; }
        public string Referencia { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Cargo { get; set; }        
        public DateTime? DataNascimento { get; set; }
        public DateTime? DataAdmissao { get; set; }
        public DateTime? DataDemissao { get; set; }
        public bool Ativo { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public int? EstadoId { get; set; }    
        public int EmpresaId { get; set; }
        public int CalendarioId { get; set; }
        public string DDD { get; set; }
        public string Telefone { get; set; }
        public string RG { get; set; }
        public string Obs { get; set; }
        public string Mes { get; set; }
        public byte[] Foto { get; set; }
        public int UsaCupom { get; set; }        

    }
}
