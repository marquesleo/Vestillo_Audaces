using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("Ocorrencias", "Ocorrências")]
    public class Ocorrencia
    {
        [Vestillo.Chave]
        public int Id { get; set; }   
        [Vestillo.RegistroUnico]
        public string Abreviatura { get; set; }
        [Vestillo.RegistroUnico]
        public string Descricao { get; set; }
        public int Tipo { get; set; }

        [Vestillo.NaoMapeado]
        public string DescTipo
        {
            get
            {
                switch (Tipo)
                {
                    case 0:
                        return "Ocorrência - Desconto em tempo útil";
                    case 1:
                        return "Desconto em tempo útil";
                    case 2:
                        return "Desconto em produtividade";
                    case 3:
                        return "Acréscimo em tempo útil";
                    case 4:
                        return "Ocorrência sem perda";
                    default:
                        return "Sem tipo selecionado";
                }
            }
        }
    }
}
