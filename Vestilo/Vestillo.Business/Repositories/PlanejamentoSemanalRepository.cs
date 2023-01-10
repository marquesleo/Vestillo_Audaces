using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;
using Vestillo.Lib;
using Vestillo.Business.Models.Views;
using Vestillo;
using Vestillo.Core.Models;
using Vestillo.Business.Service;

namespace Vestillo.Business.Repositories
{
    public class PlanejamentoSemanalRepository : GenericRepository<PlanejamentoSemanal>
    {
        public PlanejamentoSemanalRepository() : base(new DapperConnection<PlanejamentoSemanal>())
        {

        }

        public IEnumerable<PlanejamentoSemanal> GetByRefView(string referencia)
        {
            var cn = new DapperConnection<PlanejamentoSemanal>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	* ");            
            SQL.AppendLine("FROM 	planejamento");            
            SQL.AppendLine("WHERE planejamento.referencia like '%" + referencia + "%'");


            SQL.Append(" AND " + FiltroEmpresa("planejamento.EmpresaId"));


            return cn.ExecuteStringSqlToList(new PlanejamentoSemanal(), SQL.ToString());
        }



        public IEnumerable<BalanceamentoSemanalItensView> GetDadosDasOps(int IdPlanejamento, decimal Aproveitamento, decimal Eficiencia, decimal Presenca)
        {
            decimal TempoTotal = 0;
            var cn = new DapperConnection<PlanejamentoSemanal>();
            var cnItens = new DapperConnection<PlanejamentoItens>();
            var cnFicha = new DapperConnection<FichaTecnica>();
            var cnPacotes = new DapperConnection<PacoteProducao>();

            IEnumerable<ItemOrdemProducaoView> DadosItensOps;



            List<BalanceamentoSemanalItensView> dadosDeRetrorno = null;
            dadosDeRetrorno = new List<BalanceamentoSemanalItensView>();
            dadosDeRetrorno.Clear();
            BalanceamentoSemanalItensView it = new BalanceamentoSemanalItensView();

            List<Setores> setores;
            List<BalanceamentoSemanalItensView> balanceamentosProducao = new List<BalanceamentoSemanalItensView>();
            var setoresService = new SetoresService().GetServiceFactory();
            setores = setoresService.GetByAtivosBalanceamento(1).ToList();
            balanceamentosProducao = new List<BalanceamentoSemanalItensView>();

            BalanceamentoSemanalItensView balanceamentoProd = null;
            foreach (var setor in setores)
            {
                balanceamentoProd = new BalanceamentoSemanalItensView();
                balanceamentoProd.Setor = setor.Abreviatura;
                balanceamentoProd.SetorId = setor.Id;
                balanceamentoProd.Aproveitamento = Aproveitamento;
                balanceamentoProd.Eficiencia = Eficiencia;
                balanceamentoProd.Presenca = Presenca;
                balanceamentosProducao.Add(balanceamentoProd);
            }


            string SQL = String.Empty;

            SQL = "select * from planejamentoitens where planejamentoitens.PlanejamentoId = " + IdPlanejamento;

            var DadosItensPlan = cnItens.ExecuteStringSqlToList(new PlanejamentoItens(), SQL.ToString());

            if(DadosItensPlan.Count() > 0)
            {
                decimal temposemanal = TempoSemanal(IdPlanejamento);

                var cnItensDasOps = new DapperConnection<ItemOrdemProducao>();
                foreach (var item in DadosItensPlan)
                {

                    string[] IdsOps = item.OrdensIds.Split(';');

                    foreach (var IdOP in IdsOps)
                    {
                        using (ItemOrdemProducaoRepository ItOp = new ItemOrdemProducaoRepository())
                        {
                            DadosItensOps = ItOp.GetByOrdemBalanco(int.Parse(IdOP));
                        }

                           
                        if (DadosItensOps.Count() > 0)
                        {
                            foreach (var itensOp in DadosItensOps)
                            {
                                decimal TempoPacote = 0;
                                decimal QtdPacote = 0;
                                decimal QtdDivPacote = 0;
                                decimal TempoParaSomar = 0;
                                if (itensOp.Status == 0 || itensOp.Status == 1)
                                {
                                    string SqlFicha = " SELECT * FROM fichatecnica WHERE fichatecnica.ProdutoId = " + itensOp.ProdutoId;

                                    var ficha = new FichaTecnica();
                                    cnFicha.ExecuteToModel(ref ficha, SqlFicha);
                                    if(ficha != null)
                                    {
                                        var prod = new ProdutoService().GetServiceFactory().GetById(itensOp.ProdutoId);
                                        TempoPacote = prod.TempoPacote;
                                        QtdPacote = prod.QtdPacote;
                                        QtdDivPacote = itensOp.Quantidade /  QtdPacote;
                                        TempoParaSomar = QtdDivPacote * TempoPacote;


                                        var OperaFicha = new FichaTecnicaOperacaoService().GetServiceFactory().GetByFichaTecnica(ficha.Id);
                                        foreach (var itemOperaFicha in OperaFicha)
                                        {

                                            var FIltroBalanco = balanceamentosProducao.Where(x => x.SetorId == itemOperaFicha.BalanceamentoId).FirstOrDefault();
                                            if(FIltroBalanco != null)
                                            {
                                                if (itemOperaFicha.TempoCronometrado > 0)
                                                {
                                                    TempoTotal = itensOp.Quantidade * Convert.ToDecimal(itemOperaFicha.TempoCronometrado);
                                                }
                                                else
                                                {
                                                    TempoTotal = itensOp.Quantidade * Convert.ToDecimal(itemOperaFicha.TempoCalculado);
                                                }

                                                FIltroBalanco.PessoasTrabalhando = 1;
                                                FIltroBalanco.TempoNecessario += TempoTotal + TempoParaSomar;
                                                FIltroBalanco.JornadaSemanal = temposemanal;
                                            }
                                            else
                                            {
                                                var setore = new SetoresService().GetServiceFactory().GetById(itemOperaFicha.BalanceamentoId);
                                                balanceamentoProd = new BalanceamentoSemanalItensView();
                                                balanceamentoProd.BalanceamentoId = 0;
                                                balanceamentoProd.Id = 0;
                                                balanceamentoProd.Setor = setore.Abreviatura;
                                                balanceamentoProd.SetorId = itemOperaFicha.BalanceamentoId;
                                                if (itemOperaFicha.TempoCronometrado > 0)
                                                {
                                                    TempoTotal = itensOp.Quantidade * Convert.ToDecimal(itemOperaFicha.TempoCronometrado);
                                                }
                                                else
                                                {
                                                    TempoTotal = itensOp.Quantidade * Convert.ToDecimal(itemOperaFicha.TempoCalculado);
                                                }

                                                FIltroBalanco.PessoasTrabalhando = 1;
                                                balanceamentoProd.TempoNecessario = TempoTotal + TempoParaSomar;
                                                balanceamentoProd.JornadaSemanal = temposemanal;
                                                balanceamentosProducao.Add(balanceamentoProd);
                                            }
                                            
                                        }
                                        

                                    }
                                }
                                else
                                {

                                    var pcts = new PacoteProducaoService().GetServiceFactory().GetByOrdemIdView(itensOp.OrdemProducaoId).Where(x => x.DataSaida == null);
                                    if(pcts != null)
                                    {
                                        foreach (var itemPacotes in pcts)
                                        {                                            
                                            using (GrupoOperacoesRepository DadosPct = new GrupoOperacoesRepository())
                                            {
                                                var Opera = DadosPct.GetListByGrupoPacoteVisualizar(itemPacotes.GrupoPacoteId,itemPacotes.Id).Where(x => x.DataConclusao == null);
                                                if(Opera != null)
                                                {
                                                    foreach (var itemOpera in Opera)
                                                    {
                                                        var FIltroBalanco = balanceamentosProducao.Where(x => x.SetorId == itemOpera.BalanceamentoId).FirstOrDefault();
                                                        if (FIltroBalanco != null)
                                                        {

                                                            TempoTotal = itemOpera.TempoTotal;
                                                            

                                                            FIltroBalanco.PessoasTrabalhando = 1;
                                                            FIltroBalanco.TempoNecessario += TempoTotal;
                                                            FIltroBalanco.JornadaSemanal = temposemanal;
                                                        }
                                                        else
                                                        {
                                                            var setore = new SetoresService().GetServiceFactory().GetById(itemOpera.BalanceamentoId);
                                                            balanceamentoProd = new BalanceamentoSemanalItensView();
                                                            balanceamentoProd.BalanceamentoId = 0;
                                                            balanceamentoProd.Id = 0;
                                                            balanceamentoProd.Setor = setore.Abreviatura;
                                                            balanceamentoProd.SetorId = itemOpera.BalanceamentoId;
                                                            
                                                            TempoTotal = itemOpera.TempoTotal;
                                                            

                                                            FIltroBalanco.PessoasTrabalhando = 1;
                                                            balanceamentoProd.TempoNecessario = TempoTotal;
                                                            balanceamentoProd.JornadaSemanal = temposemanal;
                                                            balanceamentosProducao.Add(balanceamentoProd);
                                                        }
                                                    }
                                                }
                                            }
                                                
                                        }
                                    }
                                    

                                }

                            }
                        }


                    }                    
                }
            }

           
            return balanceamentosProducao;// cn.ExecuteStringSqlToList(new BalanceamentoSemanalItensView(), SQL.ToString());
        }

        public decimal TempoSemanal(int IdPlan)
        {
            var cnItens = new DapperConnection<PlanejamentoItens>();
            decimal SemanaTotal = 0;
            string SQL = String.Empty;

            SQL = "select * from planejamentoitens where planejamentoitens.PlanejamentoId = " + IdPlan;
                        
            var DadosItensPlan = cnItens.ExecuteStringSqlToList(new PlanejamentoItens(), SQL.ToString());

            foreach (var item in DadosItensPlan)
            {
                SemanaTotal = item.TempoSemana;
            }

            return SemanaTotal;

        }

        public int ExisteBalanco(int PlanejamentoId)
        {
            int existe = 0;
            var cnBalanceamento = new DapperConnection<BalanceamentoSemanal>();            
            string SQL = String.Empty;

            SQL = "select * from balanceamentosemanal where balanceamentosemanal.IdPlanejamento = " + PlanejamentoId;

            var DadosItensPlan = cnBalanceamento.ExecuteStringSqlToList(new BalanceamentoSemanal(), SQL.ToString());

           if(DadosItensPlan != null && DadosItensPlan.Count() > 0)
           {
                foreach (var item in DadosItensPlan)
                {
                    existe = item.Id;
                }                
           }

            return existe;
        }

    }
}
