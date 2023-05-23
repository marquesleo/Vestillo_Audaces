
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{
    
    public class AtualizaLeitorDePrecoView 
    {
        public int Id { get; set; }        
        public int  IdUsuario { get; set; }
        public int idtabelaPreco { get; set; }
        public DateTime DataCriacao { get; set; }
        public string TabReferencia { get; set; }
        public string TabDescricao { get; set; }
        public string UsuarioNome { get; set; }
        public string DiretorioArquivo { get; set; }
    }

}
