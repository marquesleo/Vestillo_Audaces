using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("PlanoAnualDetalhes")]
    public class PlanoAnualDetalhesView : PlanoAnualDetalhes
    {
        [NaoMapeado]
        public int MinTotais
        {
            get
            {
                return Convert.ToInt32(Costureira * DiasUteis * Jornada);
            }

         
        }
        [NaoMapeado]
        public int MinPresenca
        {
            get
            {
                return Convert.ToInt32(MinTotais * Presenca / 100);
            }
         
        }
        [NaoMapeado]
        public int MinUteis
        {
            get
            {
                return Convert.ToInt32(MinPresenca * Aproveitamento / 100);
            }
           
        }
        [NaoMapeado]
        public int MinProducao { 
            get 
            { 
                return Convert.ToInt32(MinUteis * Eficiencia / 100); 
            } 
             
        }
        [NaoMapeado]
        public int Pecas {
            get 
            {
                if (TempoMedio > 0)
                    return Convert.ToInt32(MinProducao / TempoMedio);
                else
                    return 0;
            }
            
        }

        [NaoMapeado]
        public string MesNome
        {
            get
            {
                switch (Mes)
                {
                    case 1:
                        return "Janeiro";
                    case 2:
                        return "Fevereiro";
                    case 3:
                        return "Março";
                    case 4:
                        return "Abril";
                    case 5:
                        return "Maio";
                    case 6:
                        return "Junho";
                    case 7:
                        return "Julho";
                    case 8:
                        return "Agosto";
                    case 9:
                        return "Setembro";
                    case 10:
                        return "Outubro";
                    case 11:
                        return "Novembro";
                    case 12:
                        return "Dezembro";
                    case 13:
                        return "Total";
                    case 14:
                        return "Média";
                    default:
                        return "";
                }
            }
        }
    }
}
