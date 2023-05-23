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
    public class NfceItensRepository : GenericRepository<NfceItens>
    {
         public NfceItensRepository()
            : base(new DapperConnection<NfceItens>())
        {
        }

         public IEnumerable<NfceItens> GetListByNfce(int IdNfce)
         {
             var SQL = new Select()
                 .Campos("produtos.descricao as produtodesc, cores.descricao as cordesc, tamanhos.descricao as tamanhodesc, nfceitens.* ")
                 .From("nfceitens")
                 .InnerJoin("produtos", "produtos.id =  nfceitens.idproduto")
                 .LeftJoin("cores", "cores.id =  nfceitens.idcor")
                 .LeftJoin("tamanhos", "tamanhos.id =  nfceitens.idtamanho")
                 .Where("idnfce = " + IdNfce)
                 .OrderBy("nfceitens.numitem");

             var cn = new DapperConnection<NfceItens>();
             var tm = new NfceItens();
             return cn.ExecuteStringSqlToList(tm, SQL.ToString());
         }


         public IEnumerable<NfceItensView> GetListViewItensNfce(int IdNfce, bool emissao = false)
         {
            string where = string.Empty;
            if (emissao)
                where = " AND nfceitens.Devolucao = 0";

             var SQL = new Select()
                 .Campos("produtodetalhes.codbarras as CodBarrasGrade, produtodetalhes.codbarras as CodBarrasUnico,  produtos.descricao as DescProduto, cores.descricao as DescCor, cores.Abreviatura as AbreviaturaCor,produtos.referencia as RefProduto, " +
                        "produtos.ncm as Ncm, produtos.cest as Cest, produtos.origem as Origem, unidademedidas.abreviatura as unidade, tamanhos.descricao as DescTamanho,  tamanhos.Abreviatura as AbreviaturaTamanho," +
                        "produtos.csosnnfce as Csosn, produtos.creditoicms as CreditoIcms, nfceitens.* ")
                 .From("nfceitens")
                 .InnerJoin("produtos", "produtos.id =  nfceitens.idproduto")
                 .InnerJoin("unidademedidas", "unidademedidas.id = produtos.IdUniMedida")
                 .LeftJoin("produtodetalhes", "produtodetalhes.IdProduto =  produtos.id And produtodetalhes.Idcor = nfceitens.idcor AND produtodetalhes.Idtamanho = nfceitens.idtamanho")
                 .LeftJoin("cores", "cores.id =  nfceitens.idcor")
                 .LeftJoin("tamanhos", "tamanhos.id =  nfceitens.idtamanho")
                 .Where("idnfce = " + IdNfce + where)
                 .OrderBy("numitem");

             var cn = new DapperConnection<NfceItensView>();
             var tm = new NfceItensView();
             return cn.ExecuteStringSqlToList(tm, SQL.ToString());
         }

         public IEnumerable<NfceItensView> GetListViewItensNfceAgrupado(int IdNfce, bool emissao = false)
         {
            string where = string.Empty;
            if (emissao)
                where = " AND nfceitens.Devolucao = 0";

            var SQL = new Select()
                 .Campos(" produtos.descricao as DescProduto, produtos.referencia as RefProduto, " +
                        "produtos.ncm as Ncm, produtos.cest as Cest, produtos.origem as Origem, unidademedidas.abreviatura as unidade,  " +
                        "produtos.csosnnfce as Csosn, produtos.creditoicms as CreditoIcms,sum(nfceitens.quantidade) as qtdSomada,SUM(nfceitens.DescValor) as DescValorSomado, SUM(nfceitens.TotalComDesconto) as totalSomado , nfceitens.* ")
                 .From("nfceitens")
                 .InnerJoin("produtos", "produtos.id =  nfceitens.idproduto")
                 .InnerJoin("unidademedidas", "unidademedidas.id = produtos.IdUniMedida")                 
                 .Where("idnfce = " + IdNfce + where)
                 .GroupBy("nfceitens.idproduto,nfceitens.Devolucao");

             var cn = new DapperConnection<NfceItensView>();
             var tm = new NfceItensView();
             return cn.ExecuteStringSqlToList(tm, SQL.ToString());
         }

    }
}
