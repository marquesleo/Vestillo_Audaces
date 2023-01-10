using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;
using Vestillo.Business.Service;


namespace Vestillo.Business.Models
{
     [Vestillo.Tabela("fichatecnicadomaterialitem", "Ficha Técnica do Material Item")]
    public class FichaTecnicaDoMaterialItem
    {

       [Vestillo.Chave]
        public int Id { get; set; }  
                                                                     
        public int FichaTecnicaId { get; set; }                                                                                     
        public int MateriaPrimaId { get; set; }
        public int DestinoId { get; set; }
        public decimal quantidade { get; set; }                  
        public  string excecao { get; set; }   
        public short sequencia { get; set; }   
        public decimal percentual_custo { get; set; } 
        public decimal preco { get; set; }
        public decimal valor { get; set; }
        public int idFornecedor { get; set; }

        public decimal retornarCalculoValor()
        {
                  
            if (percentual_custo == 100)
            {
                return quantidade * preco;
            }
            else
            {
              decimal perc_valor = (preco * percentual_custo) / 100;
               return perc_valor * quantidade;
            }
        }

        public decimal CustoCalculado
        {
            get
            {
                return retornarCalculoValor();
            }
        }


        private Produto _MateriaPrima;
        [Vestillo.NaoMapeado]
        public Produto getMateriaPrima
        {
            get
            {
                 if (_MateriaPrima == null && MateriaPrimaId > 0)
                    {
                        _MateriaPrima = new Produto();
                        _MateriaPrima = new ProdutoService().GetServiceFactory().GetById(MateriaPrimaId);
                        var unidade = new UniMedidaService().GetServiceFactory().GetById(_MateriaPrima.IdUniMedida);
                        if (unidade != null)
                        {
                            _MateriaPrima.UM = unidade.Abreviatura;
                        }
                    }
                 return _MateriaPrima;
            }
           
        }

    }
}
