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
    
    public class TipoMovimentacaoRepository: GenericRepository<TipoMovimentacao>
    {
        private class Existentes
        {
            public int IdNfe { get; set; }
            public int IdNotaEntrada { get; set; }
            public int IdPedidoVenda { get; set; }
            public int IdPedidoCompra { get; set; }
        }

        public TipoMovimentacaoRepository()
            : base(new DapperConnection<TipoMovimentacao>())
        {
        }

        public IEnumerable<TipoMovimentacao> GetListByTipoEId(int tipo, int id, int SomenteAtivo)
        {
            string where;

            if (tipo == 0)
                where = "id = " + id;
            else
                where = "tipo = " + tipo + " And id = " + id;

            if(SomenteAtivo == 1)
            {
                where = where +  " AND Ativo = 1 ";
            }

            var SQL = new Select() 
                .Campos("id, referencia, descricao")
                .From("tipomovimentacoes ")
                .Where(where.ToString());

            var tm = new TipoMovimentacao();
            return _cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }


        public IEnumerable<TipoMovimentacao> GetPorReferencia(int tipo, string referencia, int SomenteAtivo)
        {
            string where;

            if (tipo == 0)
                where = "referencia like '%" + referencia + "%'";
            else
                where = "referencia like '%" + referencia + "%' And tipo = " + tipo;


            if (SomenteAtivo == 1)
            {
                where = where + " AND Ativo = 1 ";
            }

            TipoMovimentacao m = new TipoMovimentacao();

            return _cn.ExecuteToList(m, where.ToString());
        }

        public IEnumerable<TipoMovimentacao> GetPorDescricao(int tipo, string desc, int SomenteAtivo)
        {
            string where;

            if (tipo == 0)
                where = "descricao like '%" + desc + "%'";
            else
                where = "descricao like '%" + desc + "%' And tipo = " + tipo;

            if (SomenteAtivo == 1)
            {
                where = where + " AND Ativo = 1 ";
            }

            where = where + " Order By referencia";
            TipoMovimentacao m = new TipoMovimentacao();
            return _cn.ExecuteToList(m, "descricao like '%" + desc + "%' And tipo = " + tipo );
        }

        public int GetCountUso(int IdTipoMovimentacao)
        {
            int count = 0;

            string sql = " SELECT * FROM tipomovimentacoes " +
                         " INNER JOIN nfeitens on  nfeitens.IdTipoMov = tipomovimentacoes.id " +
                         " where nfeitens.IdTipoMov  = " + IdTipoMovimentacao + " group by nfeitens.IdNfe limit 1";
            var c = new TipoMovimentacao();
            var dados = _cn.ExecuteStringSqlToList(c, sql.ToString());
            if (dados != null && dados.Count() > 0)
            {
                count += dados.Count();
            }


            string sql1 = " SELECT * FROM tipomovimentacoes " +
                         " INNER JOIN itenspedidovenda on  itenspedidovenda.TipoMovimentacaoId = tipomovimentacoes.id " +
                         " where itenspedidovenda.TipoMovimentacaoId = " + IdTipoMovimentacao + " group by itenspedidovenda.PedidoVendaId limit 1";
            var c1 = new TipoMovimentacao();
            var dados1 = _cn.ExecuteStringSqlToList(c1, sql1.ToString());
            if (dados1 != null && dados1.Count() > 0)
            {
                count += dados1.Count();
            }

            string sql2 = " SELECT * FROM tipomovimentacoes " +
                         " INNER JOIN notaentradaitens on  notaentradaitens.IdTipoMov = tipomovimentacoes.id " +
                         " where notaentradaitens.IdTipoMov = " + IdTipoMovimentacao + " group by notaentradaitens.IdNota limit 1";
            var c2 = new TipoMovimentacao();
            var dados2 = _cn.ExecuteStringSqlToList(c2, sql2.ToString());
            if (dados2 != null && dados2.Count() > 0)
            {
                count += dados2.Count();
            }

            return count;

        }

        public bool TipoUsado(int IdTipo)
        {
            bool JaFoiUsado = false;
            var cn = new DapperConnection<Existentes>();
            int IdNfe = 0;
            int IdNotaEntrada = 0;
            int IdPedidoVenda = 0;
            int IdPedidoCompra = 0;
            string SQL = String.Empty;

            var c1 = new Existentes();         

             SQL =  "select IFNULL(nfeitens.id,0) as IdNfe from nfeitens WHERE nfeitens.IdTipoMov = " + IdTipo + " limit 1 ";
             var dados1 = cn.ExecuteStringSqlToList(c1, SQL);
             SQL = " select IFNULL(notaentradaitens.id,0) as IdNotaEntrada from notaentradaitens WHERE notaentradaitens.IdTipoMov = " + IdTipo + " limit 1 " ;
             var dados2 = cn.ExecuteStringSqlToList(c1, SQL);
             SQL = " select IFNULL(itenspedidovenda.id,0) as IdPedidoVenda from itenspedidovenda WHERE itenspedidovenda.TipoMovimentacaoId = " + IdTipo + " limit 1 " ;
             var dados3 = cn.ExecuteStringSqlToList(c1, SQL);
             SQL = " select IFNULL(itenspedidocompra.Id,0) as IdPedidoCompra from itenspedidocompra WHERE itenspedidocompra.TipoMovimentacaoId = " + IdTipo + " limit 1 " ;
             var dados4 = cn.ExecuteStringSqlToList(c1, SQL);


           

            foreach (var item in dados1)
            {
                IdNfe = item.IdNfe;
            }

            foreach (var item in dados2)
            {
                IdNotaEntrada = item.IdNotaEntrada;
            }
            foreach (var item in dados3)
            {
                IdPedidoVenda = item.IdPedidoVenda;
            }
            foreach (var item in dados4)
            {
                IdPedidoCompra = item.IdPedidoCompra;
            }
            
            
            

            if (IdNfe > 0 || IdNotaEntrada > 0 || IdPedidoVenda  > 0 || IdPedidoCompra  > 0 )
            {
                JaFoiUsado = true;
            }
            return JaFoiUsado;


        }

        
                              
    }
}
