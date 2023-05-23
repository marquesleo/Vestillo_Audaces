
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Repositories;

namespace Vestillo.Core.Services
{
    public class AtualizaLeitorDePrecoService : GenericService<AtualizaLeitorDePrecoView, AtualizaLeitorDePrecoRepository>
    {


        public IEnumerable<ItensDaTabelaDoLeitorView> ItensDaTabelaParaLeitor(int IdTabela)
        {
            return _repository.ItensDaTabelaParaLeitor(IdTabela);
        }

        public AtualizaLeitorDePrecoView RegistroDoLeitor()
        {
            return _repository.RegistroDoLeitor();
        }

        public void UpdateRegistroLeitor(int IdUsuario, int IdTabela, string Diretorio)
        {
            _repository.UpdateRegistroLeitor(IdUsuario, IdTabela, Diretorio);
        }

        public void DeletarDadosTabelaLeitor()
        {
            _repository.DeletarDadosTabelaLeitor();
        }

        public void AtualizaDadosLeitor(List<BuscaPreco> ListaItens)
        {
            _repository.AtualizaDadosLeitor(ListaItens);
        }
    }
}


