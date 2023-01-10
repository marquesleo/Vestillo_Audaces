using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Vestillo.Business.Models
{
    [Tabela("Usuarios", "Usuarios")]
    public class Usuario
    {
        [Chave]
        public int Id { get; set; }
        public string Nome { get; set; }
        [RegistroUnico]
        public string Login { get; set; }
        [RegistroUnico]
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataStatus { get; set; }
        public string Idioma { get; set; }
        public string Tema { get; set; }
        public bool SomentePontoDeVenda { get; set; }
        public bool ExibeDashBoardFinanceiro { get; set; }
        public int Arena { get; set; }


        [NaoMapeado]
        public IEnumerable<UsuarioGrupo> Grupos { get; set; }
        [NaoMapeado]
        public IEnumerable<UsuarioEmpresa> Empresas { get; set; }
        [NaoMapeado]
        public IEnumerable<UsuarioModulosSistema> Modulos { get; set; }
        
        public override string ToString()
        {            
            return string.Format("Id: {0} Nome: {1} Login: {2} Senha: {3}", Id, Nome, Login, Senha);
        }
    }
}