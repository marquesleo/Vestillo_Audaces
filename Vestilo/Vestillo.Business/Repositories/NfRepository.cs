using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class NfRepository : GenericRepository<NfeEmitida>
    {
         string modificaWhere = "";

        public NfRepository()
             : base(new DapperConnection<NfeEmitida>())
        {
        }

        public IEnumerable<RankingVenda> GetRankingVenda(FiltroRankingVendaRelatorio filtro)
        {
            var SQL = new StringBuilder();

                SQL.AppendLine("select referencia, descricao, sum(quantidade) as quantidade, preco,sum(total) as total, ");
                SQL.AppendLine("cor, tamanho");
                SQL.AppendLine("from (");


            if (filtro.nfe)//se usa nfe
            {
                SQL.AppendLine("select produto.referencia, produto.descricao, (ifnull(nfei.quantidade, 0) - ifnull(nfei.Qtddevolvida,0)) as quantidade, nfei.preco, ((ifnull(nfei.quantidade, 0) - ifnull(nfei.Qtddevolvida, 0))*nfei.preco) as total, ");
                SQL.AppendLine("cores.Abreviatura as cor, tamanhos.Abreviatura as tamanho");
                SQL.AppendLine("from nfe");
                SQL.AppendLine("inner join nfeitens nfei on nfei.IdNfe = nfe.id");
                SQL.AppendLine("inner join produtos produto on nfei.iditem = produto.id");
                SQL.AppendLine("inner join cores on nfei.idcor = cores.id");
                SQL.AppendLine("inner join tamanhos on nfei.idtamanho = tamanhos.id");                
                SQL.AppendLine(" WHERE " + FiltroEmpresa("NFE.Idempresa"));
                SQL.AppendLine("AND ");


                if (filtro.SomenteEmitidos)
                {
                    SQL.AppendLine(" nfe.statusnota = 1");
                }
                else
                {
                    SQL.AppendLine(" nfe.statusnota <> 2");
                }

                SQL.AppendLine("and nfe.tipo = 0");
                AdicionarFiltros(filtro, SQL);

                if (filtro.DaEmissao != null && filtro.AteEmissao != null && filtro.AteEmissao.ToString("yyyy-MM-dd") != "0001-01-01")
                    SQL.AppendLine(" AND Date(nfe.DataInclusao) between '" + filtro.DaEmissao.ToString("yyyy-MM-dd") + "' AND '" + filtro.AteEmissao.ToString("yyyy-MM-dd") + "'");
                
            }

            if (filtro.nfe && filtro.nfce)//se usa nfe e nfce
            {
                SQL.AppendLine("union all");
            }

            if (filtro.nfce)//se usa nfce
            {
                SQL.AppendLine("select produto.referencia, produto.descricao, (ifnull(nfcei.quantidade, 0) - ifnull(nfcei.Qtddevolvida, 0)) as quantidade, nfcei.preco, ((ifnull(nfcei.quantidade, 0) - ifnull(nfcei.Qtddevolvida, 0))*nfcei.preco) as total, ");
                SQL.AppendLine("cores.Abreviatura as cor, tamanhos.Abreviatura as tamanho");
                SQL.AppendLine("from nfce");
                SQL.AppendLine("inner join nfceitens nfcei on nfcei.IdNfce = nfce.id");
                SQL.AppendLine("inner join produtos produto on nfcei.idproduto = produto.id");
                SQL.AppendLine("inner join cores on nfcei.idcor = cores.id");
                SQL.AppendLine("inner join tamanhos on nfcei.idtamanho = tamanhos.id");
                SQL.AppendLine(" WHERE " + FiltroEmpresa("NFCE.Idempresa"));
                SQL.AppendLine(" AND nfcei.Devolucao = 0 ");
                SQL.AppendLine("AND ");

                if (filtro.SomenteEmitidos)
                {
                    SQL.AppendLine(" nfce.statusnota = 1");
                }
                else
                {
                    SQL.AppendLine(" nfce.statusnota <> 2");
                }

                AdicionarFiltros(filtro, SQL);

                if (filtro.DaEmissao != null && filtro.AteEmissao != null && filtro.AteEmissao.ToString("yyyy-MM-dd") != "0001-01-01")
                    SQL.AppendLine(" AND Date(nfce.dataemissao) between '" + filtro.DaEmissao.ToString("yyyy-MM-dd") + "' AND '" + filtro.AteEmissao.ToString("yyyy-MM-dd") + "'");

            }

           
                SQL.AppendLine(") as nf");
                SQL.AppendLine("where quantidade > 0");

                if (filtro.Agrupar)
                {
                    SQL.AppendLine("group by referencia");
                }
                else
                {
                    SQL.AppendLine("group by referencia, cor, tamanho");
                }

                if (filtro.precoDiferente)//verifica se agrupa por preço
                    SQL.Append(", preco");


            var cn = new DapperConnection<RankingVenda>();
            return cn.ExecuteStringSqlToList(new RankingVenda(), SQL.ToString());
        }

        private static void AdicionarFiltros(FiltroRankingVendaRelatorio filtro, StringBuilder SQL)
        {
            if (filtro.Produtos != null && filtro.Produtos.Count() > 0)
                SQL.AppendLine(" AND produto.id in (" + string.Join(",", filtro.Produtos.ToArray()) + ")");

            if (filtro.Cor != null && filtro.Cor.Count() > 0)
                SQL.AppendLine(" AND cores.id in (" + string.Join(",", filtro.Cor.ToArray()) + ")");

            if (filtro.Tamanho != null && filtro.Tamanho.Count() > 0)
                SQL.AppendLine(" AND tamanhos.id in (" + string.Join(",", filtro.Tamanho.ToArray()) + ")");

            if (filtro.Segmento != null && filtro.Segmento.Count() > 0)
                SQL.AppendLine(" AND produto.idSegmento in (" + string.Join(",", filtro.Segmento.ToArray()) + ")");

            if (filtro.Colecao != null && filtro.Colecao.Count() > 0)
                SQL.AppendLine(" AND produto.idColecao in (" + string.Join(",", filtro.Colecao.ToArray()) + ")");

            if (filtro.Grupo != null && filtro.Grupo.Count() > 0)
                SQL.AppendLine(" AND produto.idGrupo in (" + string.Join(",", filtro.Grupo.ToArray()) + ")");

            if (filtro.DoAno != null && filtro.DoAno != "")
                SQL.AppendLine(" AND produto.ano >= " + filtro.DoAno + "");

            if (filtro.AteAno != null && filtro.AteAno != "")
                SQL.AppendLine(" AND produto.ano <= " + filtro.AteAno + "");
        }

    }
}
