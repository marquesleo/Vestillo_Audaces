using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Interface;
using Vestillo.Business.Service.Web;


namespace Vestillo.Business.Service
{
    public class ImagemService : GenericService<Imagem, ImagemRepository, ImagemController>
    {
        public ImagemService()
        {
            base.RequestUri = "Imagem";
        }

        public new IImagemService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ImagemServiceWeb(this.RequestUri);
            }
            else
            {
                return new ImagemServiceAPP();
            } 
        }  
    }
}
