using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;

namespace Vestillo.Business.Service.APP
{
    public class ImagemServiceAPP: GenericServiceAPP<Imagem, ImagemRepository, ImagemController>, IImagemService
    {
        public ImagemServiceAPP()
            : base(new ImagemController())
        {
        }

        public IEnumerable<Imagem> GetImagem(String tipo, int Id)
        {
            ImagemController controller = new ImagemController();
            return controller.GetImagem(tipo, Id);
        }
        
    }
}
