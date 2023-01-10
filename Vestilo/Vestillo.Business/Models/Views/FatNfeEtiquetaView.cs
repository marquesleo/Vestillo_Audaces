
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class FatNfeEtiquetaView
    {
       
        public int Id { get; set; }        
        public int IdEmpresa { get; set; }             
        public string Linha1 { get; set; } // Escrever Destinatario Ou Remetente
        public string Linha2 { get; set; }  // Nome da Empresa ou Nome do cliente      
        public string Linha3 { get; set; }  // Endereço e o número da empresa ou do cliente     
        public string Linha4 { get; set; } // Bairro, cidade e estado, da empresa ou do cliente
        public string Linha5 { get; set; } // Cep da empresa ou do Cliente
        public string Linha6 { get; set; } // Escrever destinatario ou Transportadora
        public string Linha7 { get; set; } //Nome do Cliente ou da transportadora
        public string Linha8 { get; set; } // Endereço e número do cliente ou pegar volumes da nota 
        public string Linha9 { get; set; } // Bairro, cidade e estado do cliente ou Numero da nota fiscal
        public string Linha10 { get; set; } //Cep do cliente
        public string Linha11 { get; set; } // Escrever, transportado por: Empresa Brasileira de correios e telg. SEDEX      
    }
}
