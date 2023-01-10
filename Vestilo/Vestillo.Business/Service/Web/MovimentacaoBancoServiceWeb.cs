
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Lib;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Service.Web
{
    public class MovimentacaoBancoServiceWeb : GenericServiceWeb<MovimentacaoBanco, MovimentacaoBancoRepository, MovimentacaoBancoController>, IMovimentacaoBancoService
    {
        

        public MovimentacaoBancoServiceWeb(string requestUri): base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<MovimentacaoBancoView> GetCamposBrowse()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MovimentacaoBancoView> GetRelExtratoBancarioBrowse(FiltroExtratoBancarioRel filtro)
        {

            throw new NotImplementedException();
        }

    }
}