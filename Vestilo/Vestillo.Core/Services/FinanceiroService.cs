using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Repositories;
using Vestillo.Core.Models;

namespace Vestillo.Core.Services
{
    public class FinanceiroService : GenericService<Titulo, FinanceiroRepository>
    {
        public IEnumerable<Titulo> ListParcelasEmAbertoPorVendedores(int[] vendedores)
        {
            return _repository.ListParcelasEmAbertoPorVendedores(vendedores);
        }

        public IEnumerable<Titulo> ListParcelasBaixadasHoje(int[] vendedores)
        {
            return _repository.ListParcelasBaixadasHoje(vendedores);
        }

        public IEnumerable<PedidoVendaDashFinanceiro> ListPedidoVendedoresParaDash(int[] vendedores)
        {
            return _repository.ListPedidoVendedoresParaDash(vendedores);
        }

        public DespesaXReceitaDashFinanceiro GetDespesaEReceita(int[] vendedores, DateTime data, bool considerarPenasMes = true)
        {
            return _repository.GetDespesaEReceita(vendedores, data, considerarPenasMes);
        }

        public DadosFaturamentoDashFinanceiro GetDadosFaturamentoDashFinanceiro(int[] vendedores)
        {
            return _repository.GetDadosFaturamentoDashFinanceiro(vendedores);
        }

        public IEnumerable<Titulo> ListParcelasEmProtestoPorVendedores(int[] vendedores)
        {
            return _repository.ListParcelasEmProtestoPorVendedores(vendedores);
        }

        public IEnumerable<Titulo> ListParcelasEmProtestoPorVendedoresRecuperados(int[] vendedores)
        {
            return _repository.ListParcelasEmProtestoPorVendedoresRecuperados(vendedores);
        }

        public IEnumerable<Titulo> ListParcelasRenegociados1A30(int[] vendedores)
        {
            return _repository.ListParcelasRenegociados1A30(vendedores);
        }

        public IEnumerable<Titulo> ListParcelasComAtividadeEQuitado(int[] vendedores)
        {
            return _repository.ListParcelasComAtividadeEQuitado(vendedores);
        }
    }
}
