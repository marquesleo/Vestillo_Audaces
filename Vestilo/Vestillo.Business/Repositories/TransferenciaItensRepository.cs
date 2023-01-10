
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;
using Vestillo.Lib;

namespace Vestillo.Business.Repositories
{

    public class TransferenciaItensRepository : GenericRepository<TransferenciaItens>
    {
        public TransferenciaItensRepository() : base(new DapperConnection<TransferenciaItens>())
        {
        }

        public IEnumerable<TransferenciaItens> GetListByItens(int Idtransferencia)
        {
            var SQL = new Select()
                .Campos("transferenciaitens.Id  as Id,transferenciaitens.Idtransferencia as Idtransferencia, transferenciaitens.* ")
                .From("transferenciaitens")
                .Where("Idtransferencia = " + Idtransferencia);

            var cn = new DapperConnection<TransferenciaItens>();
            var tm = new TransferenciaItens();
            return cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }       



        public IEnumerable<TransferenciaItensView> GetListByItensView(int Idtransferencia)
        {
            var SQL = new Select()
                .Campos(" produtos.Referencia as Referencia, produtos.descricao as Descricao,cores.descricao as Cor, " +
                        "tamanhos.descricao as Tamanho,transferenciaitens.quantidade   ")
                 .From(" transferenciaitens ")
                 .InnerJoin("produtos", "produtos.id =  transferenciaitens.iditem")
                .LeftJoin("cores", "cores.id =  transferenciaitens.idcor")
                .LeftJoin("produtodetalhes", "produtodetalhes.IdProduto = produtos.id AND produtodetalhes.idtamanho = transferenciaitens.idtamanho AND produtodetalhes.idcor = transferenciaitens.idcor")
                .LeftJoin("tamanhos", "tamanhos.id =  transferenciaitens.idtamanho")
                .Where("Idtransferencia = " + Idtransferencia);

            var cn = new DapperConnection<TransferenciaItensView>();
            var tm = new TransferenciaItensView();
            return cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }


    }





}
