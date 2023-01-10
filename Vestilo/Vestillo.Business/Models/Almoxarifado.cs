using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("almoxarifados", "almoxarifado")]    
    public class Almoxarifado
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }
        [RegistroUnico]
        public string Abreviatura { get; set; }
        [RegistroUnico]
        public string Descricao { get; set; }
        public int Tipo { get; set; }
        public int? IdColaborador { get; set; }
        public bool EnviarEcommerce { get; set; }
    }
}