using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;
using Vestillo.Business.Service;


namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("fichatecnicadomaterial", "Ficha Tecnica Material")]
    public class FichaTecnicaDoMaterial
    {
        [Vestillo.Chave]

        public int Id { get; set; }

        [Vestillo.FiltroEmpresa]
        public int EmpresaId { get; set; }

        public int ProdutoId { get; set; }

        public bool Ativo { get; set; }

        public bool possuiquebra { get; set; }

        public string QuebraManual { get; set; }
       
        public Decimal Total { get; set; }

        [Vestillo.NaoMapeado]
        public byte[] imagem { get; set; }

        public string Observacao { get; set; }

        public DateTime? DataAlteracao { get; set; }
        public int? UserId { get; set; }

        private Produto _Produto;
        [Vestillo.NaoMapeado]
        public Produto getProduto
        {
            get
            {
                if (_Produto == null && ProdutoId > 0)
                {
                    _Produto = new Produto();
                    _Produto = new ProdutoService().GetServiceFactory().GetById(ProdutoId);
                }
                return _Produto;
            }
        }

    }
}
