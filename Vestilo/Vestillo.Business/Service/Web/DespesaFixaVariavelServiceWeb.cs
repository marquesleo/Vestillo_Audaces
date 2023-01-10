
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class DespesaFixaVariavelServiceWeb : GenericServiceWeb<DespesaFixaVariavel, DespesaFixaVariavelRepository, DespesaFixaVariavelController>, IDespesaFixaVariavelService
    {

        public DespesaFixaVariavelServiceWeb(string requestUri)  : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public int GetUltimoAno()
        {
            throw new NotImplementedException();
        }

        public DespesaFixaVariavel GetByAno(int ano)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DespesaFixaVariavelMes> GetDespesasNaturezasByAno(int ano)
        {
            throw new NotImplementedException();
        }

        public void UpdateDespesaByNatureza(int idNatFinanceira, bool automatico)
        {
            throw new NotImplementedException();
        }

        public void UpdateTotaisDespesa(int despesaId)
        {
            throw new NotImplementedException();
        }
    }
}


