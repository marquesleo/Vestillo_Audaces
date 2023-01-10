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
    public class ImagemServiceWeb : GenericServiceWeb<Imagem, ImagemRepository, ImagemController>, IImagemService
    {

        public ImagemServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<Imagem> GetImagem(String tipo, int Id)
        {
            var c = new ConnectionWebAPI<IEnumerable<Imagem>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, tipo + " = " + Id.ToString());
        }
    }
}
