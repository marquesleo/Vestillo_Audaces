using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;
using Vestillo.Lib;

namespace Vestillo.Business.Repositories
{
    public class ImagemRepository : GenericRepository<Imagem>
    {
        public ImagemRepository()
            : base(new DapperConnection<Imagem>())
        {
        }

        public IEnumerable<Imagem> GetImagem(String tipo, int id)
        {
            
            var SQL = new Select()
                 .Campos("id, tipo, imagem")
                 .From("imagens")
                 .Where(tipo + " = " + id);

            var imagem = new Imagem();
            return _cn.ExecuteStringSqlToList(imagem, SQL.ToString());


        }

        public void ApagaImagens(String tipo, int id)
        {
            var sql = "delete from imagens where " + tipo + " = " + id;
            _cn.ExecuteNonQuery(sql);

        }
    }
}
