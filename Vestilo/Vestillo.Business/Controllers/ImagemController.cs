using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;


namespace Vestillo.Business.Controllers
{
    public class ImagemController : GenericController<Imagem, ImagemRepository>
    {

        public IEnumerable<Imagem> GetImagem(String tipo, int Id)
        {
            using (ImagemRepository repository = new ImagemRepository())
            {
                return repository.GetImagem(tipo, Id);
            }
        }

        
    }
}
