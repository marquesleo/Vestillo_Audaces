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
    public class TamanhoRepository: GenericRepository<Tamanho>
    {
        public TamanhoRepository()
            : base(new DapperConnection<Tamanho>())
        {
        }

        public IEnumerable<Tamanho> GetByAtivos(int AtivoInativo)
        {
            var SQL = new Select()
                .Campos("Id,Abreviatura,Descricao,Ativo ")
                .From("tamanhos ")
                .Where(" Ativo = " + AtivoInativo);

            var tm = new Tamanho();
            return _cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }

        public IEnumerable<Tamanho> GetListByIds(List<int> ids)
        {
            var SQL = new Select()
                .Campos("*")
                .From("tamanhos")
                .Where("id in ( " + string.Join(",", ids) + " )");

            var tm = new Tamanho();
            return _cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }

        public IEnumerable<Tamanho> GetListPorDescricao(string Descricao)
        {
            var SQL = new Select()
                .Campos("Id,Abreviatura,Descricao,Ativo ")
                .From("tamanhos ")
               .Where(" descricao like '%" + Descricao + "%' And ativo = 1");

            var tm = new Tamanho();
            return _cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }

        public IEnumerable<Tamanho> GetTamanhosProduto(int produto)
        {
            var SQL = new Select()
                .Campos("t.Id,t.Abreviatura,t.Descricao,t.Ativo ")
                .From("tamanhos t")
                .InnerJoin(" produtoDetalhes pd", " pd.IdTamanho = t.id")
                .Where(" pd.IdProduto = " + produto + " and pd.Inutilizado = 0")
                .GroupBy(" t.id")
                .OrderBy(" t.id");

            var tm = new Tamanho();
            return _cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }

        public int RetornaIdTamanho(string NomeTamanho)
        {
            int IdTamanho = 0;
            string Sql = String.Empty;

            Sql = "select id from tamanhos where tamanhos.Abreviatura = " + "'" + NomeTamanho + "'";
            var tm = new Tamanho();
            var Dados = _cn.ExecuteStringSqlToList(tm, Sql).ToList();

            if (Dados != null && Dados.Count() > 0)
            {
                IdTamanho = Dados[0].Id;
            }
            return IdTamanho;
        }
        public IEnumerable<Tamanho> GetTamanhosCatalogo(List<int> catalogoIds)
        {
            string SQL = String.Empty;
            SQL = " SELECT Distinct(tamanhos.Id) as Id,tamanhos.Abreviatura from PRODUTOS " +
                  "  INNER JOIN catalogo on catalogo.id = produtos.IdCatalogo " +
                  "  INNER JOIN produtodetalhes ON produtodetalhes.IdProduto = produtos.Id " +
                  "  INNER JOIN tamanhos ON tamanhos.Id = produtodetalhes.IdTamanho " +
                  "  WHERE produtos.IdCatalogo IN(" + string.Join(", ", catalogoIds) + ") " + "Order by tamanhos.Id";
            var tm = new Tamanho();
            return _cn.ExecuteStringSqlToList(tm, SQL);
        }
    }
}
