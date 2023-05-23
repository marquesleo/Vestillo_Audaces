using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Repositories;

namespace Vestillo.Core.Services
{
    public class AtividadeService : GenericService<Atividade, AtividadeRepository>
    {
        public void Save(IEnumerable<AtividadeView> atividades, IEnumerable<AtividadeView> atividadesExcluidas = null)
        {
            bool openTransaction = false;

            try
            {
                openTransaction = _repository.BeginTransaction();

                foreach (AtividadeView view in atividades)
                {
                    Atividade atividade = (view as Atividade);
                    base.Save(atividade);
                }

                foreach (AtividadeView view in atividadesExcluidas)
                {
                    _repository.Delete(view.Id);
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
        public IEnumerable<AtividadeView> ListByColaboradorETipoAtividadeCliente(int colaborador, Atividade.EnumTipoAtividadeCliente tipoAtividadeCliente)
        {
            return _repository.ListByColaboradorETipoAtividadeCliente(colaborador, tipoAtividadeCliente);
        }

        public IEnumerable<AtividadeView> ListByVendedor(int vendedorId)
        {
            return _repository.ListByVendedor(vendedorId);
        }

        public IEnumerable<AtividadeView> ListByUsuarioVendedor(int userId, bool somenteComAlertaPendente = false)
        {
            return _repository.ListByUsuarioVendedor(userId, somenteComAlertaPendente);
        }

        public IEnumerable<AtividadeView> ListByVendedores(int[] vendedores, bool somenteComAlertaPendente = false)
        {
            return _repository.ListByVendedores(vendedores, somenteComAlertaPendente);
        }

        public IEnumerable<AtividadeView> ListByVendedoresCobranca(int[] vendedores)
        {
            return _repository.ListByVendedoresCobranca(vendedores);
        }

        public IEnumerable<AtividadeView> ListByVendedoresCobrancaDash(int[] vendedores)
        {
            return _repository.ListByVendedoresCobrancaDash(vendedores);
        }

        public IEnumerable<AtividadeView> ListAlertas(int usuarioId)
        {
            return _repository.ListAlertas(usuarioId);
        }

        public IEnumerable<AtividadeView> ListByVendedoresAgendaDash(int[] vendedores, bool somenteComAlertaPendente = false)
        {
            return _repository.ListByVendedoresAgendaDash(vendedores, somenteComAlertaPendente);
        }

        public IEnumerable<ListagemAtividadesView> ListByAtividadesCobranca(DateTime DataInicio, DateTime DataFim)
        {
            return _repository.ListByAtividadesCobranca(DataInicio, DataFim);
        }

        public IEnumerable<ListagemItensBling> ListaItensBling(List<int> Produtos)
        {
            return _repository.ListaItensBling(Produtos);
        }

    }
}

