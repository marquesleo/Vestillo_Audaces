using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class EmpresaController : GenericController<Empresa, EmpresaRepository>
    {
        public override Empresa GetById(int id)
        {
            var empresa = base.GetById(id);
            if (empresa != null)
            {
                var ctEndereco = new EnderecoController();
                empresa.Enderecos = ctEndereco.GetByEmpresaId(id);
            }
            return empresa;
        }

        public override void Save(ref Empresa entity)
        {
            var repositoryEndereco = new EnderecoRepository();
            try
            {
                repositoryEndereco._cn.Provider.BeginTransaction();
                base.Save(ref entity);

                if (entity.Enderecos != null && entity.Enderecos.Count() > 0)
                {
                    foreach (var item in entity.Enderecos)
                    {
                        var endereco = item;

                        endereco.EmpresaId = entity.Id;
                        repositoryEndereco.Save(ref endereco);
                    }
                }

                using (FuncionarioController funcionarioController = new FuncionarioController())
                {
                    funcionarioController.AtualizaPercentuaisEmpresa(entity);
                }

                repositoryEndereco._cn.Provider.CommitTransaction();
            }
            catch (Exception ex)
            {
                repositoryEndereco._cn.Provider.RollbackTransaction();
                throw new Vestillo.Lib.VestilloException(ex);
            }
            finally
            {
                repositoryEndereco.Dispose();
                repositoryEndereco = null;
            }
        }

        public IEnumerable<Empresa> GetByUsuarioId(int usuarioId)
        {
            using (EmpresaRepository repository = new EmpresaRepository())
            {
                return repository.GetByUsuarioId(usuarioId);
            }
        }

        public void RetornaInformacaoMaoDeObra(ref int QtdFuncProducao, ref int QtdFuncExtras, ref  int MinutosTotalFuncProducao,
                                           ref int MinutosTotalFuncExtras, ref decimal CustoTotalFuncProducao, ref  decimal CustoTotalFuncExtras,
                                           ref decimal Aproveitamento, ref  decimal Presenca, ref  decimal Eficiencia, ref  decimal CustoMinutoMaoObra)
        {
            QtdFuncProducao = 0;
            QtdFuncExtras = 0;
            MinutosTotalFuncProducao = 0;
            MinutosTotalFuncExtras = 0;
            CustoTotalFuncProducao = 0;
            CustoTotalFuncExtras = 0;
            Aproveitamento = 0;
            Presenca = 0;
            Eficiencia = 0;
            CustoMinutoMaoObra = 0;
            var DadosEmpresa = new PercentuaisEmpresasRepository().GetEmpresaLogada(VestilloSession.EmpresaLogada.Id);
            var DadosFuncionario = new FuncionarioRepository().GetAll().Where(x => (x.EmpresaId == VestilloSession.EmpresaLogada.Id || x.EmpresaId == null) && x.Ativo == true);

            //separa funcionários de produção e não produção
            foreach (var func in DadosFuncionario)
            {
                if (func.CalculaProducao)
                {
                    CustoTotalFuncProducao += Convert.ToDecimal(func.DespesaTotal);
                    MinutosTotalFuncProducao += Convert.ToInt32(func.MinutosDia);
                    QtdFuncProducao += 1;

                }
                else
                {
                    CustoTotalFuncExtras += Convert.ToDecimal(func.DespesaTotal);
                    MinutosTotalFuncExtras += Convert.ToInt32(func.MinutosDia);
                    QtdFuncExtras += 1;
                }
            }

            //verifica se o custo da empresa com % de funcionários é fixo ou controlado pela empresa
            if (!DadosEmpresa.calcperauto)
            {
                Aproveitamento = DadosEmpresa.aproveitamentoMan;
                Eficiencia = DadosEmpresa.eficienciaMan;
                Presenca = DadosEmpresa.presencaMan;
            }
            else
            {
                Aproveitamento = DadosEmpresa.aproveitamentoAut;
                Eficiencia = DadosEmpresa.eficienciaAut;
                Presenca = DadosEmpresa.presencaAut;
            }

            decimal MinProd = (MinutosTotalFuncProducao * (Aproveitamento / 100) * (Eficiencia / 100) * (Presenca / 100));
            MinProd = MinProd == 0 ? 1 : MinProd;

            if (!DadosEmpresa.fixarcustosfunc)
            {
                CustoMinutoMaoObra = ((CustoTotalFuncProducao + CustoTotalFuncExtras) / DadosEmpresa.DiasUteis)
                                   / MinProd;
            }
            else
            {
                CustoMinutoMaoObra = DadosEmpresa.despfixfuncionario;
            }


        }

        public void RetornaInformacaoDespesas(string Mes, string Ano, ref decimal CustoMinutoPrevisto, ref decimal MediaMensalPrevista,
                                              ref decimal CustoMinutoRealizado, ref decimal MediaMensalRealizada)
        {
            int MesAtual = 0;
            int AnoCorrente = 0;
            int CodAno = 0;


            MesAtual = int.Parse(Mes);
            AnoCorrente = int.Parse(Ano);

            var DadosEmpresa = new EmpresaAcessoRepository().GetById(VestilloSession.EmpresaLogada.Id);
            var DadosPercEmpresa = new PercentuaisEmpresasRepository().GetEmpresaLogada(VestilloSession.EmpresaLogada.Id);
            var DadosAnoDespesa = new DespesaFixaVariavelRepository().GetByAno(AnoCorrente);

            if (DadosAnoDespesa != null)
            {
                CodAno = DadosAnoDespesa.Id;
            }




            int QtdFuncProducao = 0;
            int QtdFuncExtras = 0;
            int MinutosTotalFuncProducao = 0;
            int MinutosTotalFuncExtras = 0;
            decimal CustoTotalFuncProducao = 0;
            decimal CustoTotalFuncExtras = 0;
            decimal Aproveitamento = 0;
            decimal Presenca = 0;
            decimal Eficiencia = 0;
            decimal CustoMinutoMaoObra = 0;
            CustoMinutoPrevisto = 0;
            CustoMinutoRealizado = 0;
            MediaMensalPrevista = 0;
            MediaMensalRealizada = 0;


            var DespesasDoAno = new DespesaFixaVariavelMesRepository().GetByDespesaFixaVariavel(CodAno);

            if (DespesasDoAno != null)
            {
                MediaMensalPrevista = DespesasDoAno.Select(x => x.ValorPrevisto).Sum() / 12;

                MediaMensalRealizada = DespesasDoAno.Select(x => x.ValorRealizado).Sum() / (MesAtual == 1 ? 1 : MesAtual - 1);
            }


            RetornaInformacaoMaoDeObra(ref  QtdFuncProducao, ref  QtdFuncExtras, ref  MinutosTotalFuncProducao,
                                           ref  MinutosTotalFuncExtras, ref  CustoTotalFuncProducao, ref  CustoTotalFuncExtras,
                                            ref Aproveitamento, ref  Presenca, ref  Eficiencia, ref  CustoMinutoMaoObra);

            decimal MinProd = (MinutosTotalFuncProducao * (Aproveitamento / 100) * (Eficiencia / 100) * (Presenca / 100));
            MinProd = MinProd == 0 ? 1 : MinProd;



            if (DadosPercEmpresa.fixarcustosprev == true)
            {
                CustoMinutoPrevisto = DadosPercEmpresa.despfixvarprevista;
            }
            else
            {
                CustoMinutoPrevisto = (MediaMensalPrevista / DadosPercEmpresa.DiasUteis) / MinProd;
            }


            if (DadosPercEmpresa.fixarcustosreal == true)
            {
                CustoMinutoRealizado = DadosPercEmpresa.despfixvarrealizada;
            }
            else
            {
                CustoMinutoRealizado = (MediaMensalRealizada / 20) / MinProd;
            }




        }

        public List<ProducaoEmpresa> GetByDataProducao(DateTime daData, DateTime ateData)
        {

            List<ProducaoEmpresa> producoesEmpresa = new List<ProducaoEmpresa>();

            for (var date = daData.Date; date.Date <= ateData.Date; date = date.AddDays(1))
            {
                var producaoEmpresa = new ProducaoEmpresa();
                producaoEmpresa.Data = date;
                TempoFuncionarioRepository tempoFuncionarioRepository = new TempoFuncionarioRepository();
                var tempo = tempoFuncionarioRepository.GetByData(date);
                if (tempo != null)
                {
                    producaoEmpresa.Tempo = tempo.Tempo;
                }

                OcorrenciaFuncionarioRepository ocorrenciaFuncionarioRepository = new OcorrenciaFuncionarioRepository();
                producaoEmpresa.Ocorrencias = ocorrenciaFuncionarioRepository.GetByData(date);

                OperacaoOperadoraRepository operacaoOperadoraRepository = new OperacaoOperadoraRepository();
                var operacao = operacaoOperadoraRepository.GetByData(date);
                if (operacao != null)
                {
                    producaoEmpresa.Operacao = operacao.Tempo;
                }

                ProdutividadeRepository produtividadeRepository = new ProdutividadeRepository();
                var produtividade = produtividadeRepository.GetByData(date);
                if (produtividade != null)
                {
                    producaoEmpresa.Jornada = produtividade.Jornada;
                    producaoEmpresa.Produtividade = produtividade.Tempo ;
                }

                var pacotes = new PacoteProducaoRepository().GetByData(date);
                if (pacotes != null)
                {
                    producaoEmpresa.Defeito = pacotes.Sum(p => p.QtdDefeito);
                    producaoEmpresa.Pecas = pacotes.Sum(p => p.Quantidade) - pacotes.Sum(p => p.QtdDefeito);
                    producaoEmpresa.TempoPacote = pacotes.Sum(p => p.TempoPacote);
                }

                if (producaoEmpresa.Operacao != 0 || producaoEmpresa.Tempo != 0 || producaoEmpresa.Produtividade != 0 || producaoEmpresa.Jornada != 0
                    || (producaoEmpresa.Ocorrencias != null && producaoEmpresa.Ocorrencias.Count() > 0) || producaoEmpresa.Defeito != 0 || producaoEmpresa.Pecas != 0)
                {
                    producoesEmpresa.Add(producaoEmpresa);
                }
                 
            }

            return producoesEmpresa;
        }

        public PercentuaisEmpresaAuto GetByProducaoEmpresa()
        {


            var producaoEmpresa = new ProducaoEmpresa();
            var percentuaisEmpresaAuto = new PercentuaisEmpresaAuto();
            DateTime hoje = DateTime.Today;
            DateTime diaReferencia = DateTime.Today.AddDays(-VestilloSession.DiasPercentuaisEmpresa);

            producaoEmpresa.Data = hoje;

            TempoFuncionarioRepository tempoFuncionarioRepository = new TempoFuncionarioRepository();
            var tempo = tempoFuncionarioRepository.GetByDatas(diaReferencia, hoje);
            if (tempo != null)
            {
                producaoEmpresa.Tempo = tempo.Tempo;
            }

            OcorrenciaFuncionarioRepository ocorrenciaFuncionarioRepository = new OcorrenciaFuncionarioRepository();
            producaoEmpresa.Ocorrencias = ocorrenciaFuncionarioRepository.GetByDatas(diaReferencia, hoje);

            OperacaoOperadoraRepository operacaoOperadoraRepository = new OperacaoOperadoraRepository();
            var operacao = operacaoOperadoraRepository.GetByDatas(diaReferencia, hoje);
            if (operacao != null)
            {
                producaoEmpresa.Operacao = operacao.Tempo;
            }

            ProdutividadeRepository produtividadeRepository = new ProdutividadeRepository();
            var produtividade = produtividadeRepository.GetByDatas(diaReferencia, hoje);
            if (produtividade != null)
            {
                producaoEmpresa.Jornada = produtividade.Jornada;
                producaoEmpresa.Produtividade = produtividade.Tempo;
            }


            decimal tempoUtil = 0;
            decimal ocorrencia = 0;
            decimal desconto = 0;
            decimal punicao = 0;
            decimal acrescimo = 0;
            decimal produtividadete = 0;
            decimal jornada = 0;

            tempoUtil = producaoEmpresa.Jornada;
            jornada = producaoEmpresa.Jornada;
            produtividadete += producaoEmpresa.Tempo;
            produtividadete += producaoEmpresa.Operacao;

            foreach (var ocorencia in producaoEmpresa.Ocorrencias)
            {
                switch (ocorencia.OcorrenciaTipo)
                {
                    case 0:
                        tempoUtil -= ocorencia.Tempo;
                        ocorrencia += ocorencia.Tempo;
                        break;
                    case 1:
                        tempoUtil -= ocorencia.Tempo;
                        desconto += ocorencia.Tempo;
                        break;
                    case 2:
                        produtividadete -= ocorencia.Tempo;
                        punicao += ocorencia.Tempo;
                        break;
                    case 3:
                        tempoUtil += ocorencia.Tempo;
                        acrescimo += ocorencia.Tempo;
                        break;
                }
            }

            if (tempoUtil > 0)
            {
                percentuaisEmpresaAuto.Eficiencia = (produtividadete * 100 / tempoUtil);
            }

            if (jornada > 0)
            {
                percentuaisEmpresaAuto.Aproveitamento = ((jornada - ocorrencia) * 100 / jornada);
                percentuaisEmpresaAuto.Presenca = ((jornada - desconto) * 100 / jornada);
            }



            return percentuaisEmpresaAuto;
        }

        public IEnumerable<Empresa> GetBySelecao()
        {
            using (EmpresaRepository repository = new EmpresaRepository())
            {
                return repository.GetBySelecao();
            }
        }

        public Endereco GetEndereco(int IdEmpresa)
        {
            using (EmpresaRepository repository = new EmpresaRepository())
            {
                return repository.GetEndereco(IdEmpresa);
            }
        }


    }
}
