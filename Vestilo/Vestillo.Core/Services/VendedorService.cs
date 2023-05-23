using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Repositories;

namespace Vestillo.Core.Services
{
    public class VendedorService : GenericService<Vendedor, VendedorRepository>
    {
        public Vendedor FindByUsuario(int usuarioId)
        {
            return _repository.FindByUsuario(usuarioId);
        }

        public FunilDeVendasView ListDadosFunilDeVendas(int[] id, DateTime[] mesAno)
        {
            return _repository.ListDadosFunilDeVendas(id, mesAno);
        }

        public DashVendedorLead ListDadosClientesLead(int[] id, DateTime mesAno)
        {
            return _repository.ListDadosClientesLead(id, mesAno);
        }

        public DashVendedorCarteiraClientes ListDadosCarteira(int[] id)
        {
            return _repository.ListDadosCarteira(id);
        }

        public IEnumerable<ItenPorClienteView> ItensPorClientesNFe(List<int> ListaIdsProduto, DateTime DataInicio, DateTime DataFim, bool UnirEmpresas, int EmpresaLogada)
        {
            return _repository.ItensPorClientesNFe(ListaIdsProduto, DataInicio, DataFim, UnirEmpresas, EmpresaLogada);
        }

        public IEnumerable<ItenPorClienteView> ItensPorClientesNFCe(List<int> ListaIdsProduto, DateTime DataInicio, DateTime DataFim, bool UnirEmpresas, int EmpresaLogada)
        {
            return _repository.ItensPorClientesNFCe(ListaIdsProduto, DataInicio, DataFim, UnirEmpresas, EmpresaLogada);
        }
    
        public IEnumerable<ItenPorClienteView> ItensPorClientes(List<int> ListaIdsProduto, DateTime DataInicio, DateTime DataFim, bool UnirEmpresas, int EmpresaLogada)
        {
            return _repository.ItensPorClientes(ListaIdsProduto, DataInicio, DataFim, UnirEmpresas, EmpresaLogada);
        }

        public IEnumerable<CurvaAbcView> GetCurvaAbc(int[] id, DateTime[] mesAno, int tipo = 0)
        {
            return _repository.GetCurvaAbc(id, mesAno,tipo);
        }

        public decimal GetValorMetaRealizadaNoMes(int vendedorId, DateTime[] mesAno)
        {
            return _repository.GetValorMetaRealizadaNoMes(vendedorId, mesAno);
        }

        public decimal GetValorMetaNoMes(int vendedorId, DateTime[] mesAno)
        {
            return _repository.GetValorMetaNoMes(vendedorId, mesAno);
        }

        public decimal GetValorRealizadaNoMes(int vendedorId, DateTime[] mesAno)
        {
            return _repository.GetValorRealizadaNoMes(vendedorId, mesAno);
        }

        public decimal GetValorMetaRealizadaNoMesGeral(int[] id, DateTime[] mesAno)
        {
            return _repository.GetValorMetaRealizadaNoMesGeral(id, mesAno);
        }

        public decimal GetValorMetaNoMesGeral(int[] id, DateTime[] mesAno)
        {
            return _repository.GetValorMetaNoMesGeral(id, mesAno);
        }

        public decimal GetValorRealizadaNoMesGeral(int[] id, DateTime[] mesAno)
        {
            return _repository.GetValorRealizadaNoMesGeral(id, mesAno);
        }


        public IEnumerable<CurvaAbcItensView> GetCurvaAbcItens(int id, DateTime[] mesAno, int tipo = 0)
        {
            return _repository.GetCurvaAbcItens(id, mesAno,tipo);
        }
       
    }
}

