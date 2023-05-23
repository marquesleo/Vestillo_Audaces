using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Core.Models
{
    [Tabela("Atividades")]
    public class AtividadeView : Atividade
    {
        [NaoMapeado]
        [GridColumn("Nome Cliente")]
        public string NomeCliente { get; set; }
        [NaoMapeado]
        [GridColumn("Nome Vendedor")]
        public string NomeVendedor { get; set; }
        [NaoMapeado]
        public string NomeUsuarioCriacao { get; set; }
        [NaoMapeado]
        [GridColumn("Tipo Atividade")]
        public string DescricaoTipoAtividade { get; set; }
        [NaoMapeado]
        public string NomeUsuarioAlerta { get; set; }
        [NaoMapeado]
        public int  IdCliente { get; set; }
        
        [GridColumn("Ref Titulo")]
        public string RefTitulo { get; set; }
        
        [GridColumn("Valor do Título")]
        public decimal ValorTitulo { get; set; }

        [NaoMapeado]
        public string DescricaoStatus
        {
            get
            {
                switch (this.StatusAtividade)
                {
                    case EnumStatusAtividade.Incluido:
                        return "Incluído";
                    case EnumStatusAtividade.Concluido:
                        return "Concluído";
                    default:
                        return "";
                }

            }
        }
        [NaoMapeado]
        public string StatusAlerta
        {
            get
            {
                if (AlertarUsuario)
                {
                    if (DataExibicaoAlerta.GetValueOrDefault() == DateTime.MinValue)
                    {
                        return "Pendente";
                    }
                    else
                    {
                        return "Exibido";
                    }
                }

                return "";

            }
        }
    }
}
