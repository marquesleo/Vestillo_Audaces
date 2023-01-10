using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class LogView
    {
        
        public int Id { get; set; }        
        public int EmpresaId { get; set; }
        public int UsuarioId { get; set; }
        public string NomeUsuario { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataAcao { get; set; }
        public int Operacao { get; set; }
        public string DescricaAcao
        {
            get
            {
                switch (this.Operacao)
                {
                    case 1:
                        return "Inclusão";
                    case 2:
                        return "Alteração";
                    case 3:
                        return "Exclusão";
                    default:
                        return "";
                }
            }
        }
        public string Modulo { get; set; }
        public string DescricaoOperacao { get; set; }
        public int ObjetoId { get; set; }
        public string Objeto { get; set; }
        public string Hora { get; set; }
        
    }
}
