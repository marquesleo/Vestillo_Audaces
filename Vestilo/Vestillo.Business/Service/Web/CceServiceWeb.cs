
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class CceServiceWeb : GenericServiceWeb<Cce, CceRepository, CceController>, ICceService
    {
        public CceServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public int MaiorSeqNota(int Nota)
        {
            var c = new ConnectionWebAPI<Cce>(VestilloSession.UrlWebAPI);
            var teste =  c.Get(this.RequestUri, "idNotaFiscal=" + Nota);

            return Convert.ToInt32(teste);
        }

        public IEnumerable<Cce> DadosDaUltimaCarta(int Nota, int seq)
        {
            var SQL = new Select()
                    .Campos("*")
                    .From("colaboradores")
                    .Where("idNotaFiscal = " + Nota + " AND " + seq );
            var c = new ConnectionWebAPI<IEnumerable<Cce>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, SQL.ToString());   
        }


    }
}
