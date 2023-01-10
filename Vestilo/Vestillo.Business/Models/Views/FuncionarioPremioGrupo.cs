
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FuncionarioPremioGrupo
    {
        public int CodFuncionario { get; set; }
        public string NomeFuncionario { get; set; }
        public string RefFuncionario { get; set; }
        public decimal Eficiencia { get; set; }
        public decimal Aproveitamento { get; set; }
        public decimal Presenca { get; set; }
        public decimal Jornada { get; set; }
        public decimal Ocorrencia { get; set; }
        public decimal Falta { get; set; }
        public decimal Extra { get; set; }
        public decimal MinutosProduzidos { get; set; }
        public decimal MinutosUtil { get; set; }
        public int PremioId { get; set; }
        public string PremioReferencia { get; set; }
        public string PremioDescricao { get; set; }      
        public int GruTipo { get; set; }
        public decimal GruMinimo { get; set; }
        public decimal GruMaximo { get; set; }
        public decimal GruValor { get; set; }
        public decimal GruValPartida { get; set; }
        public decimal PremioDia { get; set; }
        public decimal PremioMes { get; set; }
        public decimal BHDebito { get; set; }
        public decimal Punicao { get; set; }
        public decimal Producao { get; set; }
        public int QtdFunc { get; set; }
        public DateTime? ProdutividadeData { get; set; }
    }
}
