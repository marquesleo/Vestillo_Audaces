using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Lib
{
    public class XMLFormBotao
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public int Imagem { get; set; }
        public bool RegistroValido { get; set; }
        public bool Confirmar { get; set; }
        public string MsgConfirmacao { get; set; }

        public bool FormRegistroAutomatico { get; set; }
        
        public override string ToString()
        {
            return string.Format("Id: {0} Label: {1} Imagem: {2} RegValido: {3} Confirmar: {4} MsgConfirmacao: {5} FormRegistroAutomatico: {6}", 
                Id, Label, Imagem, RegistroValido, Confirmar, MsgConfirmacao, FormRegistroAutomatico);
        }
    }
}
