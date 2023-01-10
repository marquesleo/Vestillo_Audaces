using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System.Globalization;

namespace Vestillo.Business.Controllers
{
    public class FuncionarioController : GenericController<Funcionario, FuncionarioRepository>
    {
        public IEnumerable<FuncionarioView> GetAllView()
        {
            return _repository.GetAllView();
        }

        public bool CpfRGExiste(string Cpf, string RG)
        {
            return _repository.CpfRGExiste(Cpf, RG);
        }

        public List<FuncionarioView> GetByIdView(List<int> id)
        {
            return _repository.GetByIdView(id);
        }

        public IEnumerable<Funcionario> GetAllAtivo()
        {
            return _repository.GetAllAtivo();
        }

        public override void Delete(int id)
        {
            bool openTransaction = false;

            try
            {
                openTransaction = _repository.BeginTransaction();

                FuncionarioDespesaRepository funcionarioDespesaRepository = new FuncionarioDespesaRepository();
                funcionarioDespesaRepository.DeleteByFuncionario(id);

                FuncionarioMaquinaRepository funcionarioMaquinaRepository = new FuncionarioMaquinaRepository();
                funcionarioMaquinaRepository.DeleteByFuncionario(id);

                base.Delete(id);

                if (openTransaction)
                    _repository.CommitTransaction();
            }
            catch (Exception ex)
            {
                if (openTransaction)
                    _repository.RollbackTransaction();

                throw ex;
            }


        }

        public override Funcionario GetById(int id)
        {
            Funcionario funcionario = base.GetById(id);

            if (funcionario != null)
            {
                FuncionarioDespesaRepository funcionarioDespesaRepository = new FuncionarioDespesaRepository();
                funcionario.Despesas = funcionarioDespesaRepository.GetByFuncionario(id).ToList();

                FuncionarioMaquinaRepository funcionarioMaquinaRepository = new FuncionarioMaquinaRepository();
                funcionario.Maquinas = funcionarioMaquinaRepository.GetByFuncionario(id);
            }

            return funcionario;
        }

        public IEnumerable<FuncionarioView> GetListPorCargo(string cargo)
        {
            return _repository.GetListPorCargo(cargo);
        }

        public IEnumerable<FuncionarioView> GetListPorNome(string nome)
        {
            return _repository.GetListPorNome(nome);
        }

        public IEnumerable<FuncionarioSimplificado> GetListComCargo()
        {
            return _repository.GetListComCargo();
        }

        public FuncionarioView GetFuncPorCPF(string CPF)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Funcionario> GetByFuncionarioProducao(List<int> funcionariosIds)
        {
            IEnumerable<Funcionario> funcionarios = _repository.GetByIds(funcionariosIds, "Nome");

            if (funcionarios != null && funcionarios.Count() > 0)
            {
                foreach (var funcionario in funcionarios)
                {
                    TempoFuncionarioRepository tempoFuncionarioRepository = new TempoFuncionarioRepository();
                    funcionario.Tempos = tempoFuncionarioRepository.GetByFuncionario(funcionario.Id).ToList();

                    OcorrenciaFuncionarioRepository ocorrenciaFuncionarioRepository = new OcorrenciaFuncionarioRepository();
                    funcionario.Ocorrencias = ocorrenciaFuncionarioRepository.GetByFuncionario(funcionario.Id).ToList();

                    OperacaoOperadoraRepository operacaoOperadoraRepository = new OperacaoOperadoraRepository();
                    funcionario.Operacoes = operacaoOperadoraRepository.GetByFuncionario(funcionario.Id).ToList();

                    ProdutividadeRepository produtividadeRepository = new ProdutividadeRepository();
                    funcionario.Produtividades = produtividadeRepository.GetAll().ToList();
                }
            }

            return funcionarios;
        }

        public IEnumerable<Funcionario> GetByFuncionarioDataProducao(List<int> premiosIds, List<int> funcionariosIds, DateTime daData, DateTime ateData, string ordenacao, bool premioPartida)
        {
            IEnumerable<Funcionario> funcionarios = _repository.GetByIdsEPremios(premiosIds, funcionariosIds, ordenacao, premioPartida);

            CarregarCampos(daData, ateData, funcionarios, premioPartida);

            return funcionarios;
        }

        public IEnumerable<Funcionario> GetByPremioDataProducao(List<int> premiosIds, DateTime daData, DateTime ateData, string ordenacao, bool premioPartida)
        {
            IEnumerable<Funcionario> funcionarios = _repository.GetByPremios(premiosIds, ordenacao, premioPartida);

            CarregarCampos(daData, ateData, funcionarios, premioPartida);

            return funcionarios;
        }


        private static void CarregarCampos(DateTime daData, DateTime ateData, IEnumerable<Funcionario> funcionarios, bool premioPartida)
        {
            if (funcionarios != null && funcionarios.Count() > 0)
            {
                foreach (var funcionario in funcionarios)
                {
                    TempoFuncionarioRepository tempoFuncionarioRepository = new TempoFuncionarioRepository();
                    funcionario.Tempos = tempoFuncionarioRepository.GetByFuncionarioIdEDatas(funcionario.Id, daData, ateData).ToList();

                    OcorrenciaFuncionarioRepository ocorrenciaFuncionarioRepository = new OcorrenciaFuncionarioRepository();
                    funcionario.Ocorrencias = ocorrenciaFuncionarioRepository.GetByFuncionarioIdEDatas(funcionario.Id, daData, ateData).ToList();

                    OperacaoOperadoraRepository operacaoOperadoraRepository = new OperacaoOperadoraRepository();
                    funcionario.Operacoes = operacaoOperadoraRepository.GetByFuncionarioIdEDatas(funcionario.Id, daData, ateData).ToList();

                    ProdutividadeRepository produtividadeRepository = new ProdutividadeRepository();
                    funcionario.Produtividades = produtividadeRepository.GetByFuncionarioIdEDatas(funcionario.Id, daData, ateData).ToList();

                    if (premioPartida)
                    {
                        PremioPartidaRepository premioRepository = new PremioPartidaRepository();
                        funcionario.PremioPartida = premioRepository.GetByFuncionario(funcionario.Id);
                    }
                    else
                    {
                        PremioRepository premioRepository = new PremioRepository();
                        funcionario.Premio = premioRepository.GetByFuncionario(funcionario.Id);
                    }                    
                }
            }
        }

        public override void Save(ref Funcionario funcionario)
        {
            bool openTransaction = false;

            try
            {
                openTransaction = _repository.BeginTransaction();

                base.Save(ref funcionario);

                IEnumerable<FuncionarioDespesa> funcionarioDespesas = funcionario.Despesas ?? new List<FuncionarioDespesa>();
                IEnumerable<FuncionarioMaquinaView> funcionarioMaquinas = funcionario.Maquinas ?? new List<FuncionarioMaquinaView>();

                //=================================================================================================
                // Despesas 
                //=================================================================================================
                FuncionarioDespesaRepository funcionarioDespesaRepository = new FuncionarioDespesaRepository();
                funcionarioDespesaRepository.DeleteByFuncionario(funcionario.Id);

                foreach (FuncionarioDespesa d in funcionarioDespesas)
                {
                    var despesa = d;
                    despesa.Id = 0;
                    despesa.FuncionarioId = funcionario.Id;
                    funcionarioDespesaRepository.Save(ref despesa);
                }

                //=================================================================================================
                // Maquinas 
                //=================================================================================================
                FuncionarioMaquinaRepository funcionarioMaquinaRepository = new FuncionarioMaquinaRepository();
                funcionarioMaquinaRepository.DeleteByFuncionario(funcionario.Id);

                foreach (FuncionarioMaquinaView d in funcionarioMaquinas)
                {
                    var maquina = d as FuncionarioMaquina;
                    maquina.Id = 0;
                    maquina.FuncionarioId = funcionario.Id;
                    funcionarioMaquinaRepository.Save(ref maquina);
                }

                //=================================================================================================
                // Colaborador 
                //=================================================================================================
                using (ColaboradorController colaboradorController = new ColaboradorController())
                {
                    Colaborador colaborador = colaboradorController.GetByIdFuncionario(funcionario.Id);

                    if (colaborador == null)
                    {
                        if (!colaboradorController.CnpfCpfExiste(funcionario.CPF, 0))
                            colaborador = new Colaborador();
                        else
                            colaborador = new ColaboradorRepository().GetByCnpj(funcionario.CPF);

                        colaborador.IdFuncionario = funcionario.Id;
                        colaborador.Ativo = true;
                    }

                    colaborador.RazaoSocial = funcionario.Nome;
                    colaborador.IdEmpresa = funcionario.EmpresaId == null ? 1 : (int)funcionario.EmpresaId;
                    colaborador.Nome = funcionario.Nome;
                    colaborador.CnpjCpf = funcionario.CPF;
                    colaborador.RegistroGeral = funcionario.RG;
                    colaborador.Cep = funcionario.CEP;
                    colaborador.Endereco = funcionario.Endereco;
                    colaborador.Complemento = funcionario.Complemento;
                    colaborador.Bairro = funcionario.Bairro;
                    colaborador.IdEstado = funcionario.EstadoId;
                    colaborador.Ddd = funcionario.DDD;
                    colaborador.Telefone = funcionario.Telefone;
                    colaborador.DataNascimento = funcionario.DataNascimento;
                    colaborador.Observacao = funcionario.Obs;
                    colaborador.Referencia = funcionario.Referencia;
                    colaborador.Fornecedor = true;
                    colaborador.IdPais = 1058;

                    new ColaboradorRepository().Save(ref colaborador);
                }
                //=================================================================================================

                if (openTransaction)
                    _repository.CommitTransaction();
            }
            catch (Exception ex)
            {
                if (openTransaction)
                    _repository.RollbackTransaction();

                throw ex;
            }
        }

        public IEnumerable<Funcionario> GetByCalendario(int idCalendario)
        {
            return _repository.GetByCalendario(idCalendario);
        }

        public void AtualizarMinutoDia(int IdCalendario)
        {
            bool openTransaction = false;

            try
            {
                openTransaction = _repository.BeginTransaction();

                double _minutosTotaisSemana = 0;
                double MinDias = 0;
                int ContadorDias = 0;
                DayOfWeek DiasSemana = 0;

                var CalIntervalos = new CalendarioFaixasRepository().GetByCalendario(IdCalendario);

                DateTime dataDe, dataAte;
                string value;
                IFormatProvider culture = new CultureInfo("EN", true);
                string dataBase = "01/01/2000 ";


                foreach (var item in CalIntervalos)
                {
                    if (!string.IsNullOrEmpty(item.HoraFinal))
                    {
                        value = dataBase + item.HoraInicial;
                        DateTime.TryParseExact(value, new string[] { "dd/MM/yyyy HH:mm", "dd/MM/yyyy hh:mm" }, culture, System.Globalization.DateTimeStyles.None, out dataDe);

                        value = dataBase + item.HoraFinal;
                        DateTime.TryParseExact(value, new string[] { "dd/MM/yyyy HH:mm", "dd/MM/yyyy hh:mm" }, culture, System.Globalization.DateTimeStyles.None, out dataAte);

                        TimeSpan diff = dataAte - dataDe;
                        _minutosTotaisSemana += diff.TotalMinutes;
                    }
                    if (DiasSemana != item.Dia)
                    {
                        ContadorDias += 1;
                    }
                    DiasSemana = item.Dia;
                }

                if (ContadorDias > 0)
                {
                    MinDias = _minutosTotaisSemana / ContadorDias;
                }

                var funcionarios = GetByCalendario(IdCalendario);
                foreach (var func in funcionarios)
                {
                    Funcionario novoFuncionario = func;
                    novoFuncionario.MinutosDia = Convert.ToInt32(MinDias);
                    base.Save(ref novoFuncionario);
                }
                if (openTransaction)
                    _repository.CommitTransaction();
            }
            catch (Exception ex)
            {
                if (openTransaction)
                    _repository.RollbackTransaction();

                throw ex;
            }
        }

        public void AtualizaPercentuaisEmpresa(Empresa empresa)
        {
            bool openTransaction = false;

            try
            {
                openTransaction = _repository.BeginTransaction();

                var funcionarios = GetAll().Where(f => f.EmpresaId == empresa.Id);

                foreach(Funcionario funcionario in funcionarios)
                {
                    Funcionario novoFuncionario = new Funcionario();
                    novoFuncionario = funcionario;
                    novoFuncionario.AliquotaFGTS = Math.Round(empresa.fgts,2);
                    novoFuncionario.AliquotaINSS = Math.Round(empresa.inss,2);

                    decimal salarioBase = Convert.ToDecimal(novoFuncionario.SalarioBase);
                    decimal aliquotaFgts = Convert.ToDecimal(novoFuncionario.AliquotaFGTS);
                    decimal aliquotaInss = Convert.ToDecimal(novoFuncionario.AliquotaINSS);
                    decimal gratificacao = Convert.ToDecimal(novoFuncionario.Gratificacao);
                    decimal auxilioSaude = Convert.ToDecimal(novoFuncionario.AuxilioSaude);
                    decimal auxilioTransporte = Convert.ToDecimal(novoFuncionario.AuxilioTransporte);
                    decimal auxilioAlimentacao = Convert.ToDecimal(novoFuncionario.AuxilioAlimentacao);
                    decimal valeAlimentacao = Convert.ToDecimal(novoFuncionario.ValeAlimentacao);
                    decimal valeTransporte = Convert.ToDecimal(novoFuncionario.ValeTransporte);
                    decimal planoSaude = Convert.ToDecimal(novoFuncionario.PlanoSaude);

                    var despesas = new FuncionarioDespesaRepository().GetByFuncionario(funcionario.Id);
                    decimal outrasDespesas = despesas == null ? 0 : despesas.Sum(x => x.Valor);
                    
                    var despesaTotal = ((salarioBase + gratificacao + auxilioSaude + auxilioTransporte + auxilioAlimentacao + valeAlimentacao + valeTransporte + planoSaude) +
                                             (salarioBase / 12) + ((salarioBase / 12) / 3) +
                                             (salarioBase * (aliquotaFgts / 100)) + (salarioBase * (aliquotaInss / 100)) + outrasDespesas).ToMoney();

                    novoFuncionario.DespesaTotal = despesaTotal.ToDecimal();

                    base.Save(ref novoFuncionario);
                }

                if (openTransaction)
                    _repository.CommitTransaction();
            }
            catch (Exception ex)
            {
                if (openTransaction)
                    _repository.RollbackTransaction();

                throw ex;
            }
        }
    }
}
