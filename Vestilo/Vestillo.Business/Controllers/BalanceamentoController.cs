using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class BalanceamentoController : GenericController<Balanceamento, BalanceamentoRepository>
    {

        public Balanceamento GetByIdView(int id)
        {
            Balanceamento balanceamento = _repository.GetById(id);

            balanceamento.BalanceamentosProducao = new BalanceamentoProducaoRepository().GetByBalanceamento(balanceamento.Id).ToList();
            balanceamento.BalanceamentosProduto = new BalanceamentoProdutoRepository().GetByBalanceamento(balanceamento.Id).ToList();
            return balanceamento;  
        }

        public override void Save(ref Balanceamento balanceamento)
        {
            _repository.BeginTransaction();
            try
            {
                base.Save(ref balanceamento);
                var balanceamentoId = balanceamento.Id;

                if (balanceamento.BalanceamentosProducao != null && balanceamento.BalanceamentosProducao.Count > 0)
                {
                    foreach (var balanceamentoProducao in balanceamento.BalanceamentosProducao)
                    {
                        BalanceamentoProducao balanceamentoProducaoSave = new BalanceamentoProducao();
                        balanceamentoProducaoSave = balanceamentoProducao;
                        balanceamentoProducaoSave.BalanceamentoId = balanceamento.Id;
                        new BalanceamentoProducaoRepository().Save(ref balanceamentoProducaoSave);
                    }
                }

                if (balanceamento.BalanceamentosProduto != null && balanceamento.BalanceamentosProduto.Count > 0)
                {
                    foreach (var balanceamentoProduto in balanceamento.BalanceamentosProduto)
                    {
                        if (balanceamentoProduto.Quantidade == 0 || balanceamentoProduto.Quantidade == null)
                        {
                            new BalanceamentoProdutoRepository().Delete(balanceamentoProduto.Id);
                        }
                        else
                        {
                            BalanceamentoProduto balanceamentoProdutoSave = new BalanceamentoProduto();
                            balanceamentoProdutoSave = balanceamentoProduto;
                            balanceamentoProdutoSave.BalanceamentoId = balanceamento.Id;
                            new BalanceamentoProdutoRepository().Save(ref balanceamentoProdutoSave);
                        }
                    }
                } 
                
                _repository.CommitTransaction();
            }
            catch (Exception ex)
            {
                _repository.RollbackTransaction();
                throw ex;
            }
        }

        public override void Delete(int id)
        {
            bool openTransaction = false;
            try
            {
                openTransaction = _repository.BeginTransaction();
                var balanceamentosProducao = new BalanceamentoProducaoRepository().GetByBalanceamento(id).ToList();
                var balanceamentosProduto = new BalanceamentoProdutoRepository().GetByBalanceamento(id).ToList();

                if (balanceamentosProducao != null && balanceamentosProducao.Count() > 0)
                {
                    foreach (var balanceamentoProducao in balanceamentosProducao)
                    {
                        new BalanceamentoProducaoRepository().Delete(balanceamentoProducao.Id);
                    }
                }

                if (balanceamentosProduto != null && balanceamentosProduto.Count() > 0)
                {
                    foreach (var balanceamentoProduto in balanceamentosProduto)
                    {
                        new BalanceamentoProdutoRepository().Delete(balanceamentoProduto.Id);
                    }
                }

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
    }
}
